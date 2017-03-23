using Me20.Common.Interfaces;
using Me20.Identity.Interfaces;

namespace Me20.Identity.Abstracts
{
    public abstract class AuthenthicationInfoBase : IHaveUserName, IHaveAuthenthicationInfo
    {
        public bool IsValid => !string.IsNullOrEmpty(UserName);

        public string UserName => !string.IsNullOrEmpty(AuthenticationType) && !string.IsNullOrEmpty(Id) ? $"{AuthenticationType}-{Id}" : string.Empty;

        public string Id { get; protected set; } = string.Empty;

        public string AuthenticationType { get; protected set; } = string.Empty;

        protected AuthenthicationInfoBase(string authenticationType, string id) : this()
        {
            this.Id = id;
            this.AuthenticationType = authenticationType;
        }

        private AuthenthicationInfoBase(){}
    }
}
