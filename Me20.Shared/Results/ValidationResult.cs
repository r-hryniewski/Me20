using Me20.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Results
{
    public class ValidationResult : ICircuitBreakerResult
    {
        public bool Break { get; private set; }
        public IEnumerable<string> ErrorList => Errors;
        public bool Successful => ErrorList == null || !ErrorList.Any();

        private List<string> Errors;

        public ValidationResult()
        {
            Errors = new List<string>();
            Break = false;
        }

        public ICircuitBreakerResult AddError(string errorMessage, bool breakAfterAddingError = false)
        {
            Errors.Add(errorMessage);
            Break |= breakAfterAddingError;
            return this;
        }

        public ICircuitBreakerResult AddUnexpectedError(bool breakAfterAddingError = false)
        {
            AddError("Unexpected error occured", breakAfterAddingError);
            return this;
        }

        public ICircuitBreakerResult AddErrorsFrom(IResult otherResult)
        {
            if (otherResult != null && otherResult.ErrorList != null)
                Errors.AddRange(otherResult.ErrorList);
            return this;
        }
    }
}
