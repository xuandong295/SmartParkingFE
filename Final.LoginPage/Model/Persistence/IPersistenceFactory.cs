using Final.LoginPage.Model.Configs;
using Final.LoginPage.Model.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Model.Persistence
{
    /// <summary>
    /// Defines the persistence factory for getting repository when needed
    /// </summary>
    public interface IPersistenceFactory
    {
        IMessageDispatcher GetMessageDispatcher();
        IAppConfig GetAppConfig();

    }
}
