using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Updater.Models;
using Updater.Statics;

namespace Updater.Utils
{
    public static class AccountHandler
    {
        public static void SaveAccount(AccountBase accountBase, bool resave = false)
        {
            var accounts = GetAllAccounts();
            var mirrorAccount = accounts.FirstOrDefault(x => x.Login == accountBase.Login);
            if (resave || mirrorAccount is null)
            {
                accounts.Remove(mirrorAccount);
                accounts.Add(accountBase);
            }

            WriteAllAccounts(accounts);
        }

        public static bool DeleteAccount(AccountBase accountBase)
        {
            var accounts = GetAllAccounts();
            var mirrorAccount = accounts.FirstOrDefault(x => x.Login == accountBase.Login);
            if (mirrorAccount is null)
            {
                return false;
            }

            accounts.Remove(mirrorAccount);
            WriteAllAccounts(accounts);
            return true;
        }

        public static List<AccountBase> GetAllAccounts()
        {
            try
            {
                var json = File.ReadAllText(Paths.PathToFileAccountsJson);
                return JsonConvert.DeserializeObject<List<AccountBase>>(json);
            }
            catch (Exception e)
            {
                return new List<AccountBase>();
            }
        }

        private static void WriteAllAccounts(IEnumerable<AccountBase> accounts)
        {
            var json = JsonConvert.SerializeObject(accounts);
            if (!Directory.Exists(Paths.PathToFolderAccountsJson))
            {
                Directory.CreateDirectory(Paths.PathToFolderAccountsJson);
            }

            File.WriteAllText(Paths.PathToFileAccountsJson, json);
        }
    }
}