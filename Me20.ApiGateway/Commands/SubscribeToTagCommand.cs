using Me20.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me20.ApiGateway.Commands
{
    public class SubscribeToTagCommand : ICreateTagCommand
    {
        public string TagName { get; set; }
    }
}