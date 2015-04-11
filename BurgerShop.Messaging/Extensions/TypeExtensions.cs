using System;

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
