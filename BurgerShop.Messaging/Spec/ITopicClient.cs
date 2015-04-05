using System;

namespace BurgerShop.Messaging.Spec
{
    public interface ITopicClient : IClient
    {
        void Publish(Message message);
        string SubscriptionName { get; }
        string TopicName { get; }
        void DeleteSubscription();
    }
}
