using HouseSelfInspection.Interface;
using HouseSelfInspection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public MessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }


        [HttpGet]
        public string Get()
        {
            string retMessage = string.Empty;
            var message = new Message() { Type = "warning", Information = "test messsage " + Guid.NewGuid()};

            try
            {
                _hubContext.Clients.All.BroadCastMessage(message);
                retMessage = "Success";

            }
            catch (Exception ex)
            {
                retMessage = ex.ToString();
            }
            return retMessage;
        }


    }
}
