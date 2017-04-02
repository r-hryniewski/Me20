using Akka.Event;
using Akka.Persistence;

namespace Me20.Common.Abstracts
{
    public abstract class ReceivePersistentActorBase : ReceivePersistentActor
    {
        protected ILoggingAdapter Logger { get; private set; }

        protected virtual ushort SnapshotInterval => 100;
        private ushort eventCounter = 0;

        protected ReceivePersistentActorBase()
        {
            Logger = Logging.GetLogger(Context);
        }

        protected void HandleSnapshoting<TState>(TState state)
        {
            eventCounter++;
            if (eventCounter >= SnapshotInterval)
            {
                SaveSnapshot(state);
                eventCounter = 0;
            }
        }
    }
}
