using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models.ViewModels
{
    public class AuthResultVM
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public ApplicationUserModel UserDetail { get; set; }
        public string UserId { get; set; }

    }
}
