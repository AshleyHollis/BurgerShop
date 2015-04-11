using System;
using System.Collections.Generic;
using BurgerShop.Core;
using BurgerShop.Messaging.Spec;

namespace BurgerShop.Messaging
{
    public class QueueClient : ClientBase<IQueueClient>, IQueueClient
    {
        public string QueueName { get; }

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

            var primaryType = Type.GetType(Config.GetSetting("PrimaryQueueClientType"));
            if (primaryType == null) Log.Error("Could not resolve type for PrimaryQueueClientType");
            clientTypes.Add(primaryType);

            var secondaryType = Type.GetType(Config.GetSetting("SecondaryQueueClientType"));
            if (secondaryType == null) Log.Error("Could not resolve type for SecondaryQueueClientType");
            clientTypes.Add(secondaryType);
        }

        protected override IQueueClient CreateInstance(Type type)
        {
            return (IQueueClient)Activator.CreateInstance(type, QueueName);
        }
    }
}
