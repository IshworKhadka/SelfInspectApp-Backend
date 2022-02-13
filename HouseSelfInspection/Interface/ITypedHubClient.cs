using HouseSelfInspection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Interface
{
    public interface ITypedHubClient
    {
        Task BroadCastMessage(Message message);
    }
}
