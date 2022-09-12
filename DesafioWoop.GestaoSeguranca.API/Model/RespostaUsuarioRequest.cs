using System.ComponentModel.DataAnnotations;

namespace DesafioWoop.GestaoSeguranca.API.Model
{
    public class RespostaUsuarioRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        public List<RespostaRequest> RespostasUsuario { get; set; }
    }
    public class RespostaRequest
    {
        public int IdQuestionario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Resposta { get; set; }
    }
}
