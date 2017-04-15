using Me20.Common.Abstracts;
using Me20.Common.DTO;
using Me20.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Me20.Core.Contents
{
    public class ContentEntity : EntityBase<ContentEntity>
    {
        public override string Uid => Uri.ToActorPathSegment();
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

        public IEnumerable<string> Tags { get; set; }
        public byte Rating { get; set; }

        public ContentEntity() : base()
        {

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
