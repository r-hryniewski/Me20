using Akka.Actor;
using Akka.Event;

namespace Me20.Common.Abstracts
{
    public abstract class ReceiveActorBase : ReceiveActor
    {
        protected ILoggingAdapter Logger { get; private set; }

        protected ReceiveActorBase()
        {
            Logger = Logging.GetLogger(Context);
        }
    }
}
