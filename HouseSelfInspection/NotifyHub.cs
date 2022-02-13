using HouseSelfInspection.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection
{
    public class NotifyHub: Hub<ITypedHubClient>
    {

    }
}
