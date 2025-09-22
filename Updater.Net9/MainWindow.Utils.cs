using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Updater.Annotations;
using Updater.Properties;
using Updater.UtillsClasses;

namespace Updater
{
    public partial class MainWindow
    {
 
        //Создание ярлыка, закачивает иконку с линка
        private void CreatDesctopShortCut(string name)
        {
            //string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $@"\{name}.lnk";
            //if (File.Exists(shortcutPath)) return;
            //try
            //{

            //    Downdload(UPDATE_URL + "icon.ico", MainDirSavePath + "icon.ico", "icon.ico");
            //}
            //catch (Exception)
            //{
            //    return;
            //}


            //WshShell shell = new WshShell();

            ////путь к ярлыку


            ////создаем объект ярлыка

            //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            ////задаем свойства для ярлыка
            ////описание ярлыка в всплывающей подсказке
            //shortcut.Description = "Lineage II";


            //if (File.Exists(MainDirSavePath + "logo.ico"))
            //    shortcut.IconLocation = MainDirSavePath + "logo.ico";

            ////путь к самой программе

            //shortcut.TargetPath = Assembly.GetExecutingAssembly().Location;

            ////Создаем ярлык

            //shortcut.Save();

        }

        private void ShowStandardBalloon(double proc)
        {
            string title = "Progress";
            string text = string.Format("{0:0}%", proc);
        }
    }
}
