using Me20.Common.Interfaces;
using System;
using System.Collections.Generic;
using Me20.Common.DTO;
using System.Linq;

namespace Me20.Common.Abstracts
{
    public abstract class EnquirerBase<T> : IEnquire<T> where T : IEntity
    {
        public TimeSpan AcceptableTimeout { get; protected set; }
        protected readonly ICollection<IQuery<T>> queries;
        protected virtual ICollection<T> Results { get; set; }

        protected EnquirerBase()
        {
            queries = new List<IQuery<T>>();
            AcceptableTimeout = TimeSpan.FromSeconds(30);
            Results = new HashSet<T>();//TODO: Some kind of comparer?
        }

        public IEnquire<T> WaitFor(TimeSpan timeout)
        {
            AcceptableTimeout = timeout;
            return this;
        }

        public IEnquire<T> QueryFor(IQuery<T> query)
        {
            queries.Add(query);
            return this;
        }

        public IEnquire<T> QueryForAll(IEnumerable<IQuery<T>> queries)
        {
            foreach (var query in queries)
                this.queries.Add(query);
            return this;
        }
        public virtual HttpResult<IEnumerable<T>> Execute()
        {
            if (queries.Count < 1)
                return HttpResult<IEnumerable<T>>.CreateErrorResult(400, "Enquirer does not have any queries to execute");

            //TODO: Parallel and async
            //TODO: Distinct by IEntity Comparer
            foreach (var result in queries.SelectMany(q => q.Execute(this)))
            {
                Results.Add(result);
            }
            if (Results.Any())
                return new HttpResult<IEnumerable<T>>(Results, 200);
            else
                return HttpResult<IEnumerable<T>>.CreateErrorResult(404, "Not found any results");
        }

        public IEnquire<T> QueryForAll(params IQuery<T>[] queries) => QueryForAll(queries.AsEnumerable());
    }
}
