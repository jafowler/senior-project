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
    class SystemViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string m_ipaddress = "Please input the starting IP address range";
        public SystemViewModel()
        {
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
            m_ipaddress = "http://" + m_ipaddress + ".";
            Parallel.For(1, 255,
                   index => {
                       var retval = getActiveSystemInformation(m_ipaddress + index);
                       if (retval != null && retval.myDrives.Count != 0)
                       {
                           retval.ipAddress = m_ipaddress + index;
                           Application.Current.Dispatcher.Invoke(() =>
                           {
                               NetworkSystems.Add(retval);
                           });
                       }
                   });
        }

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

        public ObservableCollection<SysInfo> NetworkSystems{ get; private set; }
        public string IPAddressRange {
            get
            {
                return m_ipaddress;
            }
            set
            {
                if(m_ipaddress != value)
                {
                    m_ipaddress = value;
                    OnPropertyChanged("IPAddressRange");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string Error
        {
            get { return "The field cannot be empty!"; }
        }
        public Drive SelectedDrive { get; set; }
        public SysInfo SelectedSysInfo { get; set; }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
        
}

    

