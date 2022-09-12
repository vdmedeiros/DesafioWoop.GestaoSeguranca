using AutoFixture;
using DesafioWoop.GestaoSeguranca.API.Application.Commands;
using DesafioWoop.GestaoSeguranca.API.Controllers;
using DesafioWoop.GestaoSeguranca.API.Core;
using DesafioWoop.GestaoSeguranca.API.Data.Repository;
using DesafioWoop.GestaoSeguranca.API.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public QuestionarioUsuarioAddTests()
        {
            _mediator = new Mock<IMediator>();
            _log = new Mock<ILogger<QuestionarioUsuariosController>>();
            _config = new Mock<IConfiguration>();
            _questionarioRepository = new Mock<IQuestionarioUsuarioRepository>();
            _userLoginRepository = new Mock<IUserLoginRepository>();
            _appSettings = new Mock<IOptions<AppSettings>>();
        }

        [Fact(DisplayName = "Salvar Questionário com sucesso")]
        [Trait("Questionário Usuário", "Salvar - Mediator Command Handler")]
        public async Task QuestionarioUsuario_NovoQuestionario_Mediator()
        {
            // Arrange
            var questionarioUsuarioRequest = new Fixture().Create<QuestionarioUsuarioRequest>();

            var commandResultFake = new CommandResult(true);

            _mediator.Setup(x => x.Send(It.IsAny<IRequest<CommandResult>>(),
                            default(System.Threading.CancellationToken))).Returns(Task.FromResult(commandResultFake));

            // Act
            var controller = new QuestionarioUsuariosController(_config.Object, _questionarioRepository.Object, _userLoginRepository.Object,
                                                                _appSettings.Object, _mediator.Object, _log.Object);

            var result = await controller.SalvarQuestionarioUsuario(questionarioUsuarioRequest);

            //Assert
            Assert.NotNull(result);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<CommandResult>>(), default(System.Threading.CancellationToken)), Times.Once);
        }

        [Fact(DisplayName = "Remover Questionário com sucesso")]
        [Trait("Questionário Usuário", "Remover por Id")]
        public async Task QuestionarioUsuario_RemoveQuestionario_NotFound()
        {
            var questionarioUsuario = new QuestionarioUsuario()
            {
                Id = 5,
                DataCriacao = DateTime.Now,
                UserId = 8,
                Pergunta = "Teste 123?",
                Resposta = "Resposta 123"
            };

            // set up the repository’s Delete call
            _questionarioRepository.Setup(r => r.Delete(It.IsAny<QuestionarioUsuario>()));

            // act
            var controller = new QuestionarioUsuariosController(_config.Object, _questionarioRepository.Object, _userLoginRepository.Object,
                                                                _appSettings.Object, _mediator.Object, _log.Object);

            var result = await controller.RemoverQuestionario(questionarioUsuario.Id);

            // assert            
            Assert.IsType<NotFoundResult>(questionarioUsuario);

        }
    }
}