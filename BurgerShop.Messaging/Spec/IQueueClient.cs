namespace BurgerShop.Messaging.Spec
{
    public interface IQueueClient : IClient
    {
        string QueueName { get; }
        void Send(Message message);
        void DeleteQueue();
    }
}
