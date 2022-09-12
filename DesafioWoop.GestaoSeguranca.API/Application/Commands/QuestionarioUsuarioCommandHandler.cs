using DesafioWoop.GestaoSeguranca.API.Application.Commands;
using DesafioWoop.GestaoSeguranca.API.Core;
using DesafioWoop.GestaoSeguranca.API.Data;
using DesafioWoop.GestaoSeguranca.API.Data.Repository;
using DesafioWoop.GestaoSeguranca.API.Model;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DesafioWoop.GestaoSeguranca.API.Commands
{
    public class QuestionarioUsuarioCommandHandler : IRequestHandler<QuestionarioUsuarioCommand, CommandResult>
    {
        private readonly IMediator _mediatorHandler;
        private readonly IQuestionarioUsuarioRepository _questionarioUsuarioRepository;
        public IConfiguration _configuration;
        public ApplicationDbContext _dbContext;

        public QuestionarioUsuarioCommandHandler(IMediator mediator, IQuestionarioUsuarioRepository questionarioUsuarioRepository, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _mediatorHandler = mediator;
            _questionarioUsuarioRepository = questionarioUsuarioRepository;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(QuestionarioUsuarioCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult(true);
            
            try
            {
                var result = request.ValidarCommand();

                if (!result.IsValid) return new CommandResult(false, string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage)));

                var user = await _dbContext.UserLogin.Include("QuestionarioUsuarios").FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user != null)
                {
                    foreach (var questionario in request.Questionarios)
                    {
                        if (questionario.Id == null || questionario.Id == 0)
                        {
                            if (!user.QuestionarioUsuarios.Any())
                                user.QuestionarioUsuarios = new List<QuestionarioUsuario>();
                            await CriarQuestao(user, questionario);
                        }
                        else
                            await AlterarQuestao(questionario);
                    }
                }
                else
                    return await Task<CommandResult>.FromResult(new CommandResult(false, "Usuário informado não existe."));
            }
            catch(Exception ex)
            {
                commandResult = new CommandResult(false, ex.Message);
            }
            
            return await Task<CommandResult>.FromResult(commandResult);
        }

        private async Task AlterarQuestao(Questionario questionario)
        {
            var questionarioBD = _questionarioUsuarioRepository.GetById((int)questionario.Id);
            
            if (questionarioBD == null)
                throw new Exception("Id questionário não existe");

            questionarioBD.Pergunta = questionario.Pergunta;
            questionarioBD.Resposta = questionario.Resposta;
            await _dbContext.SaveChangesAsync();
        }

        private async Task CriarQuestao(UserLogin user, Questionario questionario)
        {
            var questionarioUsuario = new QuestionarioUsuario()
            {
                UserId = user.Id,
                DataCriacao = DateTime.Now,
                Pergunta = questionario.Pergunta,
                Resposta = questionario.Resposta
            };
            user.AdicionarQuestionarioUsuario(questionarioUsuario);
            await _dbContext.SaveChangesAsync();
        }        
    }
}
