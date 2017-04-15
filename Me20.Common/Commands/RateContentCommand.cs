using Me20.Common.Abstracts;
using Me20.Common.Interfaces;
using System;

namespace Me20.Common.Commands
{
    public class RateContentCommand : CommandBase, IHaveUserName
    {
        public Uri ContentUri { get; private set; }
        public string UserName { get; private set; }
        public byte Rating { get; private set; }

        public RateContentCommand(Uri uri, string userName, byte rating) : base()
        {
            ContentUri = uri;
            UserName = userName;
            Rating = rating;
        }
    }
}
