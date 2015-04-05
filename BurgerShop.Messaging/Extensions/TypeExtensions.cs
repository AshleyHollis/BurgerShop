using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Messaging.Extensions
{
    public static class TypeExtensions
    {
        public static string GetMessageType(this Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}
