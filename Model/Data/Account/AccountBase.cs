using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Account
{
    [JsonDerivedType(typeof(AccountPayment), typeDiscriminator: "payments")]
    [JsonDerivedType(typeof(AccountSavings), typeDiscriminator: "savings")]    
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
            [Description("Накопительный")]
            Savings,
            [Description("Расчётный")]
            Payment
        }

        private readonly int _id;
        private double _money;
        private readonly CurrencyEnum _currency;
        private readonly int _clientId;
        private readonly AccountTypeEnum _accountType;

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
        public AccountTypeEnum AccountType
        {
            get => _accountType;
        }

        public AccountBase(int id, CurrencyEnum currency, int clientId, AccountTypeEnum accountType)
        {
            _id = id;
            _money = 0;
            _currency = currency;
            _clientId = clientId;
            _accountType = accountType;
        }

        [JsonConstructor]
        public AccountBase(int id, double money, CurrencyEnum currency, int clientId, AccountTypeEnum accountType) //для сериализатора
        {
            _id = id;
            _money = money;
            _currency = currency;
            _clientId = clientId;
            _accountType = accountType;
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
