using Final.LoginPage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Services
{
    public interface IUserService
    {
        public bool Register(tblUser user);
    }
}
