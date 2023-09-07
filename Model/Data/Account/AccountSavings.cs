using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Account
{
    public class AccountSavings : AccountBase
    {
        private double _rate;
        public double Rate
        {
            get => _rate;
        }


        public AccountSavings(int id, CurrencyEnum currency, int clientId, double rate) : 
            base(id, currency, clientId, AccountTypeEnum.Savings)
        {
            _rate = rate;
        }
    }
}
