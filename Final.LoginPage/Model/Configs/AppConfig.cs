using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Model.Configs
{
    public class AppConfig : IAppConfig
    {
        public string WPFManageQueue { get; set; }

        public AppConfig()
        {
        }
  
        public AppConfig(string dataCollectorQueue)
        {
            WPFManageQueue = dataCollectorQueue;
        }

        public AppConfig GetAppConfig()
        {
            return new AppConfig(WPFManageQueue);
        }

        public void Dispose()
        {

        }
    }

}
