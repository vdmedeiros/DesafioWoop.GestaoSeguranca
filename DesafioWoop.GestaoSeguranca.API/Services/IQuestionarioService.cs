using DesafioWoop.GestaoSeguranca.API.Model;

namespace DesafioWoop.GestaoSeguranca.API.Services
{
    public interface IQuestionarioService
    {
        public Task<IEnumerable<QuestionarioUsuario>> ValidaPerguntaResposta(RespostaUsuarioRequest respostaUsuarioRequest);
    }
}