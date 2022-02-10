using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models.ViewModels
{
    public class ChatUserVM
    {
        public string id { get; set; }
        public string name { get; set; }
        public string connId { get; set; }
        public int roleId { get; set; }

        public ChatUserVM(string _id, string _name, string _connId, int _roleId)
        {
            id = _id;
            name = _name;
            connId = _connId;
            roleId = _roleId;
        }
    }
}
