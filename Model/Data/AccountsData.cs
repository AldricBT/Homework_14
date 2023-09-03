using Homework_12_notMVVM.Model.Data.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    public class AccountsData : DataBase<AccountBase>
    {
        private readonly string _pathToAccountData;
        public AccountsData(string pathToAccountData) :
            base(pathToAccountData)
        {
            _pathToAccountData = pathToAccountData;
        }
        /// <summary>
        /// Получает уникальный Id нового счета
        /// </summary>
        /// <returns></returns>
        public override int GetNewId()
        {
            if (Data.Count == 0)
                return 1;
            return Data.Max(c => c.Id) + 1;
        }

        public override object Clone() => new AccountsData(_pathToAccountData);

    }
}
