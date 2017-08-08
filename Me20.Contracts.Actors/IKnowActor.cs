using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Contracts.Actors
{
    public interface IKnowActor<TActor> where TActor : ActorBase
    {
        IActorRef Ref { get; }
    }
}
