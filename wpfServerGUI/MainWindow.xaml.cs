using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;
using System.IO;
using System.Net;

public struct Drive
{
    public string caption { get; set; }
    public string physicalLoc { get; set; }
    public string model { get; set; }
    public string partitions { get; set; }
    public string size { get; set; }
}
public struct SysInfo
{
    public string systemName { get; set; }
    public List<Drive> myDrive { get; set; }
    public string ipAddress { get; set; }
}


namespace wpfServerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SysInfo mySystem;
        string port = "";
        public MainWindow()
        {
            InitializeComponent();
            mySystem.myDrive = new List<Drive>();
            collectDriveInformation();
        }

        private void collectDriveInformation()
        {
            Drive tempDrive = new Drive();
            string line = "";
            //setting up the process to get drive information from the system.
            string strCmdText = "diskdrive list brief /format:list";
            Process p = new Process();
            p.StartInfo.FileName = "wmic.exe";
            p.StartInfo.Arguments = strCmdText;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();

            StreamReader srrOutput = p.StandardOutput;
            while ((line = srrOutput.ReadLine()) != null)
            {
                if (line.Contains("Caption")) tempDrive.caption = parseLine(line);
                else if (line.Contains("DeviceID")) tempDrive.physicalLoc = parseLine(line);
                else if (line.Contains("Model")) tempDrive.model = parseLine(line);
                else if (line.Contains("Partitions")) tempDrive.partitions = parseLine(line);
                else if (line.Contains("Size"))
                {
                    tempDrive.size = parseLine(line);
                    if (tempDrive.partitions == "0") mySystem.myDrive.Add(tempDrive);
                    else tempDrive = new Drive();
                }
            }
         }
        private string parseLine(string line)
        {
            string tempLine = "";
            int pos = line.IndexOf("=");
            for (int i = pos + 1; i < line.Length; i++)
            {
                tempLine = tempLine + line[i];
            }
            return tempLine;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mySystem.systemName = systemNameInput.Text;
            port = portInput.Text;
        }
    }
}