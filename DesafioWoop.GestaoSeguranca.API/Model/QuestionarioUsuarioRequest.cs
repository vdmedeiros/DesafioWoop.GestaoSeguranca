using System.ComponentModel.DataAnnotations;

namespace DesafioWoop.GestaoSeguranca.API.Model
{
    public class QuestionarioUsuarioRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        public List<QuestionarioRequest> Questionario { get; set; }                
    }

    public class QuestionarioRequest
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Pergunta { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Resposta { get; set; }
    }
}
