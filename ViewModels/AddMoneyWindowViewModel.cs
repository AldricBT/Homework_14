using Homework_12_notMVVM.Infrastructure.Commands;
using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Account;
using Homework_12_notMVVM.View;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Homework_12_notMVVM.ViewModels
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
            StaticMainData.Clients.Data.Where(c => c.Id == _selectedClient.Id).First().AddMoney(_selectedAccount, int.Parse(_addedMoney));
            
            StaticMainData.SaveAllData();
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
