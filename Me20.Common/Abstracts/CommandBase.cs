using Me20.Common.Interfaces;
using System;

namespace Me20.Common.Abstracts
{
    public abstract class CommandBase : ICommand
    {
        private readonly DateTime created;
        public DateTime Created => created;

        protected CommandBase()
        {
            created = DateTime.UtcNow;
        }
    }
}
