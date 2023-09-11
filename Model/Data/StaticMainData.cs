using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    // Статический класс для хранения данных
    public static class StaticMainData
    {
        private static readonly ClientsData _clients;
        private static readonly AccountsData _accounts;

        private static readonly string _pathToClientsData = "clients.json";
        private static readonly string _pathToAccountsData= "accounts.json";
               

        public static ClientsData Clients
        {
            get => _clients;
        }
        public static AccountsData Accounts
        {
            get => _accounts;
        } 
        public static string PathToClientsData
        {
            get => _pathToClientsData;
        }
        public static string PathToAccountsData
        {
            get => _pathToAccountsData;
        }

        public static void SaveAllData()
        {
            _accounts.Save();
            _clients.Save();
        }

        static StaticMainData()
        {
            _accounts = new AccountsData(_pathToAccountsData);
            _clients = new ClientsData(_pathToClientsData);
            
        }
    }
}
