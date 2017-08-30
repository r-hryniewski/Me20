using Me20.Common.Interfaces;

namespace Me20.Identity.Interfaces
{
    public interface IHaveAuthenthicationInfo : IHaveUserName
    {
        string Id { get;}
        string AuthenticationType { get; }
        bool IsValid { get; }
    }
}
