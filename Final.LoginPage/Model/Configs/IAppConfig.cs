using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Model.Configs
{
    public interface IAppConfig : IDisposable
    {
        AppConfig GetAppConfig();
    }
}
