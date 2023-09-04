using Homework_12_notMVVM.Model.Data.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    public class Client
    {
        private int _id;
        private string _name;
        private List<AccountBase> _accounts;

        [JsonInclude]
        public int Id 
        {
            get => _id; 
            private set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [JsonInclude]
        public List<AccountBase> Accounts
        {
            get => _accounts;
            private set => _accounts = value;
        }

        public Client(int id, string name)
        {         
            _id = id;
            _name = name;
            _accounts = new List<AccountBase>();
            //При добавлении клиента автоматически открывает ему расчётный счет
            OpenNewAccount(new AccountPayment(StaticMainData.Accounts.GetNewId(),
                AccountBase.CurrencyEnum.RUR, _id));
        }

        [JsonConstructor]
        public Client()
        {
                       
        }

        /// <summary>
        /// Открытие счёта
        /// </summary>
        public void OpenNewAccount(AccountBase account)
        {            
            StaticMainData.Accounts.Add(account);
            //добавление ссылки на счет клиента в данные клиента
            _accounts.Add(account);
        }

        public void RemoveAccount(AccountBase account)
        {
            StaticMainData.Accounts.Remove(account);
            _accounts.Remove(account);
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, numofacc: {Accounts.Count} ";
        }

        ///// <summary>
        ///// Открытие накопительного счёта
        ///// </summary>
        ///// <param name="currency">Валюта счёта</param>
        ///// <param name="rate">Ставка счёта</param>
        //public void OpenNewAccount(AccountBase.CurrencyEnum currency, double rate)
        //{
        //    int newAccontId = StaticMainData.Accounts.GetNewId();
        //    AccountBase addingAccount = new AccountSavings(newAccontId, currency, _id, rate); ;
        //    StaticMainData.Accounts.Add(addingAccount);
        //    //добавление ссылки на счет клиента в данные клиента
        //    _accounts.Add(addingAccount);
        //}

        ///// <summary>
        ///// Открытие расчётного счета
        ///// </summary>
        ///// <param name="currency">Валюта счета</param>
        //public void OpenNewAccount(AccountBase.CurrencyEnum currency)
        //{
        //    int newAccontId = StaticMainData.Accounts.GetNewId();
        //    AccountBase addingAccount = new AccountPayment(newAccontId, currency, _id); ;
        //    StaticMainData.Accounts.Add(addingAccount);
        //    //добавление ссылки на счет клиента в данные клиента
        //    _accounts.Add(addingAccount);
        //}

    }
}
