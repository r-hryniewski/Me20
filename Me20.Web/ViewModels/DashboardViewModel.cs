using Nancy.Security;

namespace Me20.Web.ViewModels
{
    public class DashboardViewModel
    {
        public string CurrentUserName { get; private set; }
        public ushort ContentPageSize => 20;

        public DashboardViewModel(IUserIdentity currentUser)
        {
            this.CurrentUserName = currentUser?.UserName ?? string.Empty;
        }
    }
}