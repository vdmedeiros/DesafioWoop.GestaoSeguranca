using DesafioWoop.GestaoSeguranca.API.Model;

namespace DesafioWoop.GestaoSeguranca.API.Data.Repository
{
    public interface IQuestionarioUsuarioRepository
    {
        public void Add(QuestionarioUsuario questionarioUsuario);

        public void Delete(QuestionarioUsuario questionarioUsuario);

        public QuestionarioUsuario GetById(int id);

        public void Update(QuestionarioUsuario questionarioUsuario);

    }
}
