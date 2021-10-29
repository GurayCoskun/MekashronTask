using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mekashron_Login.Models
{
    public class RegisterResult
    {
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string AffiliateResultMessage { get; set; }
        public int EntityId { get; set; }
        public int AffiliateResultCode { get; set; }
    }
}
