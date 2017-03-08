using Me20.Identity.Interfaces;

namespace Me20.Identity.Abstracts
{
    public abstract class UserDataBase : IHaveUserName, IHaveUserData
    {
        public bool IsValid => string.IsNullOrEmpty(UserName);

        public string UserName => !string.IsNullOrEmpty(AuthenticationType) && !string.IsNullOrEmpty(Id) ? $"{AuthenticationType}-{Id}" : string.Empty;

        public string Id { get; protected set; } = string.Empty;

        public string FullName { get; protected set; } = string.Empty;
        public string FirstName { get; protected set; } = string.Empty;
        public string LastName { get; protected set; } = string.Empty;

        public string Email { get; protected set; } = string.Empty;

        public string Gender { get; protected set; } = string.Empty;

        public string AuthenticationType { get; protected set; } = string.Empty;

        protected UserDataBase(string id, string fullName, string firstName, string lastName, string email, string gender, string authenticationType) : this()
        {
            this.Id = id;
            this.FullName = fullName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Gender = gender;
            this.AuthenticationType = authenticationType;
        }

        protected UserDataBase(){}
    }
}
