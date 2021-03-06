﻿using Me20.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me20.ApiGateway.Commands
{
    public class AddContentCommand : IAddContentCommand
    {
        public string Url { get; set; }
        public IEnumerable<string> Tags { get; set; }

        private Uri uri;
        public Uri ContentUri
        {
            get
            {
                try
                {
                    return uri ?? (uri = new UriBuilder(Url).Uri);
                }
                catch (Exception)
                {
                    //TODO: Log exception
                    return null;
                }
            }
        }
    }
}