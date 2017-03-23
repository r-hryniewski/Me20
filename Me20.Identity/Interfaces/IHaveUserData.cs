namespace Me20.Identity.Interfaces
{
    public interface IHaveUserData : IHaveAuthenthicationInfo
    {
        string FullName { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Gender { get; }
    }
}
