using Homework_12_notMVVM.Model.Data.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Clients
{
    internal interface IAddMoney<out T>
    {
        T AddMoney(AccountBase account, int addedMoney);
    }
}
