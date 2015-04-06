using BurgerShop.Core;
using BurgerShop.Messaging.Spec;
using System;
using System.Collections.Generic;

namespace BurgerShop.Messaging
{
    public class TopicClient : ClientBase<ITopicClient>, ITopicClient
    {
        public string TopicName { get; private set; }
        public string SubscriptionName { get; private set; }

        public TopicClient(string topicName) : this(topicName, null) { }

        public TopicClient(string topicName, string subscriptionName)
            : base()
        {
            TopicName = topicName;
            SubscriptionName = subscriptionName;
        }

        public void Publish(Message message)
        {
            ExecutePrimary(x => x.Publish(message), x => ExecuteSecondary(y => y.Publish(message)));
        }

        public void Subscribe(Action<Message> onMessageReceived)
        {
            _onMessageReceived = onMessageReceived;
            Subscribe();
        }

        public void DeleteSubscription()
        {
            ExecutePrimary(x => x.DeleteSubscription());
            ExecuteSecondary(x => x.DeleteSubscription());
        }

        protected override void LoadClientTypes(List<Type> clientTypes)
        {
            clientTypes.Clear();

            var primaryType = Type.GetType(Config.GetSetting("PrimaryTopicClientType"));
            if (primaryType == null) Log.Error("Could not resolve type for PrimaryTopicClientType");
            clientTypes.Add(primaryType);

            var secondaryType = Type.GetType(Config.GetSetting("SecondaryTopicClientType"));
            if (secondaryType == null) Log.Error("Could not resolve type for SecondaryTopicClientType");
            clientTypes.Add(secondaryType);
        }

        protected override ITopicClient CreateInstance(Type type)
        {
            return (ITopicClient)Activator.CreateInstance(type, TopicName, SubscriptionName);
        }
    }
}
