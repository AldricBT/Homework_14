using Homework_14.Infrastructure.Commands;
using Homework_14.View;
using Homework_14.ViewModels.Base;
using Model_Library;
using Model_Library.Account;
using Model_Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_14.ViewModels
{
    internal class NewAccountWindowViewModel : ViewModel
    {
        private readonly NewAccountWindow _newAccountWindow;

        #region SelectedClient. Выбранный клиент
        public Client SelectedClient => _selectedClient;
        private readonly Client _selectedClient; //выбранный клиент     
        #endregion

        #region NewAccountControlsView. Доступные контролы (внесение денег, ставка) в зависимости от типа счёта
        public Visibility NewAccountControlsView
        {
            get => _newAccountControlsView;
            private set => Set(ref _newAccountControlsView, value);
        }
        private Visibility _newAccountControlsView;
        #endregion

        #region NewAccountRateInfo. Ставка накопительного счёта              
        public string NewAccountRateInfo
        {
            get => _newAccountRateInfo;
            private set => Set(ref _newAccountRateInfo, value);
        }
        private string _newAccountRateInfo;
        #endregion

        #region NewAccountMoney. Сумма внесения            
        public string NewAccountMoney
        {
            get => _newAccountMoney;
            set
            {
                Set(ref _newAccountMoney, value);
                if ((int.TryParse(_newAccountMoney, out int money))&&(money > 0))
                    NewAccountRateInfo = $"{GetRate() * 100}%";
                else
                    NewAccountRateInfo = $"";
            }
        }
        private string _newAccountMoney;
        #endregion

        #region NewAccountCurrency. Валюта счёта             
        public AccountBase.CurrencyEnum NewAccountCurrency
        {
            get => _newAccountCurrency;
            set => Set(ref _newAccountCurrency, value);
        }
        private AccountBase.CurrencyEnum _newAccountCurrency;
        #endregion

        #region NewAccountType. Тип счёта             
        public AccountBase.AccountTypeEnum NewAccountType
        {
            get => _newAccountType;
            set => Set(ref _newAccountType, value);
        }
        private AccountBase.AccountTypeEnum _newAccountType;
        #endregion
        

        
        #region NewAccountTypeChangedCommand. Видимость дополнительных контролов при смене типа счёта. В диалоге
        public ICommand NewAccountTypeChangedCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnNewAccountTypeChangedCommandExecuted(object p) //логика команды
        {
            if (_newAccountType == AccountBase.AccountTypeEnum.Накопительный)
                NewAccountControlsView = Visibility.Visible;
            else
                NewAccountControlsView = Visibility.Hidden;
        }
        private bool CanNewAccountTypeChangedCommandExecute(object p) => true; //если команда должна быть доступна всегда, то просто возвращаем true                
        #endregion

        #region AddAccountDialogCommand. Команда добавления нового счёта. В диалоге 
        public ICommand AddAccountDialogCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnAddAccountDialogCommandExecuted(object p) //логика команды
        {
            int newId = StaticMainData.Accounts.GetNewId();

            switch (_newAccountType)
            {
                case AccountBase.AccountTypeEnum.Накопительный:
                    double rate = 0;
                    if (_newAccountCurrency == AccountBase.CurrencyEnum.RUR)
                        rate = GetRate();

                    _selectedClient.OpenNewAccount(new AccountSavings(newId, 
                        Int32.Parse(_newAccountMoney), _newAccountCurrency,
                        _selectedClient.Id, rate));
                    break;
                case AccountBase.AccountTypeEnum.Расчётный:
                    _selectedClient.OpenNewAccount(new AccountPayment(newId, 0,
                        _newAccountCurrency,
                        _selectedClient.Id));
                    break;
                default:
                    break;
            }
            
            StaticMainData.SaveAllData();
            _newAccountWindow.DialogResult = true;
        }
        private bool CanAddAccountDialogCommandExecute(object p)
        {
            if (!((int.TryParse(_newAccountMoney, out int result)) && (result >= 0)))
                return false;
            return true;
        }
        #endregion
        

        

        /// <summary>
        /// Устанавливает накопительную ставку для нового накопительного счёта при внесении определенной суммы
        /// </summary>
        /// <returns>Ставка накопительного счёта</returns>
        private double GetRate()
        {
            double rate;
            if ((int.TryParse(_newAccountMoney, out int result)) && (result < 50000))
                rate = 0.05;
            else if ((int.TryParse(_newAccountMoney, out result)) && (result < 100000))
                rate = 0.07;
            else
                rate = 0.09;

            return rate;
        }
        private void InitializeCommand()
        {
            NewAccountTypeChangedCommand = new RelayCommand(OnNewAccountTypeChangedCommandExecuted, CanNewAccountTypeChangedCommandExecute);            
            AddAccountDialogCommand = new RelayCommand(OnAddAccountDialogCommandExecuted, CanAddAccountDialogCommandExecute);
        }


        public NewAccountWindowViewModel(Client selectedClient, NewAccountWindow newAccountWindow)
        {            
            _newAccountControlsView = Visibility.Hidden;
            _newAccountMoney = "0";
            _selectedClient = selectedClient;
            _newAccountWindow = newAccountWindow;
            InitializeCommand();

            
            _selectedClient.AddAccountLog += (clientId, accountId) =>
            {
                StaticMainData.Log.Add(new LogMessage($"У клиента #{clientId} открыт новый счёт #{accountId}"));
            };

        }
    }
}
