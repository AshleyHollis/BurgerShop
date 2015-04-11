using System;

namespace BurgerShop.Messaging.Spec
{
    public interface IClient 
    {
        void Subscribe(Action<Message> onMessageReceived);
        void Unsubscribe();
        bool HasMessages();
        bool IsListening();
    }
}
