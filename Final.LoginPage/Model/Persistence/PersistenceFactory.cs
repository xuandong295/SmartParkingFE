using Final.LoginPage.Model.Configs;
using Final.LoginPage.Model.RabbitMQ;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Model.Persistence
{
    public class PersistenceFactory : IPersistenceFactory
    {
        private RabbitMqConfiguration rabbitMqConfiguration;

        public ILoggerFactory LoggerFactory { get; set; }
        public RabbitMqConfiguration RabbitConfig { get; set; }
        public AppConfig RabbitMqQueues { get; set; }
        public PersistenceFactory
            (
            ILoggerFactory loggerFactory
            )
        {
            LoggerFactory = loggerFactory;
        }
     

        public PersistenceFactory
    (
    
    ILoggerFactory loggerFactory,
    RabbitMqConfiguration rabbitMqConfiguration
    )
        {
           
            LoggerFactory = loggerFactory;
            RabbitConfig = rabbitMqConfiguration;
        }
        public PersistenceFactory
    (
   
    ILoggerFactory loggerFactory,
    RabbitMqConfiguration rabbitMqConfiguration,
    AppConfig rabbitMqQueues
    )
        {
            
            LoggerFactory = loggerFactory;
            RabbitMqQueues = rabbitMqQueues;
            RabbitConfig = rabbitMqConfiguration;

        }
        public PersistenceFactory()
        {

        }

        
        public IMessageDispatcher GetMessageDispatcher()
        {
            return new RabbitMqDispatcher(RabbitConfig, LoggerFactory);
        }
        public IAppConfig GetAppConfig()
        {
            return RabbitMqQueues;
        }
    }

}
