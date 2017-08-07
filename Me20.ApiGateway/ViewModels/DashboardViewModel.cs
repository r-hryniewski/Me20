using Me20.Shared;
using Nancy.Security;

namespace Me20.ApiGateway.ViewModels
{
    public class DashboardViewModel
    {
        public string CurrentUserName { get; private set; }
        public byte ContentPageSize => Constants.ContentPageSize;

        public DashboardViewModel(IUserIdentity currentUser)
        {
            this.CurrentUserName = currentUser?.UserName ?? string.Empty;
        }
    }
}