using System;
using System.Collections.Generic;
using Me20.Common.DTO;
using Me20.Common.Interfaces;
using System.Linq;
using Me20.Common.Extensions;
using Me20.Core.DTO;

namespace Me20.Core.Contents
{
    //TODO: Rename this
    public class SuggestedContentEnquirer : ContentEnquirer, IHaveContentTags
    {
        private readonly new ICollection<ContentEntity> results = new List<ContentEntity>();
        protected override ICollection<ContentEntity> Results => this.results;

        public string[] Tags { get; set; }
        public IEnumerable<string> ContentTags => Tags;

        public SuggestedContentEnquirer() : base()
        {
        }

        public override HttpResult<IEnumerable<ContentEntity>> Execute()
        {
            if (queries.Count < 1)
                return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(400, "Enquirer does not have any queries to execute");

            if (Tags.IsNullOrEmpty())
                return new HttpResult<IEnumerable<ContentEntity>>(null, 204);

            FetchResultsFromQueries();

            return new HttpResult<IEnumerable<ContentEntity>>(CombineResults(), 200);
        }

        private IEnumerable<ContentEntity> CombineResults()
        {
            return Results.ToLookup(ce => ce.SchemalessUriAsMD5(), StringComparer.OrdinalIgnoreCase)
                            .Select(g =>
                            {
                                var averageRatings = g.Select(x => x.AverageRating).Where(ar => ar > 0).ToArray();
                                return new ContentEntity
                                {
                                    Url = g.First().Url,
                                    Rating = g.Select(x => x.Rating).FirstOrDefault(r => r > 0),
                                    AverageRating = averageRatings.Any() ? averageRatings.Average() : 0,
                                    Tags = g.SelectMany(ce => ce.Tags)
                                     .GroupBy(t => t.TagName, StringComparer.OrdinalIgnoreCase)
                                     .Select(gTags => new TagDTO (gTags.Key, gTags.Any(t => t.TaggedByUser)))
                                     .ToList()
                                };
                            });
        }
    }
}
