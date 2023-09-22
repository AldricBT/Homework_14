using Model_Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Library
{
    // Статический класс для хранения данных
    public static class StaticMainData
    {
        private static readonly ClientsData _clients;
        private static readonly AccountsData _accounts;
        private static readonly LogData _log;

        private static readonly string _pathToClientsData = "clients.json";
        private static readonly string _pathToAccountsData= "accounts.json";
        private static readonly string _pathToLogData = "log.json";


        public static ClientsData Clients
        {
            get => _clients;
        }
        public static AccountsData Accounts
        {
            get => _accounts;
        }
        public static LogData Log
        {
            get => _log;
        }
        public static string PathToClientsData
        {
            get => _pathToClientsData;
        }
        public static string PathToAccountsData
        {
            get => _pathToAccountsData;
        }
        public static string PathToLogData
        {
            get => _pathToLogData;
        }

        public static void SaveAllData()
        {
            _accounts.Save();
            _clients.Save();
            _log.Save();
        }

        static StaticMainData()
        {
            _accounts = new AccountsData(_pathToAccountsData);
            _clients = new ClientsData(_pathToClientsData);
            _log = new LogData(_pathToLogData);
        }
    }
}
