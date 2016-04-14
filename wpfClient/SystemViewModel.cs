using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharedObjects;
using System.Net;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace wpfClient
{
    class SystemViewModel : INotifyPropertyChanged
    {
        public SystemViewModel()
        {
            //string ipAddress = "http://192.168.0.";
            NetworkSystems = new ObservableCollection<SysInfo>();
            var retval = getActiveSystemInformation("http://localhost");
            if (retval != null) NetworkSystems.Add(retval);
        }

       public event PropertyChangedEventHandler PropertyChanged;

        public SysInfo getActiveSystemInformation(string ipAddress)
        {
            var request = (HttpWebRequest)WebRequest.Create(ipAddress + ":8089/ServerApp/devices");
            Trace.WriteLine(ipAddress + ":8089");
            request.Method = "GET";

            try
            {
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                var dataSerializer = new DataContractSerializer(typeof(SysInfo));
                var test = (SysInfo)dataSerializer.ReadObject(stream);
                return test;
            }
            catch (WebException we)
            {
                Trace.WriteLine(we);
                return null;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return null;
            }
        }

        public ObservableCollection<SysInfo> NetworkSystems
        {
            get; private set;
        }
    }
        
}

    

