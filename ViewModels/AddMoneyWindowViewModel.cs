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
using System.Windows.Input;

namespace Homework_14.ViewModels
{
    internal class AddMoneyWindowViewModel : ViewModel
    {
        private AccountBase _selectedAccount;
        private Client _selectedClient;
        private AddMoneyWindow _addMoneyWindow;

        #region AccountId. Номер счёта
        public int AccountId => _accountId;
        private readonly int _accountId;
        #endregion

        #region AccountCurrency. Валюта счёта
        public AccountBase.CurrencyEnum AccountCurrency => _accountCurrency;
        private readonly AccountBase.CurrencyEnum _accountCurrency;
        #endregion

        #region AddedMoney. Вносимые деньги 
        public string AddedMoney
        {
            get => _addedMoney;
            set => Set(ref _addedMoney, value);
        } 
        private string _addedMoney = "0";
        #endregion


        #region AddMoneyDialogCommand. Команда добавления нового счёта. В диалоге 
        public ICommand AddMoneyDialogCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnAddMoneyDialogCommandExecuted(object p) //логика команды
        {
            _selectedClient.AddMoneyLog += (clientId, accountId, moneyAdded, currency) =>
            {
                StaticMainData.Log.Add(new LogMessage($"Клиент #{clientId} внес на счёт #{accountId} {moneyAdded} {currency}"));
            };

            StaticMainData.Clients.Data.Where(c => c.Id == _selectedClient.Id).First().AddMoney(_selectedAccount, int.Parse(_addedMoney));            
            StaticMainData.SaveAllData();
            //OnPropertyChanged("ClientAccounts");
            _addMoneyWindow.DialogResult = true;            
        }
        private bool CanAddMoneyDialogCommandExecute(object p)
        {
            if (!((int.TryParse(_addedMoney, out int result)) && (result > 0)))
                return false;
            return true;
        }
        #endregion


        private void InitializeCommand()
        {
            AddMoneyDialogCommand = new RelayCommand(OnAddMoneyDialogCommandExecuted, CanAddMoneyDialogCommandExecute);
        }
        public AddMoneyWindowViewModel(AccountBase selectedAccount, Client selectedClient, AddMoneyWindow addMoneyWindow)            
        {
            _selectedAccount = selectedAccount;
            _selectedClient = selectedClient;
            _addMoneyWindow = addMoneyWindow;
            _accountId = selectedAccount.Id;
            _accountCurrency = selectedAccount.Currency;
            InitializeCommand();
        }
    }
}
