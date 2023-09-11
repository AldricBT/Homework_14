using Homework_12_notMVVM.Model.Data.Account;
using Homework_12_notMVVM.ViewModels.Base;
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
    public class Client : ViewModel, ICloneable //чтобы заново не реализовывать INPC
    {
        private int _id;
        private string _name;
        private ObservableCollection<AccountBase> _accounts;

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
        public ObservableCollection<AccountBase> Accounts
        {
            get => _accounts;
            private set => _accounts = value;
        }

        public Client(int id, string name)
        {         
            _id = id;
            _name = name;
            _accounts = new ObservableCollection<AccountBase>();
            //При добавлении клиента автоматически открывает ему расчётный счет
            OpenNewAccount(new AccountPayment(StaticMainData.Accounts.GetNewId(),
                AccountBase.CurrencyEnum.RUR, _id));
        }

        [JsonConstructor]
        public Client(int id, string name, ObservableCollection<AccountBase> accounts)
        {
            _id = id;
            _name = name;
            _accounts = accounts;
        }

        // Эта группа методов реализована таким образом, так как при десериализации баз данных
        // происходит создания двух независимых экземпляров AccountsData и _accounts у клиентов. Связать их ссылками не удается.
        // Все упирается в нелогичность наличия отдельной базы данных счётов (она существует только из-за требований задания)
        /// <summary>
        /// Открытие счёта
        /// </summary>
        public void OpenNewAccount(AccountBase account)
        {            
            StaticMainData.Accounts.Add(account);
            //добавление ссылки на счет клиента в данные клиента
            _accounts.Add(account);
        }

        /// <summary>
        /// Закрытие счёта
        /// </summary>
        /// <param name="account"></param>
        public void RemoveAccount(AccountBase account)
        {
            StaticMainData.Accounts.Remove(account);
            _accounts.Remove(account);
        }

        /// <summary>
        /// Добавление денег на выбранный счёт
        /// </summary>
        /// <param name="account"></param>
        /// <param name="addedMoney"></param>
        public void AddMoney(AccountBase account, int addedMoney)
        {
            StaticMainData.Accounts.Data.Where(a => a.Id == account.Id).First().AddMoney(addedMoney);
            _accounts.Where(a => a.Id == account.Id).First().AddMoney(addedMoney);
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, numofacc: {Accounts.Count} ";
        }

        public object Clone()
        {
            return new Client(this._id, this._name, this._accounts);
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
