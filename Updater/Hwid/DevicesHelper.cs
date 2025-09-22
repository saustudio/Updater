using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Updater
{
    public class DevicesHelper
    {
       
        public static class FormatHelper
        {
            public static string FormatMacAddress(string macAddress)
            {
                return (macAddress.Length != 12)
                    ? "00:00:00:00:00:00"
                    : Regex.Replace(macAddress, "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})", "$1:$2:$3:$4:$5:$6");
            }

            public static string DriveTypeName(DriveType type)
            {
                switch (type)
                {
                    case DriveType.Fixed:
                        return "Local Disk";
                    case DriveType.Network:
                        return "Network Drive";
                    case DriveType.Removable:
                        return "Removable Drive";
                    default:
                        return type.ToString();
                }
            }


            public static string RemoveEnd(string input)
            {
                if (input.Length > 2)
                    input = input.Remove(input.Length - 2);
                return input;
            }
        }

        public string HardwareId { get; private set; }

        public DevicesHelper()
        {
            HardwareId = MD5Hash(GetCpuName() + GetMainboardIdentifier() + GetBiosIdentifier());
        }


        public string MD5Hash(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] array = mD5CryptoServiceProvider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public string GetName()
        {
            return Environment.UserName;
        }


        public string GetBiosIdentifier()
        {
            //try
            //{
            //    string biosIdentifier = string.Empty;
            //    string query = "SELECT * FROM Win32_BIOS";

            //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            //    {
            //        foreach (ManagementObject mObject in searcher.Get())
            //        {
            //            biosIdentifier = mObject["Manufacturer"].ToString();
            //            break;
            //        }
            //    }

            //    return (!string.IsNullOrEmpty(biosIdentifier)) ? biosIdentifier : "N/A";
            //}
            //catch
            //{
            //}

            //return "Unknown";
            throw new NotImplementedException();
        }

        public  string GetMainboardIdentifier()
        {
            throw new NotImplementedException();
            //try
            //{
            //    string mainboardIdentifier = string.Empty;
            //    string query = "SELECT * FROM Win32_BaseBoard";

            //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            //    {
            //        foreach (ManagementObject mObject in searcher.Get())
            //        {
            //            mainboardIdentifier = mObject["Manufacturer"].ToString() + mObject["SerialNumber"].ToString();
            //            break;
            //        }
            //    }

            //    return (!string.IsNullOrEmpty(mainboardIdentifier)) ? mainboardIdentifier : "N/A";
            //}
            //catch
            //{
            //}

            //return "Unknown";
        }

        public  string GetCpuName()
        {
            throw new NotImplementedException();
            //try
            //{
            //    string cpuName = string.Empty;
            //    string query = "SELECT * FROM Win32_Processor";

            //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            //    {
            //        foreach (ManagementObject mObject in searcher.Get())
            //        {
            //            cpuName += mObject["Name"].ToString() + "; ";
            //        }
            //    }
            //    cpuName = FormatHelper.RemoveEnd(cpuName);

            //    return (!string.IsNullOrEmpty(cpuName)) ? cpuName : "N/A";
            //}
            //catch
            //{
            //}

            //return "Unknown";
        }

        public  int GetTotalRamAmount()
        {
            throw new NotImplementedException();

            //try
            //{
            //    int installedRAM = 0;
            //    string query = "Select * From Win32_ComputerSystem";

            //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            //    {
            //        foreach (ManagementObject mObject in searcher.Get())
            //        {
            //            double bytes = (Convert.ToDouble(mObject["TotalPhysicalMemory"]));
            //            installedRAM = (int)(bytes / 1048576);
            //            break;
            //        }
            //    }

            //    return installedRAM;
            //}
            //catch
            //{
            //    return -1;
            //}
        }

        public  string GetGpuName()
        {
            throw new NotImplementedException();
            //try
            //{
            //    string gpuName = string.Empty;
            //    string query = "SELECT * FROM Win32_DisplayConfiguration";

            //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            //    {
            //        foreach (ManagementObject mObject in searcher.Get())
            //        {
            //            gpuName += mObject["Description"].ToString() + "; ";
            //        }
            //    }
            //    gpuName = FormatHelper.RemoveEnd(gpuName);

            //    return (!string.IsNullOrEmpty(gpuName)) ? gpuName : "N/A";
            //}
            //catch
            //{
            //    return "Unknown";
            //}
        }

        public  string GetLanIp()
        {
            throw new NotImplementedException();
            //foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            //{
            //    GatewayIPAddressInformation gatewayAddress = ni.GetIPProperties().GatewayAddresses.FirstOrDefault();
            //    if (gatewayAddress != null) //exclude virtual physical nic with no default gateway
            //    {
            //        if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
            //            ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
            //            ni.OperationalStatus == OperationalStatus.Up)
            //        {
            //            foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
            //            {
            //                if (ip.Address.AddressFamily != AddressFamily.InterNetwork ||
            //                    ip.AddressPreferredLifetime == UInt32.MaxValue) // exclude virtual network addresses
            //                    continue;

            //                return ip.Address.ToString();
            //            }
            //        }
            //    }
            //}

            //return "-";
        }

        public  string GetMacAddress()
        {
            throw new NotImplementedException();
            //foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            //{
            //    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
            //        ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
            //        ni.OperationalStatus == OperationalStatus.Up)
            //    {
            //        bool foundCorrect = false;
            //        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
            //        {
            //            if (ip.Address.AddressFamily != AddressFamily.InterNetwork ||
            //                ip.AddressPreferredLifetime == UInt32.MaxValue) // exclude virtual network addresses
            //                continue;

            //            foundCorrect = (ip.Address.ToString() == GetLanIp());
            //        }

            //        if (foundCorrect)
            //            return FormatHelper.FormatMacAddress(ni.GetPhysicalAddress().ToString());
            //    }
            //}

            //return "-";
        }

        public  string GetUptime()
        {
            throw new NotImplementedException();
            //try
            //{
            //    string uptime = string.Empty;

            //    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem WHERE Primary='true'"))
            //    {
            //        foreach (ManagementObject mObject in searcher.Get())
            //        {
            //            DateTime lastBootUpTime = ManagementDateTimeConverter.ToDateTime(mObject["LastBootUpTime"].ToString());
            //            TimeSpan uptimeSpan = TimeSpan.FromTicks((DateTime.Now - lastBootUpTime).Ticks);

            //            uptime = string.Format("{0}d : {1}h : {2}m : {3}s", uptimeSpan.Days, uptimeSpan.Hours, uptimeSpan.Minutes, uptimeSpan.Seconds);
            //            break;
            //        }
            //    }

            //    if (string.IsNullOrEmpty(uptime))
            //        throw new Exception("Getting uptime failed");

            //    return uptime;
            //}
            //catch (Exception)
            //{
            //    return string.Format("{0}d : {1}h : {2}m : {3}s", 0, 0, 0, 0);
            //}
        }

        public  string GetPcName()
        {
            return Environment.MachineName;
        }

        public  string GetWindowsName()
        {
            try
            {
                return new Microsoft.VisualBasic.Devices.ComputerInfo().OSFullName;
            }
            catch { }
            return "Unknow OS";
        }

        /*public string GetFull()
        {

            try
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

                var domainName = (!string.IsNullOrEmpty(properties.DomainName)) ? properties.DomainName : "-";
                var hostName = (!string.IsNullOrEmpty(properties.HostName)) ? properties.HostName : "-";

                string full = string.Format(
                    "{0}|" + //HWID
                    "{1}|" + //WINDOWS
                    "{2}|" + //Processor (CPU): 
                    "{3}|" + //Memory (RAM): 
                    "{4}|" + //Video Card (GPU): 
                    "{5}|" + //Username: 
                    "{6}|" + //PC Name: 
                    "{7}|" + //Domain Name: 
                    "{8}|" + //Host Name: 
                    "{9}|" + //Local Directory: 
                    "{10}|" + //System Drive: 
                    "{11}|" + //System Directory: 
                    "{12}|" + //Uptime: 
                    "{13}|" + //MAC Address (CPU): 
                    "{14}",   //LAN IP Address (CPU): 
                    HardwareId,
                    GetWindowsName(),
                    DevicesHelper.GetCpuName(),
                    string.Format("{0} MB", DevicesHelper.GetTotalRamAmount()),
                    DevicesHelper.GetGpuName(),
                    DevicesHelper.GetName(),
                    DevicesHelper.GetPcName(),
                    domainName,
                    hostName,
                    Environment.CurrentDirectory,
                    Path.GetPathRoot(Environment.SystemDirectory),
                    Environment.SystemDirectory,
                    DevicesHelper.GetUptime(),
                    DevicesHelper.GetMacAddress(),
                    DevicesHelper.GetLanIp()
                    );

                return full;
            }
            catch
            {
            }

            return "";
        }*/

    }
}
