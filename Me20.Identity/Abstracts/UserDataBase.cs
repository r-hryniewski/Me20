using Me20.Identity.Interfaces;

namespace Me20.Identity.Abstracts
{
    public abstract class UserDataBase : AuthenthicationInfoBase, IHaveUserData
    {
        public string FullName { get; protected set; } = string.Empty;
        public string FirstName { get; protected set; } = string.Empty;
        public string LastName { get; protected set; } = string.Empty;

        public string Email { get; protected set; } = string.Empty;

        public string Gender { get; protected set; } = string.Empty;

        protected UserDataBase(string id, string fullName, string firstName, string lastName, string email, string gender, string authenticationType) : base(authenticationType, id)
        {
            this.FullName = fullName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Gender = gender;
        }

        protected UserDataBase(string authenthicationType, string id) : base(authenthicationType, id)
        {
        }
    }
}
