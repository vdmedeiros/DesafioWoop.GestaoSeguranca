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

        public async Task<UserLogin> GetUser(string email, string password)
        {
            return await _dbContext.UserLogin.FirstOrDefaultAsync(u => u.Email == email && u.Senha == password);
        }
    }
}
