using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.IO;
using sharedObjects;
using System.Diagnostics;

namespace wpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string ipAddress = "http://192.168.0.";
            var networkSystem = new List<SysInfo>();
            for(int i = 1; i < 255; i++)
            {
                var retval = getActiveSystemInformation(ipAddress+i.ToString());
                if(retval == null)
                    continue;
                else
                {
                    networkSystem.Add(retval);
                }
            }
            
            for(int i = 0; i < networkSystem.Count(); i++)
            {
                for (int j = 0; j < networkSystem[i].myDrives.Count(); j++)
                {
                    label.Content += networkSystem[i].myDrives[i].caption;
                }
            }
        }
        public SysInfo getActiveSystemInformation(string ipAddress)
        {
            var request = (HttpWebRequest)WebRequest.Create(ipAddress+":8089/ServerApp/devices");
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
    }
}
