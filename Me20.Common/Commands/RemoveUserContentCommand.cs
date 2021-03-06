﻿using Me20.Common.Abstracts;
using Me20.Common.Interfaces;
using System;

namespace Me20.Common.Commands
{
    public class RemoveUserContentCommand : CommandBase, IHaveUserName, IHaveContentUri 
    {
        public Uri Uri { get; private set; }
        public string UserName { get; private set; }

        public RemoveUserContentCommand(Uri uri, string userName) : base()
        {
            Uri = uri;
            UserName = userName;
        }
    }
}
