using Me20.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Results
{
    public class CommandResult : ICommandResult
    {
        public IEnumerable<string> ErrorList => Errors;
        public bool Successful => ErrorList == null || !ErrorList.Any();

        private List<string> Errors;

        public CommandResult()
        {
            Errors = new List<string>();
        }

        public CommandResult(IResult validationResult) : this()
        {
            AddErrorsFrom(validationResult);
        }

        public ICommandResult AddError(string errorMsg)
        {
            Errors.Add(errorMsg);
            return this;
        }

        public ICommandResult AddUnexpectedError()
        {
            AddError("Unexpected error occured");
            return this;
        }

        public ICommandResult AddErrorsFrom(IResult otherResult)
        {
            if (otherResult != null && otherResult.ErrorList != null)
                Errors.AddRange(otherResult.ErrorList);
            return this;
        }

        public static bool operator true(CommandResult result) => result.Successful;

        public static bool operator false(CommandResult result) => !result.Successful;
    }
}
