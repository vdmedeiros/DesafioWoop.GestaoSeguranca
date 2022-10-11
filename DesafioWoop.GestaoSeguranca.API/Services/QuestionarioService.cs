using DesafioWoop.GestaoSeguranca.API.Application.Queries;
using DesafioWoop.GestaoSeguranca.API.Core;
using DesafioWoop.GestaoSeguranca.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWoop.GestaoSeguranca.API.Services
{
    public class QuestionarioService : IQuestionarioService
    {
        private readonly IQuestionarioUsuarioQueries _questionarioUsuarioQueries;
        
        public QuestionarioService(IQuestionarioUsuarioQueries questionarioUsuarioQueries)
        {
            _questionarioUsuarioQueries = questionarioUsuarioQueries;
        }

        public async Task<IEnumerable<QuestionarioUsuario>> ValidaPerguntaResposta(RespostaUsuarioRequest respostaUsuarioRequest)
        {
            var questionarioUsuarioBD = await _questionarioUsuarioQueries.GetByEmail(respostaUsuarioRequest.Email);

            if (questionarioUsuarioBD == null)
            {
                throw new ArgumentException("Não tem registro de Questionário para este usuário");
            }

            foreach (var respostaRequest in respostaUsuarioRequest.RespostasUsuario)
            {
                var respostaBD = questionarioUsuarioBD.FirstOrDefault(q => ValidaResposta(q, respostaRequest));
                if (respostaBD == null)
                {
                    return questionarioUsuarioBD.ToList();
                }
            }
            return null;
        }

        private static bool ValidaResposta(QuestionarioUsuario q, RespostaRequest respostaRequest)
        {
            return q.Id == respostaRequest.IdQuestionario && q.Resposta == respostaRequest.Resposta;
        }
    }
}
