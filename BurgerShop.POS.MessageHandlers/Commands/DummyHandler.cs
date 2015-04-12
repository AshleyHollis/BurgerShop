using System;
using System.Collections.Generic;
using System.Timers;
using AutoMapper;
using BurgerShop.Core;
using BurgerShop.Core.Models;
using BurgerShop.Messages;
using BurgerShop.Messages.Commands;
using BurgerShop.Messages.Events;
using BurgerShop.Messaging;
using BurgerShop.Messaging.Spec;

namespace BurgerShop.POS.MessageHandlers.Commands
{
    public class DummyHandler : IMessageHandler
    {
        public void Handle(Message message)
        {}
    }
}