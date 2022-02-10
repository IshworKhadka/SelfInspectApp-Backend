using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseSelfInspection.Models;
using HouseSelfInspection.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HouseSelfInspection.HubConfig
{
    public partial class HouseHub: Hub
    {
        private readonly ApplicationContext ctx;
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public HouseHub(ApplicationContext context,
            UserManager<ApplicationUserModel> userManager,
            IHttpContextAccessor contextAccessor)
        {
            ctx = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var currUserId = ctx.Connections.Where(c => c.SignalrId == Context.ConnectionId).Select(c => c.UserId).SingleOrDefault();
            ctx.Connections.RemoveRange(ctx.Connections.Where(p => p.UserId == currUserId).ToList());
            ctx.SaveChanges();
            Clients.Others.SendAsync("userOff", currUserId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task authMe([FromBody] LoginUserViewModel credentials)
        {
            try
            {

                string currSignalrID = Context.ConnectionId;

                var userExists = await _userManager.FindByEmailAsync(credentials.Email);

                Context.Items.Add("userID", userExists.Id);

                if (userExists != null && userExists.EmailConfirmed && await _userManager.CheckPasswordAsync(userExists, credentials.Password))
                {
                    ConnectionsModel currUser = new ConnectionsModel
                    {
                        UserId = userExists.Id,
                        SignalrId = currSignalrID,
                        Name = userExists.Name,
                        RoleId = userExists.RoleId,
                        TimeStamp = DateTime.Now
                    };
                    await ctx.Connections.AddAsync(currUser);
                    await ctx.SaveChangesAsync();


                    ChatUserVM newUser = new ChatUserVM(userExists.Id, userExists.Name, currSignalrID, userExists.RoleId);
                    await Clients.Caller.SendAsync("authMeResponseSuccess", newUser);
                    await Clients.Others.SendAsync("userOn", newUser);

                }

                else //if credentials are incorrect
                {
                    await Clients.Caller.SendAsync("authMeResponseFail");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task reauthMe(string personId)
        {
            string currSignalrID = Context.ConnectionId;
            var tempPerson = await _userManager.FindByIdAsync(personId);

            if (tempPerson != null) //if credentials are correct
            {
                ConnectionsModel currUser = new ConnectionsModel
                {
                    UserId = tempPerson.Id,
                    SignalrId = currSignalrID,
                    Name = tempPerson.Name,
                    RoleId = tempPerson.RoleId,
                    TimeStamp = DateTime.Now
                };
                await ctx.Connections.AddAsync(currUser);
                await ctx.SaveChangesAsync();

                Console.WriteLine(currSignalrID);
                ChatUserVM newUser = new ChatUserVM(tempPerson.Id, tempPerson.Name, currSignalrID, tempPerson.RoleId);
                await Clients.Caller.SendAsync("reauthMeResponse", newUser);
                await Clients.Others.SendAsync("userOn", newUser);

                Context.Items.Add("userID", tempPerson.Id);

            }
        } //end of reauthMe

        public string GetDataFromSession()
        {
            return Context.Items["userID"].ToString();
        }

        public void logOut(string personId)
        {
            ctx.Connections.RemoveRange(ctx.Connections.Where(p => p.UserId == personId).ToList());
            ctx.SaveChanges();
            Clients.Caller.SendAsync("logoutResponse");
            Clients.Others.SendAsync("userOff", personId);
        }

    }
}
