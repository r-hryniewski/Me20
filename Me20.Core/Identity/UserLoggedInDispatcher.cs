using Akka.Actor;
using Me20.Common.Abstracts;
using Me20.Identity.Commands;

namespace Me20.Core.Identity
{
    public class UserLoggedInDispatcher : DispatcherBase<UserEntity>
    {
        //Helper property equals to InternalName in base class
        public static readonly string Name = "UserLoggedIn";

        public UserLoggedInDispatcher() : base()
        { }

        public override void Dispatch(UserEntity item, string userName)
        {
            ActorModel.UsersManagerActorRef.Tell(new UserLoggedInCommand(item.AuthenticationType, item.Id));
        }
    }
}
