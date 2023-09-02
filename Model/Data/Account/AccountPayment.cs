using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Account
{
    internal class AccountPayment : AccountBase
    {
        public AccountPayment(int id, CurrencyEnum currency, int clientId) :
            base(id, currency, clientId)
        {
            
        }
    }
}
