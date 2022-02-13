using HouseSelfInspection.Models;
using HouseSelfInspection.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.HubConfig
{
    public partial class HouseHub
    {
        public async Task getOnlineUsers(string ConnectionId)
        {
            string currUserId = ctx.Connections.Where(c => c.SignalrId == ConnectionId).Select(c => c.UserId).SingleOrDefault();
            List<ApplicationUserModel> tempPerson = _userManager.Users.Where(x => x.Id != currUserId).ToList();


            List<ChatUserVM> onlineUsers = ctx.Connections
                .Where(c => c.UserId != currUserId)
                .Select(c =>
                    new ChatUserVM(c.UserId,
                    tempPerson.Where(x => x.Id == c.UserId).Select(p => p.Name).SingleOrDefault(),
                    c.SignalrId,
                    tempPerson.Where(x => x.Id == c.UserId).Select(p => p.RoleId).SingleOrDefault())
                ).ToList();
            await Clients.Caller.SendAsync("getOnlineUsersResponse", onlineUsers);
        }

        public async Task sendMsg(string fromConnId, string connId, string msg)
        {
            var msgData = new
            {
                fromConnId = fromConnId,
                toConnId = connId,
                msg = msg
            };
            await Clients.All.SendAsync("sendMsgResponseCon", msgData);
        }
    }
}
