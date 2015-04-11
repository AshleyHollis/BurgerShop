using System;
using BurgerShop.POS.MessageHandlers;

namespace BurgerShop.POS.Server
{
    internal class Program
    {
        private static readonly Host Host = new Host();

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the store number for this store:");

            var storeNo = 0;
            while (storeNo == default(int))
            {
                var storeNoString = Console.ReadLine();
                int.TryParse(storeNoString, out storeNo);
            }

            Host.Start(storeNo);
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("exit");
            Host.Stop();
        }
    }
}