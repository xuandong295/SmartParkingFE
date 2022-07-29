using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Model
{
    public class tblUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string LisencePlateNumber { get; set; }
        public long Balance { get; set; }
    }
}
