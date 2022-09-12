using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioWoop.GestaoSeguranca.API.Model
{
    public class QuestionarioUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserLogin UserLogin { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Pergunta { get; set; }
        public string Resposta { get; set; }        
    }
}
