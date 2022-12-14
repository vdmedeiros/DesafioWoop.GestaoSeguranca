using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioWoop.GestaoSeguranca.API.Model
{
    public class UserLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }       
        
        public List<QuestionarioUsuario> QuestionarioUsuarios { get; set; }

        public UserLogin(string email, string senha)
        {
            Email = email;
            Senha = senha;
            
        }

        public void AdicionarQuestionarioUsuario(QuestionarioUsuario questionarioUsuario)
        {
            QuestionarioUsuarios.Add(questionarioUsuario);

        }
    }
}
