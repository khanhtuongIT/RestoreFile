using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RecoveryThisFile
{
    public class ComputerInfo
    {
       
        public string getUserAccount()
        {
            string username = "";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from Win32_UserDesktop");
            foreach (ManagementObject getUser in MOS.Get())
            {
                username = getUser["Element"].ToString();
            }
            return username;
        }

        public static string GetRamMemory()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            string result = "";
            foreach (ManagementObject item in moc)
            {
                result = Convert.ToString(Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / (1024 * 1024 * 1024), 0)) + "GB";

            }
            return result;
        }
        public static string GetCPUName()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
            {
                result += Convert.ToString(strt["Name"]);
            }
            return result;
        }

        public static string GetProcessprID()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
            {
                result += Convert.ToString(strt["ProcessorID"]);
            }
            return result;
        }

        public static string GetMACAddress()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            string MACAddress = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                if (MACAddress == string.Empty)
                {
                    if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }

            MACAddress = MACAddress.Replace(":", "-");
            return MACAddress;
        }

        public static string GetSerialNumber()
        {
            string Serial = "";
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");

            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                Serial = obj["SerialNumber"].ToString();
            }

            searcher.Dispose();
            return Serial;
        }
    }
}
