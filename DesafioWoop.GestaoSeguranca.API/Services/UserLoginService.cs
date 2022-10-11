using DesafioWoop.GestaoSeguranca.API.Core;
using DesafioWoop.GestaoSeguranca.API.Data.Repository;
using DesafioWoop.GestaoSeguranca.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DesafioWoop.GestaoSeguranca.API.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly AppSettings _appSettings;
        public UserLoginService(IUserLoginRepository userLoginRepository, IOptions<AppSettings> appSettings)
        {
            _userLoginRepository = userLoginRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> RealizaAlteracaoSenha(UserLoginRegister userLogin)
        {
            var userBd = await _userLoginRepository.GetByEmail(userLogin.Email);
            if (userBd != null)
            {
                var valido = await _userLoginRepository.CanChangePassword(userBd.Id, userLogin.Senha, _appSettings.QtdeUltimasSenhas);
                if (valido)
                {
                    userBd.Senha = userLogin.Senha;
                    _userLoginRepository.UpdateLogin(userBd);
                    return true;
                }                
            }
            return false;
        }
    }
}
