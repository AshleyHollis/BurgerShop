using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Configuration;

namespace BurgerShop.Core
{
    public static class Config
    {
        public static string GetSetting(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            try
            {
                if (RoleEnvironment.IsAvailable)
                {
                    value = CloudConfigurationManager.GetSetting(name);
                }
            }
            catch (Exception ex)
            {
                //do nothing - outside of Azure & Emulator using appSettings is correct
            }
            return value;
        }
    }
}
