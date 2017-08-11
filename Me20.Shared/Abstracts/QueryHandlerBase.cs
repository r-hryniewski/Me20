using Me20.Contracts;
using Me20.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Me20.Shared.Abstracts
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IHandleQueries<TQuery, TResult> where TQuery : IQuery
    {
        protected virtual Action<TQuery> OnBeforeQueryValidation { get; set; }
        protected virtual Action<TQuery, IResult> OnAfterQueryValidation { get; set; }
        protected virtual Action<TQuery, IResult> OnAfterQueryValidationPassed { get; set; }
        protected virtual Action<TQuery, IResult> OnAfterQueryValidationFailed { get; set; }

        protected virtual Action<TQuery> OnBeforeQuerying { get; set; }
        protected virtual Action<TQuery, IQueryResult<TResult>> OnAfterQuerying { get; set; }

        public abstract IEnumerable<Action<TQuery, ValidationResult>> Validators { get; }

        protected QueryHandlerBase()
        {

        }

        public virtual IResult Validate(TQuery item)
        {
            var validationResult = new ValidationResult(); ;
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

        public async Task<IQueryResult<TResult>> Handle(TQuery q, CancellationToken ct = default(CancellationToken))
        {
            var result = new QueryResult<TResult>();
            try
            {
                OnBeforeQueryValidation?.Invoke(q);

                var validationResult = Validate(q);

                OnAfterQueryValidation?.Invoke(q, validationResult);

                if (!validationResult.Successful)
                {
                    OnAfterQueryValidationFailed?.Invoke(q, validationResult);
                    return result.AddErrorsFrom(validationResult);
                }

                OnAfterQueryValidationPassed?.Invoke(q, validationResult);

                OnBeforeQuerying?.Invoke(q);

                result.Item = await Query(q, ct);

                OnAfterQuerying?.Invoke(q, result);
            }
            catch (Exception)
            {
                result.AddUnexpectedError();
            }
            return result;
        }

        public abstract Task<TResult> Query(TQuery q, CancellationToken ct = default(CancellationToken));
    }
}
