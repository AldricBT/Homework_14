using Homework_12_notMVVM.Model.Data.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    internal class Client
    {
        private readonly int _id;
        private string _name;
        private List<AccountBase> _accounts;

        public int Id 
        {
            get => _id; 
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public List<AccountBase> Accounts
        {
            get => _accounts;
        }

        public Client(int id, string name)
        {         
            _id = id;
            _name = name;

            //При добавлении клиента автоматически открывает ему счет
            
        }

        Продумать открытие нового счета!
        public void OpenNewAccount(AccountBase.AccountTypeEnum acType, AccountBase.CurrencyEnum currency)
        {
            int newAccontId = StaticMainData.Accounts.GetNewId();
            AccountBase addingAccount;
            switch (acType)
            {
                case AccountBase.AccountTypeEnum.Savings:
                    addingAccount = new AccountSavings(newAccontId, 0, AccountBase.CurrencyEnum.RUR, _id);
                    break;
                default:
                    break;
            }
            
            StaticMainData.Accounts.Add(
                new AccountPayment();
            _accounts = new List<AccountBase>
            {
                StaticMainData.Accounts.Data.Find( a => a.Id == newAccontId)
            };
        }
    }
}
