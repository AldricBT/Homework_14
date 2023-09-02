using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Account
{
    internal abstract class AccountBase
    {
        public enum CurrencyEnum
        {
            USD,
            EUR,
            RUR
        }

        private int _id;
        private double _money;
        private CurrencyEnum _currency;
        public int Id
        {
            get => _id;
        }
        public double Money
        {
            get => _money;            
        }
        public CurrencyEnum Currency
        {
            get => _currency;
        }
        public AccountBase(int id, double money, CurrencyEnum currency) //параметр money нужен для сериализатора
        {
            _id = id;
            _money = 0;
            _currency = currency;
        }

        /// <summary>
        /// Добавить или снять деньги. Нельзя снять больше, чем на счете
        /// </summary>
        /// <param name="moneyAdded">Добавляемая или снимаемая сумма</param>
        public void AddMoney(double moneyAdded)
        {
            if ((_money + moneyAdded) < 0)
                return;
            _money += moneyAdded;
        }




    }
}
