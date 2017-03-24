using Akka.Actor;
using Akka.Event;

namespace Me20.Common.Abstracts
{
    public abstract class ReceiveActorBase : ReceiveActor
    {
        protected ILoggingAdapter log;

        protected ReceiveActorBase()
        {
            log = Logging.GetLogger(Context);
        }
    }
}
