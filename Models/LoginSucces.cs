using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mekashron_Login.Models
{
    public class LoginSucces
    {
        //"lid":"k553-%7Fcuub_vgq*%3C4Cdrpp%5Dlh1_ri%7F37-61j","FTPHost":"lease4.mekashron.com","FTPPort":221}
        public int EntityId { get; set; }
        public int MobileConfirm { get; set; }
        public int CountryID { get; set; }
        public int Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string lid { get; set; }
        public string FTPHost { get; set; }
        public int EmailConfirm { get; set; }
        public int FTPPort { get; set; }
    }
}
