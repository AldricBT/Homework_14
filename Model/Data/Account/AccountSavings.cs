﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Account
{
    internal class AccountSavings : AccountBase
    {
        private double _rate;
        public double Rate
        {
            get => _rate;
        }


        public AccountSavings(int id, double money, CurrencyEnum currency, double rate) : 
            base(id, money, currency)
        {
            _rate = rate;
        }
    }
}
