using DesafioWoop.GestaoSeguranca.API.Core;
using DesafioWoop.GestaoSeguranca.API.Model;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DesafioWoop.GestaoSeguranca.API.Commands
{
    [DataContract]
    public class QuestionarioUsuarioCommand : Command
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public List<Questionario> Questionarios { get; set; }

        public QuestionarioUsuarioCommand(string email, List<QuestionarioRequest> questionario)
        {
            Email = email;
            Questionarios = new List<Questionario>();
            foreach (var item in questionario)
            {
                Questionarios.Add(new Commands.Questionario() { Id = item.Id, Pergunta = item.Pergunta, Resposta = item.Resposta });
            }
        }

        public override FluentValidation.Results.ValidationResult ValidarCommand()
        {
            return new QuestionarioUsuarioValidation().Validate(this);            
        }
    }

    public class Questionario
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Pergunta { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Resposta { get; set; }
    }


    public class QuestionarioUsuarioValidation : AbstractValidator<QuestionarioUsuarioCommand>
    {
        public static string EmailErroMsg => "Email inválido";
        public static string QuestionarioErroMsg => "Nenhuma questão foi incluída";
        public static string QuantidadeQuestoesPermitida => "Não pode ser incluído mais do que 10 questões";

        public QuestionarioUsuarioValidation()
        {
            RuleFor(c => c.Email)
               .NotEqual(String.Empty)
               .EmailAddress()
               .WithMessage(EmailErroMsg);

            RuleFor(c => c.Questionarios)
                .NotNull()
                .WithMessage(QuestionarioErroMsg);

            ///Caso queira limitar uma quantidade de questões
            RuleFor(x => x.Questionarios)
                .Must(x => x.Count <= 2).WithMessage(QuantidadeQuestoesPermitida);
        }
    }
}