using Nancy.Security;

namespace Me20.Web.Dashboard
{
    public class DashboardViewModel
    {
        public string CurrentUserName { get; private set; }

        public DashboardViewModel(IUserIdentity currentUser)
        {
            this.CurrentUserName = currentUser.UserName;
        }
    }
}