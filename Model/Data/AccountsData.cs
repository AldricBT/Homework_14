using Homework_12_notMVVM.Model.Data.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    internal class AccountsData : DataBase<AccountBase>
    {
        public AccountsData(string pathToAccountData) :
            base(pathToAccountData)
        {
            
        }

        public override int GetNewId()
        {
            return Data.Max(c => c.Id) + 1;
        }
    }
}
