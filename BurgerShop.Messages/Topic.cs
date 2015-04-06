﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Messages
{
    public struct Topic
    {
        public const string Top10 = "top10"; //lowercase only for azure
        public const string OrdersCreated = "orderscreated"; //lowercase only for azure
    }
}
