using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.LoginPage.Model.RabbitMQ
{
    public interface IMessageDispatcher : IDisposable
    {
        bool Enqueue<TMessage>(string queueName, TMessage message);
        bool Enqueue<TMessage>(TMessage message);
        TMessage Dequeue<TMessage>(string queueName);
        TMessage Dequeue<TMessage>();
        IModel GetChannel();
        IModel GetChannel(string queueName);
        IModel DeleteQueue(string queueName);
    }

}
