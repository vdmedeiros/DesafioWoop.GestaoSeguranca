using DesafioWoop.GestaoSeguranca.API.Model;

namespace DesafioWoop.GestaoSeguranca.API.Application.Queries
{
    public interface IQuestionarioUsuarioQueries
    {
        public Task<IEnumerable<QuestionarioUsuario>> GetByEmail(string email);        
        public Task<IEnumerable<QuestionarioUsuario>> GetAll();
    }
}