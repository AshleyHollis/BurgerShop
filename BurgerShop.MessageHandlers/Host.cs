using BurgerShop.Core;
using BurgerShop.Messages;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BurgerShop.MessageHandlers
{
    public class Host
    {
        private static List<Task> _clientTasks;
        private static Dictionary<string, IQueueClient> _clients;
        private ManualResetEvent _completedEvent;

        private void EnsureAndSubscribeQueue(string queueName)
        {
            if (!_clients.ContainsKey(queueName))
            {
                var client = new Messaging.QueueClient(queueName);
                client.Subscribe(OnMessageReceived);
                _clients.Add(queueName, client);
                Log.Info("--Subscriber ready: " + queueName + " --");
            }
        }

        private static void OnMessageReceived(Message message)
        {
            Log.Info("** Received message type: {0}, from: {1}", message.MessageType, message.TransportedBy);
            try
            {
                var handler = MessageHandlerFactory.GetMessageHandler(message);
                handler.Handle(message);
                Log.Info(string.Format("Handled message with handler type: {0}:", handler.GetType().Name));
            }
            catch (Exception ex)
            {
                Log.Error("Host.OnMessageReceived, handler errored: {0}", ex.Message);
            }
        }

        public void Start(bool keepAlive = true)
        {
            try
            {
                _clients = new Dictionary<string, IQueueClient>();
                _clientTasks = new List<Task>();
                _clientTasks.Add(Task.Factory.StartNew(() => EnsureAndSubscribeQueue(Queue.RecordSongPlay)));
                _clientTasks.Add(Task.Factory.StartNew(() => EnsureAndSubscribeQueue(Queue.ComputeTop10)));
                _clientTasks.Add(Task.Factory.StartNew(() => EnsureAndSubscribeQueue(Queue.GetTop10)));
                Log.Info("--Awaiting messages");
                if (keepAlive)
                {
                    _completedEvent = new ManualResetEvent(false);
                    _completedEvent.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Log.Fatal("--Host.Start failed: {0}", ex);
            }
        }

        public void Stop()
        {
            Log.Info("--Stopped");
            _clientTasks.ForEach(x => x.Dispose());
            _clients = new Dictionary<string, IQueueClient>();
            if (_completedEvent != null)
            {
                _completedEvent.Set();
            }
        }
    }
}
