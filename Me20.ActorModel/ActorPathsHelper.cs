﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.ActorModel
{
    public static class ActorPathsHelper
    {
        public const string ActorSystemName = "MainSystem";
        public const string UsersManagerActorName = "UsersManager";

        public const string TagsManagerActorName = "TagsManager";
        public const string ContentManagerActorName = "ContentManager";

        public const string ActorPathPrefix = "/user";

        public static string BuildAbsoluteActorPath(params string[] segments) => segments.Aggregate(
            seed: new StringBuilder(ActorPathPrefix),
            func: (sb, segment) => sb.Append($"/{segment}"),
            resultSelector: sb => sb.ToString());
    }
}
