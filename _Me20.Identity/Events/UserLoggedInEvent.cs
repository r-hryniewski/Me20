using System;

namespace Me20.Identity.Events
{
    public class UserLoggedInEvent
    {
        public DateTime LoginTime { get; private set; }
        public UserLoggedInEvent(DateTime loginTime)
        {
            LoginTime = loginTime;
        }
    }
}
