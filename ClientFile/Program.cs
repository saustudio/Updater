using JSONProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientFile
{
    class Program
    {
       static private string name = "Client File v.0.1";
        static private string getFileMD5(string filename)
        {
            using (MD5 mD = MD5.Create()){
                using (FileStream inputStream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", string.Empty);
                }
            }
        }
        static private void generateClientHash()
        {
            Console.WriteLine("<Files data collection>");
            JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\", "*.*", SearchOption.AllDirectories);
            Console.WriteLine("<Found files: {0}>", files.Length);
            Console.WriteLine("<Getting files information>");
            Console.Title = string.Format("{0} : Directory({1}/{2})", name, 0, files.Length);
            for (int i = 0; i < files.Length; i++)  {
                
                string text = files[i].Replace(Directory.GetCurrentDirectory(), "");
                string pathd = Path.GetDirectoryName(files[i]);
                if (text.IndexOf("ClientFile.exe") == -1 && text.IndexOf("launcher.txt") == -1
                    && text.IndexOf("launcher.ini") == -1 && text.IndexOf("client.json") == -1
                    && text.IndexOf("Dispel.exe") == -1 && text.IndexOf("dispel.exe") == -1
                    && text.IndexOf("launcher.exe") == -1 && text.IndexOf("updater.exe") == -1
                    && text.IndexOf("Launcher.exe") == -1 && text.IndexOf("Updater.exe") == -1
                    && pathd.IndexOf("\\launcher") == -1)
                { 
                    jSONObject.AddField(text, new JSONObject(JSONObject.Type.OBJECT));
                    jSONObject[text].AddField("md5", getFileMD5(files[i]));
                    FileInfo fileInfo = new FileInfo(files[i]);
                    jSONObject[text].AddField("time", fileInfo.LastWriteTimeUtc.ToString("dd.MM.yyyy H:mm:ss"));
                    jSONObject[text].AddField("size", fileInfo.Length.ToString());
                }

                Console.Title = string.Format("{0} : Directory({1}/{2}) > {3}", name, i + 1, files.Length, text);
            }
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\client.json", jSONObject.print(true));
            Console.WriteLine("<All files are collected = \\client.json>");
        }



        static void Main(string[] args) {
            Console.Title = string.Format("{0} : Directory({1}/{2})", name,0,0); 
            generateClientHash();
            Thread.Sleep(5000);
        }
    }
}
