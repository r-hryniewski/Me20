using Akka.Actor;
using Akka.Event;

namespace Me20.Common.Abstracts
{
    public abstract class ReceiveActorBase : ReceiveActor
    {
        protected ILoggingAdapter Log { get; private set; }

        protected ReceiveActorBase()
        {
            Log = Logging.GetLogger(Context);
        }
    }
}
