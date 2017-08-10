using Me20.Contracts;
using Me20.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Abstracts
{
    public abstract class CommandHandlerBase<TCommand> : IHandleCommands<TCommand> where TCommand : ICommand
    {
        protected virtual Action<TCommand> OnBeforeCommandValidation { get; set; }
        protected virtual Action<TCommand, IResult> OnAfterCommandValidation { get; set; }
        protected virtual Action<TCommand, IResult> OnAfterCommandValidationPassed { get; set; }
        protected virtual Action<TCommand, IResult> OnAfterCommandValidationFailed { get; set; }
        protected virtual Action<TCommand> OnBeforeCommandExecute { get; set; }
        protected virtual Action<TCommand, ICommandResult> OnAfterCommandExecute { get; set; }

        public abstract IEnumerable<Action<TCommand, ValidationResult>> Validators { get; }

        protected CommandHandlerBase()
        {

        }

        public virtual IResult Validate(TCommand item)
        {
            var validationResult = new ValidationResult();
            if (Validators != null)
            {
                foreach (var v in Validators)
                {
                    v?.Invoke(item, validationResult);

                    if (validationResult.Break)
                        return validationResult;
                }
            }
            return validationResult;
        }

        public async Task<ICommandResult> Handle(TCommand cmd)
        {
            var result = new CommandResult();
            try
            {
                OnBeforeCommandValidation?.Invoke(cmd);

                var validationResult = Validate(cmd);

                OnAfterCommandValidation?.Invoke(cmd, validationResult);

                if (!validationResult.Successful)
                {
                    OnAfterCommandValidationFailed?.Invoke(cmd, validationResult);
                    return result.AddErrorsFrom(validationResult);
                }

                OnAfterCommandValidationPassed?.Invoke(cmd, validationResult);

                OnBeforeCommandExecute?.Invoke(cmd);

                result = await ExecuteCommand(cmd, result);

                OnAfterCommandExecute?.Invoke(cmd, result);
            }
            catch (Exception)
            {
                result.AddUnexpectedError();
            }
            return result;
        }



        protected abstract Task<CommandResult> ExecuteCommand(TCommand command, ICommandResult result);

    }
}
