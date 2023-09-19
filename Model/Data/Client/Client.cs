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
        #region Events for Log

        private Action<int, int> _addAccountLog;
        public event Action<int, int> AddAccountLog
        {
            add 
            {
                _addAccountLog -= value;
                _addAccountLog += value;
            }
            remove
            {
                _addAccountLog -= value;
            }
        }
                
        private Action<int, int, int, AccountBase.CurrencyEnum> _addMoneyLog;        
        public event Action<int, int, int, AccountBase.CurrencyEnum> AddMoneyLog
        {
            add
            {
                _addMoneyLog -= value;
                _addMoneyLog += value;
            }
            remove
            {
                _addMoneyLog -= value;
            }
        }

        private Action<int, int> _removeAccountLog;
        public event Action<int, int> RemoveAccountLog
        {
            add 
            { 
                _removeAccountLog -= value;
                _removeAccountLog += value;
            }
            remove
            {
                _removeAccountLog -= value;
            }
        }
        #endregion

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
            OpenNewAccount(new AccountPayment(StaticMainData.Accounts.GetNewId(),0,
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

            _addAccountLog?.Invoke(Id, account.Id);            
        }

        /// <summary>
        /// Закрытие счёта
        /// </summary>
        /// <param name="account"></param>
        public void RemoveAccount(AccountBase account)
        {
            StaticMainData.Accounts.Remove(account);
            _accounts.Remove(account);

            _removeAccountLog?.Invoke(Id, account.Id);
        }

        /// <summary>
        /// Добавление денег на выбранный счёт
        /// </summary>
        /// <param name="account"></param>
        /// <param name="addedMoney"></param>
        public void AddMoney(AccountBase account, int addedMoney)
        {            
            _accounts.Where(a => a.Id == account.Id).First().AddMoney(addedMoney);

            _addMoneyLog?.Invoke(Id, account.Id, addedMoney, account.Currency);
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, numofacc: {Accounts.Count} ";
        }

        public object Clone()
        {
            return new Client(this._id, this._name, this._accounts);
        }
    }
}
