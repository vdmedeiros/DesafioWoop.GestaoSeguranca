using DesafioWoop.GestaoSeguranca.API.Model;

namespace DesafioWoop.GestaoSeguranca.API.Data.Repository
{
    public interface IUserLoginRepository
    {
        public Task<UserLogin> AddLogin(UserLogin userLogin);
        public void UpdateLogin(UserLogin userLogin);
        public Task<UserLogin> GetUser(string email, string password);
        public Task<UserLogin> GetByEmail(string email);
        public Task<bool> CanChangePassword(int idUser, string password, int qtdLastPassword);

    }
}