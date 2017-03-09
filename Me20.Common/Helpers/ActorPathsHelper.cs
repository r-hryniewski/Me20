using System.Linq;
using System.Text;

namespace Me20.Common.Helpers
{
    public static class ActorPathsHelper
    {
        public const string ActorSystemName = "MainSystem";
        public const string UsersManagerActorName = "UsersManager";
        public const string TagsManagerActorName = "TagssManager";

        public const string ActorPathPrefix = "/user";

        public static string BuildAbsoluteActorPath(params string[] segments) => segments.Aggregate(
            seed: new StringBuilder(ActorPathPrefix),
            func: (sb, segment) => sb.Append($"/segment"),
            resultSelector: sb => sb.ToString());
    }
}
