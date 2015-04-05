using Microsoft.WindowsAzure.ServiceRuntime;
using BurgerShop.MessageHandlers;
using System.Threading;

namespace BurgerShop.Azure.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private Host _host;

        public override bool OnStart()
        {
            _host = new Host();
            return base.OnStart();
        }

        public override void Run()
        {
            _host.Start();
            
        }

        public override void OnStop()
        {
            _host.Stop();
            base.OnStop();
        }
    }
}
