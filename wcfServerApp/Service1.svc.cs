using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;
using System.IO;
using sharedObjects;
using System.Runtime.InteropServices;
using System.Net;

namespace wcfServerApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class Service1 : ISystemInformation
    {
        public SysInfo collectDriveInformation()
        {
            var mySystem = new SysInfo();
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
                    if(tempDrive.partitions.Equals("0") && !tempDrive.size.Equals("")) mySystem.myDrives.Add(tempDrive);
                    tempDrive = new Drive();
                }
            }
            return mySystem;
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

        public void GetBenchmarkData(BenchmarkSettings benchmarkSettings)
        {
            StartBenchmark(benchmarkSettings.time, benchmarkSettings.selectedDrive.physicalLoc, benchmarkSettings.packetSize);
        }

        [DllImport(@"../Debug/BenchmarkDLL.dll")]
        static extern void StartBenchmark(int time, string physicalDrive, int packetSize);
    }
}
