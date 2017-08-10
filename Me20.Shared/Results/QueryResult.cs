using Me20.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Shared.Results
{
    public class QueryResult<TItem> : IQueryResult<TItem>
    {
        public TItem Item { get; set; }
        public IEnumerable<string> ErrorList => Errors;
        public bool Successful => ErrorList == null || !ErrorList.Any();

        private List<string> Errors;

        public QueryResult()
        {
            Errors = new List<string>();
        }

        public QueryResult<TItem> AddError(string errorMsg)
        {
            Errors.Add(errorMsg);
            return this;
        }

        public QueryResult<TItem> AddUnexpectedError()
        {
            AddError("Unexpected error occured");
            return this;
        }

        public QueryResult<TItem> AddErrorsFrom(IResult otherResult)
        {
            if (otherResult != null && otherResult.ErrorList != null)
                Errors.AddRange(otherResult.ErrorList);
            return this;
        }

        public static bool operator true(QueryResult<TItem> result) => result.Successful;

        public static bool operator false(QueryResult<TItem> result) => !result.Successful;

    }
}
