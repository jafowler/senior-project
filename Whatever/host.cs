using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using wcfServerApp;

namespace Install
{
    public class RestfulHost : ServiceBaseHost
    {
        public RestfulHost() : base()
        {

        }

        public static void Main()
        {
            ServiceBase.Run(new RestfulHost());
        }
    }
}
