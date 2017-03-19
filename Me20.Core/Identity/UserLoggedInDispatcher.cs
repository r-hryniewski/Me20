using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Identity.Messages;

namespace Me20.Core.Identity
{
    public class UserLoggedInDispatcher : DispatcherBase<User>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "UserLoggedIn";

        public UserLoggedInDispatcher() : base()
        { }

        public override void Dispatch(User item, string userName)
        {
            ActorModel.UsersManagerActorRef.Tell(new UserLoggedInMessage(
                    id: item.Id,
                    fullName: item.FullName,
                    firstName: item.FirstName,
                    lastName: item.LastName,
                    email: item.Email,
                    gender: item.Gender,
                    authenticationType: item.AuthenticationType
                    ));
        }
    }
}
