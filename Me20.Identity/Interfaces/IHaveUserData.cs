namespace Me20.Identity.Interfaces
{
    public interface IHaveUserData
    {
        string Id { get;}
        string FullName { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Gender { get; }
        string AuthenticationType { get; }
    }
}
