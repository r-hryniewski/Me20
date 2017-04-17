using Me20.Common.Abstracts;
using Me20.Common.DTO;
using Me20.Common.Extensions;
using Me20.Common.Interfaces;
using Me20.Core.DTO;
using System;
using System.Collections.Generic;

namespace Me20.Core.Contents
{
    public class ContentEntity : EntityBase<ContentEntity>, IHaveContentUri
    {
        public override string Uid => Uri.ToSchemalessUriAsBase64();
        public string Url { get; set; }

        private Uri uri;
        public Uri Uri
        {
            get
            {
                try
                {
                    return uri ?? (uri = new UriBuilder(Url).Uri);
                }
                catch (Exception)
                {
                    //TODO: Handle exception
                    return null;
                }
            }
        }

        //tagname/tagedByUser
        public ICollection<TagDTO> Tags { get; set; }
        public byte Rating { get; set; }
        public double AverageRating { get; set; }

        public ContentEntity() : base()
        {
            AverageRating = 0;
            Rating = 0;
        }

        public override HttpResult<ContentEntity> DispatchAll(string userName)
        {
            if (Uri != null)
                return base.DispatchAll(userName);
            else
                return HttpResult<ContentEntity>.CreateErrorResult(400, "Provided Uri is invalid and cannot be parsed");
        }
    }
}
