using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace DataWork_Library.Account
{
    [JsonDerivedType(typeof(AccountPayment), typeDiscriminator: "payments")]
    [JsonDerivedType(typeof(AccountSavings), typeDiscriminator: "savings")]    
    public abstract class AccountBase : INotifyPropertyChanged
    {
        private Action<int, int, int, CurrencyEnum>? _transferMoneyLog;
        public event Action<int, int, int, CurrencyEnum> TransferMoneyLog
        {
            add
            {
                _transferMoneyLog -= value;
                _transferMoneyLog += value;
            }
            remove
            {
                _transferMoneyLog -= value;
            }
        }

        public enum CurrencyEnum
        {
            RUR,
            USD,
            EUR
        }
        public enum AccountTypeEnum
        {              
            Расчётный,
            Накопительный
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
            private set => Set(ref _money, value);
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
            if ((Money + moneyAdded) < 0)
                return;
            Money += moneyAdded;
        }


        // валидность ввода не проверяется
        public void TransferMoney(AccountBase target, int money)
        {
            target.AddMoney(money);
            _money -= money;
            _transferMoneyLog?.Invoke(Id, target.Id, money, Currency);
        }

        #region Реализация INPC
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызов события. Если явно не указывается название свойства, 
        /// то используется имя свойства, в котором происходит вызов
        /// </summary>
        /// <param _name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }        

        /// <summary>
        /// Метод для сеттера свойств
        /// </summary>
        /// <typeparam _name="T"></typeparam>
        /// <param _name="field">Поле во VM</param>
        /// <param _name="value">Значение, записываемое в поле</param>
        /// <param _name="PropertyName">Название обновляемого свойства 
        /// (если вызывается в самом свойстве, то можно не указывать)</param>
        /// <returns>Вызывает событие для изменения интерфейса</returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null!)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        #endregion
    }
}
