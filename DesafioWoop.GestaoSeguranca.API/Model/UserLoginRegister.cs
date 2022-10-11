using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioWoop.GestaoSeguranca.API.Model
{
    public class UserLoginRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
                
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string SenhaConfirmacao { get; set; }
        
        [JsonIgnore]
        public List<QuestionarioUsuario>? QuestionarioUsuarios { get; set; }

        public UserLogin ToEntity() => new(Email, Senha);
    }
}
