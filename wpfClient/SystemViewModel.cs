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
using System.Threading;
using System.Windows.Input;
using wpfClient;
using System.Windows.Threading;
using System.Windows;

namespace wpfClient
{
    class SystemViewModel : INotifyPropertyChanged
    {
        public SystemViewModel()
        {
            var ipaddress = Enumerable.Range(1, 255).ToArray();
            NetworkSystems = new ObservableCollection<SysInfo>();
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Refresh = new AsyncCommand(()=>asyncGetNetworkSystems());
            
        }

        public async Task asyncGetNetworkSystems()
        {
            var tempList = new List<SysInfo>();
            NetworkSystems.Clear();
            var task = Task.Factory.StartNew(() => parallelIPScan(tempList));
            await task;
            //foreach (var index in tempList) NetworkSystems.Add(index);
        }

        public delegate void UpdateTextCallback(List<SysInfo> tempList);

        public void parallelIPScan(List<SysInfo> tempList)
        {
            var ipaddress = "http://192.168.0.";
            Parallel.For(1, 255,
                   index => {
                       var retval = getActiveSystemInformation(ipaddress + index);
                       if (retval != null && retval.myDrives.Count != 0)
                       {
                           retval.ipAddress = ipaddress + index;
                           Application.Current.Dispatcher.Invoke(() =>
                           {
                               NetworkSystems.Add(retval);
                           });
                       }
                   });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public IAsyncCommand Refresh { get; set; }

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

    

