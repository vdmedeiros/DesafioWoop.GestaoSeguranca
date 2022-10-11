using AutoFixture;
using DesafioWoop.GestaoSeguranca.API.Application.Commands;
using DesafioWoop.GestaoSeguranca.API.Application.Queries;
using DesafioWoop.GestaoSeguranca.API.Commands;
using DesafioWoop.GestaoSeguranca.API.Controllers;
using DesafioWoop.GestaoSeguranca.API.Core;
using DesafioWoop.GestaoSeguranca.API.Data;
using DesafioWoop.GestaoSeguranca.API.Data.Repository;
using DesafioWoop.GestaoSeguranca.API.Model;
using DesafioWoop.GestaoSeguranca.API.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace DesafioWoop.GestaoSeguranca.Tests
{    
    public class QuestionarioUsuarioAddTests
    {
     
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<QuestionarioUsuariosController>> _log;
        private readonly Mock<IConfiguration> _config;
        private readonly Mock<IQuestionarioUsuarioRepository> _questionarioRepository;
        private readonly Mock<IUserLoginRepository> _userLoginRepository;
        private readonly Mock<IOptions<AppSettings>> _appSettings;
        private readonly Mock<IQuestionarioUsuarioQueries> _questionarioUsuarioQueries;
        private readonly Mock<IQuestionarioService> _questionarioService;
        private readonly Mock<IUserLoginService> _userLoginService;

        public QuestionarioUsuarioAddTests()
        {
            _mediator = new Mock<IMediator>();
            _log = new Mock<ILogger<QuestionarioUsuariosController>>();
            _config = new Mock<IConfiguration>();
            _questionarioRepository = new Mock<IQuestionarioUsuarioRepository>();
            _userLoginRepository = new Mock<IUserLoginRepository>();
            _appSettings = new Mock<IOptions<AppSettings>>();
            _questionarioUsuarioQueries = new Mock<IQuestionarioUsuarioQueries>();
            _questionarioService = new Mock<IQuestionarioService>();
            _userLoginService = new Mock<IUserLoginService>();
        }

        [Fact(DisplayName = "Salvar Questionário com sucesso")]
        [Trait("Questionário Usuário", "Salvar - Mediator Command Handler")]
        public async Task Handler_sends_command_when_questionario_no_exists()
        {
            // Arrange
            var questionarioCommand = new Fixture().Create<QuestionarioUsuarioCommand>();
            var commandResultFake = new CommandResult(true);
            _mediator.Setup(x => x.Send(It.IsAny<IRequest<CommandResult>>(),
                            default(System.Threading.CancellationToken))).Returns(Task.FromResult(commandResultFake));

            // Act
            var handler = new QuestionarioUsuarioCommandHandler(_mediator.Object, _questionarioRepository.Object, _config.Object, _userLoginRepository.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(questionarioCommand, cltToken);

            //Assert
            Assert.NotNull(result);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<bool>>(), default(System.Threading.CancellationToken)), Times.Once());
        }

        [Fact(DisplayName = "Salvar Questionário sem sucesso")]
        [Trait("Questionário Usuário", "Salvar - Mediator Command Handler No")]
        public async Task Handler_sends_no_command_when_questionario_exists()
        {
            // Arrange
            var questionarioUsuarioRequest = new Fixture().Create<QuestionarioUsuarioRequest>();

            var commandResultFake = new CommandResult(false);

            _mediator.Setup(x => x.Send(It.IsAny<IRequest<CommandResult>>(),
                            default(System.Threading.CancellationToken))).Returns(Task.FromResult(commandResultFake));

            // Act
            var controller = new QuestionarioUsuariosController(_config.Object, _questionarioRepository.Object, _questionarioUsuarioQueries.Object, _questionarioService.Object,
                                                                _userLoginRepository.Object, _appSettings.Object, _mediator.Object, _log.Object, _userLoginService.Object);

            var result = await controller.SalvarQuestionarioUsuario(questionarioUsuarioRequest);

            //Assert
            Assert.NotNull(result);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<CommandResult>>(), default(System.Threading.CancellationToken)), Times.Never());
        }

        //[Fact(DisplayName = "Remover Questionário com sucesso")]
        //[Trait("Questionário Usuário", "Remover por Id")]
        //public async Task QuestionarioUsuario_RemoveQuestionario_NotFound()
        //{
        //    var questionarioUsuario = new QuestionarioUsuario()
        //    {
        //        Id = 6,
        //        DataCriacao = DateTime.Now,
        //        UserId = 8,
        //        Pergunta = "Teste 123?",
        //        Resposta = "Resposta 123"
        //    };

        //    // set up the repository’s Delete call
        //    _questionarioRepository.Setup(r => r.Delete(It.IsAny<QuestionarioUsuario>()));

        //    // act
        //    var controller = new QuestionarioUsuariosController(_config.Object, _questionarioRepository.Object, _questionarioUsuarioQueries.Object, _userLoginRepository.Object,
        //                                                        _appSettings.Object, _mediator.Object, _log.Object);

        //    var result = await controller.RemoverQuestionario(questionarioUsuario.Id);

        //    // assert            
        //    Assert.IsType<NotFoundResult>(questionarioUsuario);

        //}
    }
}