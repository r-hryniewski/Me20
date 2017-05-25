using Akka.Actor;
using Akka.Event;
using Me20.Common.Extensions;

namespace Me20.Common.Abstracts
{
    public abstract class ReceiveActorBase : ReceiveActor
    {
        protected ILoggingAdapter Logger { get; private set; }

        protected ReceiveActorBase()
        {
            Logger = Logging.GetLogger(Context);
        }

        protected virtual string ChildActorPathEncoder(string input) => input.ToLower().ToMD5();
    }
}
