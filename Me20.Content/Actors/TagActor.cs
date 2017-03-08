using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.Actors
{
    public class TagActor : ReceiveActor
    {
        public TagActor()
        {
            //TODO: Create container for content marked with this tag
            //TODO: Handle receiving added tagged content message
            //
        }
        public static Props Props => Props.Create(() => new TagActor());
    }
}
