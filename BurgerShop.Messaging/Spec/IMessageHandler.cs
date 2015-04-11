namespace BurgerShop.Messaging.Spec
{
    public interface IMessageHandler
    {
        void Handle(Message message);
    }
}
