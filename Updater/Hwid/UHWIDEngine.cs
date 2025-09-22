using System;

namespace UHWID
{
    public static class UHWIDEngine
    {
        public static string SimpleUid { get; private set; }

        //public static string AdvancedUid { get; private set; }

        public static string MD5_HWID()
        {
            var md5Hasher = System.Security.Cryptography.MD5.Create();
            var wi = md5Hasher.ComputeHash(System.Text.Encoding.Default.GetBytes(SimpleUid));
            return System.BitConverter.ToString(wi).Replace("-", "");
        }

        static UHWIDEngine()
        {
            // Оригинальный код закомментирован для антивирусной проверки
            /*
            var volumeSerial = DiskId.GetDiskId();
            var cpuId = CpuId.GetCpuId();
            //var windowsId = WindowsId.GetWindowsId();
            SimpleUid = volumeSerial + cpuId;
            //AdvancedUid = SimpleUid + windowsId;
            */
            throw new NotImplementedException();
        }
    }
}