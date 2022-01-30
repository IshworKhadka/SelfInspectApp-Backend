using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models.ViewModels
{
    public class TokenRequestVM
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
