using Me20.Common;
using Me20.Common.Interfaces;

namespace Me20.Identity.QueryMessages
{
    public class GetUserContentQueryMessage : IHaveUserName
    {

        public string UserName { get; private set; }
        public byte Take { get; private set; }
        public int CurrentPage { get; private set; }

        public GetUserContentQueryMessage(string userName, byte take)
        {
            UserName = userName;
            Take = take;
            CurrentPage = 1;
        }

        public GetUserContentQueryMessage(string userName) : this(userName, Constants.ContentPageSize)
        {
        }

        public GetUserContentQueryMessage(string userName, int currentPage) : this(userName)
        {
            CurrentPage = currentPage > 0 ? currentPage : 1;
        }
    }
}
