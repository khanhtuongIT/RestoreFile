using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace RecoveryThisFile.RangeTree
{
    [GuidAttribute("9ED54F84-A89D-4fcd-A854-44251E925F09")]
    public class Database
    {
        public static bool CheckData()
        {
            string processorID = ComputerInfo.GetProcessprID();
            bool check = false;
            if(Properties.Settings.Default.Key == Check_Key.CreateLicenseKey(Convert.ToString(Version.RECOVERTHISFILE) + "_" + (int)Version.Version_Key + "_" + Properties.Settings.Default.mail) &&
                Properties.Settings.Default.CPUID == processorID)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }
    }
}
