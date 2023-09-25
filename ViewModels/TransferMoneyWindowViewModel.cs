using Homework_14.Infrastructure.Commands;
using Homework_14.View;
using Homework_14.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Model_Library.Account;
using Model_Library;
using Model_Library.Log;
using Homework_12_notMVVM.Infrastructure;

namespace Homework_14.ViewModels
{
    internal class TransferMoneyWindowViewModel : ViewModel
    {
        private TransferMoneyWindow _window;
        private AccountBase _account;
        private Client _client;

        #region Properties
        #region SourceId
        public int SourceId
        {
            get => _sourceId;
            set => Set(ref _sourceId, value);
        }
        private int _sourceId;
        #endregion

        #region SourceCurrency
        public AccountBase.CurrencyEnum SourceCurrency
        {
            get => _sourceCurrency;
            set => Set(ref _sourceCurrency, value);
        }
        private AccountBase.CurrencyEnum _sourceCurrency;
        #endregion

        #region TransferMoney
        public string TransferMoney
        {
            get => _transferMoney;
            set => Set(ref _transferMoney, value);
        }
        private string _transferMoney = "0";
        #endregion

        #region TargetId. Id счёта, на который выполняется перевод
        public string TargetId
        {
            get => _targetId;
            set => Set(ref _targetId, value);
        }
        private string _targetId;
        #endregion

        #region TextInfo
        public string TextInfo
        {
            get =>_textInfo;
            set => Set(ref _textInfo, value);
        }
        private string _textInfo;
        #endregion

        #endregion 



        #region TransferMoneyDialogCommand. Команда перевода денег
        public ICommand TransferMoneyDialogCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnTransferMoneyDialogCommandExecuted(object p) //логика команды
        {
            if (Transfer())
            {
                _window.DialogResult = true;
            }                
        }
        private bool CanTransferMoneyDialogCommandExecute(object p)
        {
            bool isInputValid = (int.TryParse(_transferMoney, out int money))
                && (int.TryParse(_targetId, out int targetId))
                && (money > 0) && (money <= _account.Money)                
                && (targetId != _sourceId) && (targetId > 0);
            if (isInputValid)
                return true;            
            return false;
        }
        #endregion

        /// <summary>
        /// Метод перевода денег
        /// </summary>
        private bool Transfer()
        {
            // перевод денег на целевой счёт
            Client targetClient;
            AccountBase targetAccount;

            string messageBoxText, caption;
            MessageBoxButton button;
            MessageBoxImage icon;            
            //проверка на существование счёта куда переводят
            try
            {
                bool isSourseIdNotValid = (StaticMainData.Accounts.Data.Where(a => a.Id == int.Parse(_targetId)).Count() == 0);
                if (isSourseIdNotValid)
                    throw new NotValidInputException();

                targetClient = StaticMainData.Clients.Data.Where(c => c.Accounts.Any(a => a.Id == int.Parse(_targetId))).First();
                targetAccount = StaticMainData.Accounts.Data.Where(a => a.Id == int.Parse(_targetId)).First();

                bool isTransferNotValid = (targetAccount.Currency != _sourceCurrency);
                if (isTransferNotValid)
                    throw new TransferCurrencyException();                
            }
            catch (Exception e)
            {
                caption = $"Неудалось выполнить перевод";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Warning;
                MessageBox.Show(e.Message, caption, button, icon, MessageBoxResult.Yes);
                return false;
            }
            

            _account.TransferMoneyLog += (sourceId, targetId, money, currency) =>
            {
                StaticMainData.Log.Add(new LogMessage($"Со счёта #{sourceId} на счёт #{targetId} выполнен перевод на сумму {money} {currency}"));
            };

            _account.TransferMoney(targetAccount, int.Parse(_transferMoney));
            targetClient.AddMoney(targetAccount, int.Parse(_transferMoney));
            //StaticMainData.Clients.TransferMoney(_account, targetAccount, int.Parse(_transferMoney));

            StaticMainData.SaveAllData();

            messageBoxText = $"Перевод выполнен успешно!";
            caption = $"Успешный перевод";
            button = MessageBoxButton.OK;
            icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            return true;
        }


        public TransferMoneyWindowViewModel(AccountBase account, Client client, TransferMoneyWindow window)
        {
            _window = window;
            _account = account;
            _sourceId = account.Id;
            _sourceCurrency = account.Currency;
            _client = client;

            TransferMoneyDialogCommand = new RelayCommand(OnTransferMoneyDialogCommandExecuted, CanTransferMoneyDialogCommandExecute);
        }
    }
}
