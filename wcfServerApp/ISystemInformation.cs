using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using sharedObjects;

namespace wcfServerApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISystemInformation" in both code and config file together.
    [ServiceContract]
    public interface ISystemInformation
    {
        // TODO: Add your service operations here
        [WebGet(UriTemplate = "devices")]
        [OperationContract]
        SysInfo collectDriveInformation();
    }


}
