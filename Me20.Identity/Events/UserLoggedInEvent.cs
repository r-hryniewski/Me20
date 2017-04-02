using System;

namespace Me20.Identity.Events
{
    internal class UserLoggedInEvent
    {
        internal DateTime LoginTime { get; private set; }
        internal UserLoggedInEvent(DateTime loginTime)
        {
            LoginTime = loginTime;
        }
    }
}
