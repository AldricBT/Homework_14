using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Account
{
    public class AccountPayment : AccountBase
    {
        public AccountPayment(int id, double money, CurrencyEnum currency, int clientId) :
            base(id, money, currency, clientId, AccountTypeEnum.Расчётный)
        {
            
        }
    }
}
