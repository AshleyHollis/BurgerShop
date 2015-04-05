using BurgerShop.Core;
using BurgerShop.Messaging.Spec;
using System;
using System.Collections.Generic;

namespace BurgerShop.Messaging
{
    public class QueueClient : ClientBase<IQueueClient>, IQueueClient
    {
        public string QueueName { get; private set; }

        public QueueClient(string queueName) : base()
        {
            QueueName = queueName;
        }

        public void Send(Message message)
        {
            ExecutePrimary(x => x.Send(message), x => ExecuteSecondary(y => y.Send(message)));
        }

        public void Subscribe(Action<Message> onMessageReceived)
        {
            _onMessageReceived = onMessageReceived;
            Subscribe();
        }

        public void DeleteQueue()
        {
            ExecutePrimary(x => x.DeleteQueue());
            ExecuteSecondary(x => x.DeleteQueue());
        }

        protected override void LoadClientTypes(List<Type> clientTypes)
        {
            clientTypes.Clear();
            clientTypes.Add(Type.GetType(Config.GetSetting("PrimaryQueueClientType")));
            clientTypes.Add(Type.GetType(Config.GetSetting("SecondaryQueueClientType")));
        }

        protected override IQueueClient CreateInstance(Type type)
        {
            return (IQueueClient)Activator.CreateInstance(type, new object[] { QueueName });
        }
    }
}
