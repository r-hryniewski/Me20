using Me20.Common.Abstracts;
using Me20.Common.DTO;
using System;
using System.Collections.Generic;

namespace Me20.Core.Contents
{
    public class Content : HaveDispatchersBase<Content>
    {
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

        public Content() : base()
        {

        }

        public override HttpResult<Content> DispatchAll(string userName)
        {
            if (Uri != null)
                return base.DispatchAll(userName);
            else
                return HttpResult<Content>.CreateErrorResult(400, "Provided Uri is invalid and cannot be parsed");
        }
    }
}
