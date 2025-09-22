using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class HelperFile
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string info);

        public static bool DeleteFileZone(string path)
        {
            try {
                return DeleteFile(path + ":Zone.Identifier");
            }
            catch (System.Exception)
            {
            }
            return false;
        }



        public static string stringMD5(string text)
        {
            var md5Hasher = MD5.Create();
            var wi = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            return BitConverter.ToString(wi).Replace("-", "");
        }


        public static string FileMD5(string filename)
        {
            using (MD5 mD = MD5.Create())
            {
                using (FileStream inputStream = File.OpenRead(filename)) {
                    return BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", string.Empty);
                }
            }
        }

    }
}
