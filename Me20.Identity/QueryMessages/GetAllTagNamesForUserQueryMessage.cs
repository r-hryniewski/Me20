using Me20.Common.Interfaces;
using System.Collections.Generic;

namespace Me20.Identity.QueryMessages
{
    public class GetAllTagNamesForUserQueryMessage : IHaveUserName
    {
        public string UserName { get; private set; }
        public IEnumerable<string> TagNames {get; private set;}
        public GetAllTagNamesForUserQueryMessage(string userName, IEnumerable<string> tagNames = null)
        {
            UserName = userName;
            TagNames = tagNames;
        }
    }
}
