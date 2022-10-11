using DesafioWoop.GestaoSeguranca.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DesafioWoop.GestaoSeguranca.API.Data.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserLoginRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserLogin> AddLogin(UserLogin userLogin)
        {
            await _dbContext.UserLogin.AddAsync(userLogin);
            _dbContext.SaveChanges();
            return userLogin;
        }

        public void UpdateLogin(UserLogin userLogin)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.UserLogin.Update(userLogin);

                var userHistory = new UserLoginHistory() { DataCriacao = DateTime.Now, UserLogin = userLogin, Senha = userLogin.Senha };
                _dbContext.UserLoginHistory.Add(userHistory);
                _dbContext.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<UserLogin> GetUser(string email, string password)
        {
            return await _dbContext.UserLogin.FirstOrDefaultAsync(u => u.Email == email && u.Senha == password);
        }

        public async Task<UserLogin> GetByEmail(string email)
        {
            return await _dbContext.UserLogin.Include("QuestionarioUsuarios").FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> CanChangePassword(int idUser, string password, int qtdLastPassword)
        {
            return await _dbContext.UserLoginHistory.Where(u => u.Id == idUser && u.Senha == password).OrderBy(o => o.DataCriacao).Take(qtdLastPassword).CountAsync() == 0;
        }
    }
}
