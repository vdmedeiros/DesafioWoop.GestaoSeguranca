using DesafioWoop.GestaoSeguranca.API.Model;

namespace DesafioWoop.GestaoSeguranca.API.Data.Repository
{
    public interface IUserLoginRepository
    {
        public Task<UserLogin> AddLogin(UserLogin userLogin);
        public Task<UserLogin> GetUser(string email, string password);
    }
}