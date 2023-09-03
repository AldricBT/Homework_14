using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Account
{
    public abstract class AccountBase
    {
        public enum CurrencyEnum
        {
            USD,
            EUR,
            RUR
        }
        public enum AccountTypeEnum
        {
            Savings,
            Payment
        }

        private readonly int _id;
        private double _money;
        private readonly CurrencyEnum _currency;
        private readonly int _clientId;

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
        public int ClientId
        {
            get => _clientId;
        }
        

        public AccountBase(int id, CurrencyEnum currency, int clientId)
        {
            _id = id;
            _money = 0;
            _currency = currency;
            _clientId = clientId;
        }

        [JsonConstructor]
        public AccountBase(int id, double money, CurrencyEnum currency, int clientId) //для сериализатора
        {
            _id = id;
            _money = money;
            _currency = currency;
            _clientId = clientId;
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
