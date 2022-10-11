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
using System.Net;

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

        [Fact(DisplayName = "Salvar Question�rio - Mediator")]
        [Trait("Question�rio Usu�rio", "Salvar - Mediator Command Handler")]
        public async Task Handler_sends_mediator_command_questionario()
        {
            // Arrange
            var questionarioUsuarioRequest = new Fixture().Create<QuestionarioUsuarioRequest>();

            var commandResultFake = new CommandResult(true);

            _mediator.Setup(x => x.Send(It.IsAny<IRequest<CommandResult>>(),
                            default(System.Threading.CancellationToken))).Returns(Task.FromResult(commandResultFake));

            // Act
            var controller = new QuestionarioUsuariosController(_config.Object, _questionarioRepository.Object, _questionarioUsuarioQueries.Object, _questionarioService.Object, _userLoginRepository.Object,
                                                                _appSettings.Object, _mediator.Object, _log.Object, _userLoginService.Object);

            var result = await controller.SalvarQuestionarioUsuario(questionarioUsuarioRequest);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, ((StatusCodeResult)result).StatusCode);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<CommandResult>>(), default(System.Threading.CancellationToken)), Times.Once);

        }

        [Fact(DisplayName = "Salvar Question�rio sem sucesso")]
        [Trait("Question�rio Usu�rio", "Salvar - Mediator Command Handler No")]
        public async Task Handler_sends_invalid_command_questionario()
        {
            // Arrange
            var questionarioCommand = new Fixture().Create<QuestionarioUsuarioCommand>();

            // Act
            var handler = new QuestionarioUsuarioCommandHandler(_mediator.Object, _questionarioRepository.Object, _config.Object, _userLoginRepository.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(questionarioCommand, cltToken);

            //Assert
            Assert.False(result.Success);
            
        }       
    }
}