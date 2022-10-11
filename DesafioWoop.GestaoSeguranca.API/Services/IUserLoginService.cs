using DesafioWoop.GestaoSeguranca.API.Model;

namespace DesafioWoop.GestaoSeguranca.API.Services
{
    public interface IUserLoginService
    {
        public Task<bool> RealizaAlteracaoSenha(UserLoginRegister userLogin);
    }
}