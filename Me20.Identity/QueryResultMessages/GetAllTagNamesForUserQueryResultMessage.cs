using System.Collections.Generic;

namespace Me20.Identity.QueryResultMessages
{
    public class GetAllTagNamesForUserQueryResultMessage
    {
        public IEnumerable<string> TagNames { get; private set; }
        public GetAllTagNamesForUserQueryResultMessage(IEnumerable<string> tagNames)
        {
            TagNames = tagNames;
        }
    }
}
