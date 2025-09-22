using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace INI
{
    public class INIParser
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        private string Path;

        public INIParser(string ini)
        {
            Path = ini;
        }

        public bool isFile()
        {
            return File.Exists(Path);
        }

        public string Read(string Section, string Key,string Default)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, Default, RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

    }
}
