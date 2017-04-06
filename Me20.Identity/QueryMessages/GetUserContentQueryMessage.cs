using Me20.Common.Interfaces;

namespace Me20.Identity.QueryMessages
{
    public class GetUserContentQueryMessage : IHaveUserName
    {
        public string UserName { get; private set; }
        public GetUserContentQueryMessage(string userName)
        {
            UserName = userName;
        }
    }
}
