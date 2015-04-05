using System;

namespace BurgerShop.Messaging
{
    public interface IClient 
    {
        void Subscribe(Action<Message> onMessageReceived);
        void Unsubscribe();
        bool HasMessages();
        bool IsListening();
    }
}
