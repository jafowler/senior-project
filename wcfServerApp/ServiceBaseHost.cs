using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Web;
using sharedObjects;


namespace wcfServerApp
{
    public partial class ServiceBaseHost : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public ServiceBaseHost()
        {
            ServiceName = "BenchmarkService";
        }

        protected override void OnStart(string[] args)
        {

            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(Service1));

            var behavior = serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.InstanceContextMode = InstanceContextMode.Single;

            serviceHost.Open();

        }

        protected override void OnStop()
        {
            if(serviceHost != null)
           {
               serviceHost.Close();
               serviceHost = null;
           }
        }
    }
}