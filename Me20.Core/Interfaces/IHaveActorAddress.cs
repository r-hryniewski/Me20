using Akka.Actor;

namespace Me20.Core.Interfaces
{
    public interface IHaveActorAddress
    {
        ActorSelection Actor { get; }
    }
}
