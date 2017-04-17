using System;
using System.Collections.Generic;
using Me20.Common.DTO;
using Me20.Common.Interfaces;
using System.Linq;
using Me20.Common.Extensions;
using Me20.Core.DTO;

namespace Me20.Core.Contents
{
    public class ContentDetailsEnquirer : ContentEnquirer, IHaveContentUri
    {
        public Uri Uri { get; set; }

        private new ICollection<ContentEntity> results = new List<ContentEntity>();
        protected override ICollection<ContentEntity> Results => this.results;
        public ContentDetailsEnquirer() : base()
        {
        }

        public override HttpResult<IEnumerable<ContentEntity>> Execute()
        {
            if (queries.Count < 1)
                return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(400, "Enquirer does not have any queries to execute");

            if (Uri == null)
                return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(400, "ContentDetailsEnquirer does not have Uri and therefore cannot return details for any Content");

            FetchResultsFromQueries();

            if (!Results.Any())
                return HttpResult<IEnumerable<ContentEntity>>.CreateErrorResult(204, $"No details for Uri {Uri}");

            return new HttpResult<IEnumerable<ContentEntity>>(CombineResults(), 200);
        }

        private IEnumerable<ContentEntity> CombineResults()
        {
            return Results.GroupBy(ce => ce.SchemalessUriAsBase64(), StringComparer.OrdinalIgnoreCase)
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
                                     .Select(gTags => new TagDTO (gTags.Key, gTags.Any(t => t.TagedByUser)))
                                     .ToList()
                                };
                            });
        }
    }
}
