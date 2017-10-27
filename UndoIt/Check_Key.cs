using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecoveryThisFile
{
    public enum Version
    {
        // app name and lastest version 
        RECOVERTHISFILE,
        Version_Key = 1
    }

    public enum DiskResult
    {
        Yes, 
        None
    }

    public class Check_Key
    {
        public static string version_app = "1.0.0.5";
        private static readonly string[] VNSigns = new string[] { "aAdDeEiIoOuUyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "đ", "Đ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "íìịỉĩ", "ÍÌỊỈĨ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };
        //lọc và chuyển ký tự có dâu thành không dấu

        public static string CreateLicenseKey(string _license_key_info)
        {
            string license_key = "";
            try
            {
                if (_license_key_info != "")
                {
                    _license_key_info = VNStrFilter(_license_key_info);
                    string charnum = "Z1X3V5T7R9P2N4L6J9H7F5D3B1A2C4E6G8I10K12M14O17Q19S00U11W01Y00Z1X3V5T7R9P2N4L6J9H7F5D3B1A26J9H7F5D3B1A2C4E6G8I10K12M14O17Q19S001Y00Z1X3V5T7R9P2N4L6J9H7F5D3B1A26J9H7F5D3B1A2C4D3B1A2C4E6G8I10K12M14O17Q19S00U11W01Y00Z1X3V5T7R9P2N4L6J9H7F5D3B1A26J9H7F5D3B1A2C4E6G8I10K12M14O17Q19S09P2N4L6J9H7F5D3B1A2C4E6G8I10K12M14O17Q19S00U11W01Y00Z1X3V5T7R9P2N4L6J9H7F5D3B1A26J9H7F5D3B1A2C4E6G8I10K12M14O17Q19S001Y00Z1X3V5T7R9P2N4L6J9H7F5D12M14O17Q19S00U11W01Y00Z1X3V5T7R9P2N4L6J9H7F5D3B1A26J9H7F5D3B1A2C4E6G8I10K12M14O";
                    SHA1Managed managed = new SHA1Managed();
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] bytes = encoding.GetBytes(_license_key_info.ToUpper());
                    byte[] arr_byte = encoding.GetBytes(Convert.ToBase64String(managed.ComputeHash(bytes)));
                    Array.Reverse(arr_byte);

                    string byteswap = "";
                    for (int i = 0; i < arr_byte.Length; i++)
                    {
                        byteswap += arr_byte[i];
                    }

                    int distance = 0;
                    for (int index = 0; index < byteswap.Length; index += 2)
                    {
                        license_key += charnum.Substring(int.Parse(byteswap.Substring(index, 2)), 1);

                        distance++;
                        if (distance == 25)
                            break;

                        if ((distance % 5) == 0 && index < (byteswap.Length - 2))
                            license_key += "-";
                    }
                }
            }
            catch (Exception ex)
            {
                using(Dialog dialog = new Dialog(ex.Message, false, true, false, "WARNING"))
                {
                    dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    dialog.ShowDialog();
                    dialog.Dispose();
                }
            }
            return license_key;
        }

        private static string VNStrFilter(string _license_key_info)
        {
            for (int i = 1; i < VNSigns.Length; i++)
            {
                for (int j = 0; j < VNSigns[i].Length; j++)
                {
                    _license_key_info = _license_key_info.Replace(VNSigns[i][j], VNSigns[0][i - 1]);
                }
            }
            return _license_key_info;
        }

    }
}
