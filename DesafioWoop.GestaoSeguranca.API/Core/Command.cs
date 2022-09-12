using MediatR;
using FluentValidation.Results;
using DesafioWoop.GestaoSeguranca.API.Application.Commands;

namespace DesafioWoop.GestaoSeguranca.API.Core
{
    public abstract class Command : IRequest<CommandResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual ValidationResult ValidarCommand()
        {
            throw new NotImplementedException();
        }
    }
}
