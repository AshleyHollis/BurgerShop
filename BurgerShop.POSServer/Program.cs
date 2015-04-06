using BurgerShop.Messages;
using BurgerShop.Messages.Events;
using System;
using System.Linq;

namespace BurgerShop.POSServer
{
    class Program
    {
        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TopicSubscriber.Run(args);

            //string mode;
            //if (args.Length == 0)
            //{
            //    Console.WriteLine("Subscribe to? (_t_opic or _q_ueue)");
            //    mode = Console.ReadLine();
            //}
            //else
            //{
            //    mode = args[0];
            //}
            //switch (mode)
            //{
            //    case "t":
            //        TopicSubscriber.Run(args);
            //        break;
            //    case "q":
            //        QueueSubscriber.Run(args);
            //        break;
            //}
        }
    }
}
