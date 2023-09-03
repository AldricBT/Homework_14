using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    internal static class StaticMainData
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

        static StaticMainData()
        {
            _clients = new ClientsData(_pathToClientsData);
            _accounts = new AccountsData(_pathToAccountsData);
        }
    }
}
