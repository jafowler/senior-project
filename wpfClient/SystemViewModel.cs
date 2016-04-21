using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sharedObjects;
using System.Net;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;

namespace wpfClient
{
    class SystemViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string m_ipaddress = "192.168.0";
        bool debug = true;
        public SystemViewModel()
        {
            NetworkSystems = new ObservableCollection<SysInfo>();
            #region test structures

            if (debug)
            {
                var tempSys1 = new SysInfo();
                var tempSys2 = new SysInfo();

                var tempDrive1 = new Drive();
                var tempDrive2 = new Drive();

                tempSys1.SystemName = "Temporary_System_1";
                tempSys2.SystemName = "Temporary_System_2";

                tempDrive1.caption = "Temp Drive 1";
                tempDrive1.size = "123456";
                tempDrive2.caption = "Temp Drive 2";
                tempDrive2.size = "789101112";

                tempSys1.myDrives.Add(tempDrive1);
                tempSys2.myDrives.Add(tempDrive2);


                NetworkSystems.Add(tempSys1);
                NetworkSystems.Add(tempSys2);
            }
#endregion
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Refresh = new AsyncCommand(()=>asyncGetNetworkSystems());
            BenchmarkData = new AsyncCommand(()=> asyncSendBenchmarkData());

            
        }

        public async Task asyncSendBenchmarkData()
        {
            var task = Task.Factory.StartNew(() => SendData());
            await task;
        }

        public void SendData()
        {
            _benchmarkSettings.selectedDrive = SelectedDrive;
            _benchmarkSettings.time = 600;
            _benchmarkSettings.packetSize = 131072;
            var physLoc = SelectedDrive.physicalLoc;

            var request = (HttpWebRequest)WebRequest.Create(SelectedSystem.ipAddress + ":8089/ServerApp/listener");
            Trace.WriteLine(SelectedSystem.ipAddress + ":8089/ServerApp/listener");
            request.Method = "POST";
            request.ContentType = "application/json";
            var stream = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            stream.Write("{\"physicalLocation\":\""+ 
                           SelectedDrive.physicalLoc[SelectedDrive.physicalLoc.Length-1]+ 
                           "\",\"time\":\"600\",\"packetSize\":\"131072\"}");
            stream.Close();
            var response = (HttpWebResponse)request.GetResponse();

        }

        public async Task asyncGetNetworkSystems()
        {
            
            var tempList = new List<SysInfo>();
            NetworkSystems.Clear();
            var task = Task.Factory.StartNew(() => parallelIPScan(tempList));
            await task;
        }

        public delegate void UpdateTextCallback(List<SysInfo> tempList);

        public void parallelIPScan(List<SysInfo> tempList)
        {
            Parallel.For(1, 255,
                   index => {
                       var retval = getActiveSystemInformation("http://" + m_ipaddress + "." + index);
                       if (retval != null && retval.myDrives.Count != 0)
                       {
                           retval.ipAddress = "http://"+ m_ipaddress + "." + index;
                           Application.Current.Dispatcher.Invoke(() =>
                           {
                               NetworkSystems.Add(retval);
                           });
                       }
                   });
        }

        public IAsyncCommand Refresh { get; set; }
        public IAsyncCommand BenchmarkData { get; set; }

        public SysInfo getActiveSystemInformation(string ipAddress)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(ipAddress + ":8089/ServerApp/devices");
                //Trace.WriteLine(ipAddress + ":8089");
                request.Method = "GET";
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                var dataSerializer = new DataContractSerializer(typeof(SysInfo));
                var test = (SysInfo)dataSerializer.ReadObject(stream);
                return test;
            }
            catch (Exception e)
            {
                //Trace.WriteLine(e);
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
        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BenchmarkSettings _benchmarkSettings = new BenchmarkSettings();

        public SysInfo _SelectedSystem = new SysInfo();
        public SysInfo SelectedSystem {
            get
            {
                return _SelectedSystem;
            }
            set
            {
                _SelectedSystem = value;
                OnPropertyChanged("SelectedSystem");
            }
        }

        public Drive _SelectedDrive = new Drive();
        public Drive SelectedDrive
        {
            get
            {
                return _SelectedDrive;
            }
            set
            {
                _SelectedDrive = value;
                OnPropertyChanged("SelectedDrive");
            }
        }
    
    }
    
        
}