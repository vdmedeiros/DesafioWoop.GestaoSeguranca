using DesafioWoop.GestaoSeguranca.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DesafioWoop.GestaoSeguranca.API.Data.Repository
{
    public class QuestionarioUsuarioRepository : IQuestionarioUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionarioUsuarioRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public void Add(QuestionarioUsuario questionarioUsuario)
        {
            _dbContext.QuestionarioUsuario.Add(questionarioUsuario);
            _dbContext.SaveChanges();
        }
        public void Update(QuestionarioUsuario questionarioUsuario)
        {
            _dbContext.Entry(questionarioUsuario).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(QuestionarioUsuario questionarioUsuario)
        {
            _dbContext.QuestionarioUsuario.Remove(questionarioUsuario);
            _dbContext.SaveChanges();
        }

        public IEnumerable<QuestionarioUsuario> GetAll()
        {
            return _dbContext.QuestionarioUsuario.ToList();
        }

        public List<QuestionarioUsuario> GetByEmail(string email)
        {
            return _dbContext.QuestionarioUsuario.Where(q => q.UserLogin.Email.Equals(email)).ToList();
        }

        public QuestionarioUsuario GetById(int id)
        {
            return _dbContext.QuestionarioUsuario.FirstOrDefault(q => q.Id == id);
        }
    }
}
