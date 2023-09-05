using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Workers
{
    public class PublicData
    {
        private ClientsData _clientsPublic; //данные отображаемые пользователю
        private AccountsData _accountsPublic; //данные отображаемые пользователю

        public ClientsData ClientsPublic
        {
            get => _clientsPublic;
        }

        public AccountsData AccountsPublic
        {
            get => _accountsPublic;
        }

        public PublicData()
        {
            GetClientsPublic();
            GetAccountsPublic();
        }

        protected void GetClientsPublic()
        {
            _clientsPublic = (ClientsData)StaticMainData.Clients.Clone();
        }

        protected void GetAccountsPublic()
        {
            _accountsPublic = (AccountsData)StaticMainData.Accounts.Clone();
        }

        #region Методы для работы с данными
        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            StaticMainData.Clients.Add(client);
            GetClientsPublic();
        }
        /// <summary>
        /// Удаление клиента по id
        /// </summary>
        /// <param name="clientId"></param>
        public void RemoveClient(int clientId) 
        {
            StaticMainData.Clients.RemoveId(clientId); 
            GetClientsPublic();
        }

        public void AddAccount(Client client, AccountBase account)
        {
            client.OpenNewAccount(account);
            GetClientsPublic();
            GetAccountsPublic();
        }

        public void RemoveAccount(Client client, AccountBase account)
        {
            client.RemoveAccount(account);
            GetClientsPublic();
            GetAccountsPublic();
        }
        #endregion

    }
}
