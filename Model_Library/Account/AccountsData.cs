using Model_Library.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Library
{
    public class AccountsData : DataBase<AccountBase>
    {
        public AccountsData(string pathToAccountData) :
            base(pathToAccountData)
        {
           
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
                

    }
}
