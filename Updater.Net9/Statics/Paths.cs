using System.IO;
using System.Reflection;

namespace Updater.Statics
{
    public static class Paths
    {
        public static string PathToFolderAccountsJson =>
            $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\DispelSettings";
        public static string PathToFileAccountsJson =>
            $"{PathToFolderAccountsJson}\\AccountSettings.json";
    }
}