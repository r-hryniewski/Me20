using System;
using Me20.Common.Interfaces;
using Me20.Identity.Abstracts;

namespace Me20.Identity.Messages
{
    public class UserLoggedInMessage : AuthenthicationInfoBase, IHaveUserName
    {
        public string AuthenthicationType { get; private set; }

        public UserLoggedInMessage(string authenticationType, string id) : base(authenticationType, id)
        {

        }
    }
}
