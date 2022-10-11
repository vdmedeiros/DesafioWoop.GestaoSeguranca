using Microsoft.AspNetCore.Mvc;
using DesafioWoop.GestaoSeguranca.API.Data;
using DesafioWoop.GestaoSeguranca.API.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DesafioWoop.GestaoSeguranca.API.Core;
using Microsoft.Extensions.Options;
using DesafioWoop.GestaoSeguranca.API.Commands;
using MediatR;
using System.Text.Json;
using DesafioWoop.GestaoSeguranca.API.Data.Repository;
using DesafioWoop.GestaoSeguranca.API.Application.Queries;
using DesafioWoop.GestaoSeguranca.API.Services;

namespace DesafioWoop.GestaoSeguranca.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionarioUsuariosController : ControllerBase
    {
        private readonly IQuestionarioUsuarioRepository _questionarioUsuarioRepository;
        private readonly IUserLoginRepository _userLoginRepository;
        public IConfiguration _configuration;
        private readonly AppSettings _appSettings;
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionarioUsuariosController> _log;
        private readonly IQuestionarioUsuarioQueries _questionarioUsuarioQueries;
        private readonly IQuestionarioService _questionarioService;
        private readonly IUserLoginService _userLoginService;

        public QuestionarioUsuariosController(IConfiguration config,
                                              IQuestionarioUsuarioRepository questionarioUsuarioRepository,
                                              IQuestionarioUsuarioQueries questionarioUsuarioQueries,
                                              IQuestionarioService questionarioService,
                                              IUserLoginRepository userLoginRepository,
                                              IOptions<AppSettings> appSettings,
                                              IMediator mediator,
                                              ILogger<QuestionarioUsuariosController> log,
                                              IUserLoginService userLoginService)
        {
            _questionarioUsuarioRepository = questionarioUsuarioRepository;
            _configuration = config;
            _appSettings = appSettings.Value;
            _mediator = mediator;
            _log = log;
            _userLoginRepository = userLoginRepository;
            _questionarioUsuarioQueries = questionarioUsuarioQueries;
            _questionarioService = questionarioService;
            _userLoginService = userLoginService;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<QuestionarioUsuario>> Index()
        {
            return await Task.FromResult(_questionarioUsuarioQueries.GetAll()).Result;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<QuestionarioUsuario>>> BuscarPorEmail(string email)
        {
            var questionarioUsuario = await _questionarioUsuarioQueries.GetByEmail(email);

            if (questionarioUsuario == null)
            {
                _log.LogWarning("{NotFound}", JsonSerializer.Serialize(email));
                return NotFound();
            }

            return await Task.FromResult(questionarioUsuario.ToList());
        }

        [HttpPost("salvarQuestionarioUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SalvarQuestionarioUsuario([FromBody] QuestionarioUsuarioRequest questionarioUsuarioRequest)
        {
            _log.LogInformation("{Log - Save}", JsonSerializer.Serialize(questionarioUsuarioRequest));
            if (ModelState.IsValid)
            {
                var commandQuestionario = new QuestionarioUsuarioCommand(questionarioUsuarioRequest.Email, questionarioUsuarioRequest.Questionario);

                var commandResult = await _mediator.Send(commandQuestionario);

                if (!commandResult.Success)
                {
                    return BadRequest(commandResult.Mensagem);
                    _log.LogInformation("{BadRequest}", JsonSerializer.Serialize(questionarioUsuarioRequest));
                }
            }

            return Ok();
        }

        [HttpPost("removerQuestionario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoverQuestionario(int id)
        {
            _log.LogInformation("{Log - Delete}", JsonSerializer.Serialize(id));

            var questionarioUsuario = _questionarioUsuarioRepository.GetById(id);

            if (questionarioUsuario == null)
            {
                _log.LogInformation("{NotFound}", JsonSerializer.Serialize(id));
                return NotFound();
            }

            _questionarioUsuarioRepository.Delete(questionarioUsuario);
            return Ok();

        }

        [HttpPost("verificarRespostas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<QuestionarioUsuario>> VerificarRespostas([FromBody] RespostaUsuarioRequest respostaUsuarioRequest)
        {
            _log.LogInformation("{Log - CheckAnswers}", JsonSerializer.Serialize(respostaUsuarioRequest));

            if (ModelState.IsValid)
            {
                return await Task.FromResult(_questionarioService.ValidaPerguntaResposta(respostaUsuarioRequest)).Result;
            }
            return Enumerable.Empty<QuestionarioUsuario>();
        }

        

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginEnter _userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userLoginRepository.GetUser(_userLogin.Email, _userLogin.Senha);

                if (user != null)
                {
                    return Ok(new { AccessToken = GerarJwt(user) });
                }
                else
                {
                    _log.LogInformation("{BadRequest}", JsonSerializer.Serialize(user));
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("registrarLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarLogin(UserLoginRegister userLogin)
        {
            if (ModelState.IsValid)
            {
                UserLogin newUser = userLogin.ToEntity();
                _userLoginRepository.AddLogin(newUser);
                return Ok(new { AccessToken = GerarJwt(newUser) });
            }
            else
            {
                _log.LogInformation("{BadRequest}", JsonSerializer.Serialize(userLogin));
                return BadRequest();
            }
        }

        [HttpPost("alterarLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarSenha(UserLoginRegister userLogin)
        {
            if (ModelState.IsValid)
            {
                var retorno = await _userLoginService.RealizaAlteracaoSenha(userLogin);
                if(!retorno)
                {
                    var messagemRetorno = string.Format("BadRequest - Senha já utilizada nas últimas {0} vezes", _appSettings.QtdeUltimasSenhas.ToString());
                    _log.LogInformation(messagemRetorno);
                    return BadRequest(messagemRetorno);
                }
                return Ok();
            }
            else
            {
                _log.LogInformation("{BadRequest}", JsonSerializer.Serialize(userLogin));
                return BadRequest();
            }
        }

        

        private string GerarJwt(UserLogin user)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                    };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _appSettings.Emissor,
                _appSettings.ValidoEm,
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }        
    }
}
