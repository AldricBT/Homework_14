using Homework_12_notMVVM.Infrastructure.Commands;
using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Account;
using Homework_12_notMVVM.Model.Workers;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Homework_12_notMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Fields and properties

        #region NewAccountType. Тип счёта             
        public AccountBase.AccountTypeEnum NewAccountType
        {
            get => _newAccountType;
            set => Set(ref _newAccountType, value);
        }
        private AccountBase.AccountTypeEnum _newAccountType;
        #endregion

        #region NewAccountCurrency. Тип счёта             
        public AccountBase.CurrencyEnum NewAccountCurrency
        {
            get => _newAccountCurrency;
            set => Set(ref _newAccountCurrency, value);
        }
        private AccountBase.CurrencyEnum _newAccountCurrency;
        #endregion

        #region Clients. База клиентов
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }
        private ObservableCollection<Client> _clients;
        #endregion
        #region SelectedClient. Выбранный клиент
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value);
        }
        private Client _selectedClient;
        #endregion

        #region ClientAccounts. Счета выбранного клиента
        public ObservableCollection<AccountBase> ClientAccounts
        {
            get => _clientAccounts;
            set => Set(ref _clientAccounts, value);
        }
        private ObservableCollection<AccountBase> _clientAccounts;
        #endregion
        #region SelectedAccount. Выбранный счёт
        public AccountBase SelectedAccount
        {
            get => _selectedAccount;
            set => Set(ref _selectedAccount, value);
        }
        private AccountBase _selectedAccount;
        #endregion   

        #endregion

        #region Commands

        #region GetAccountCommand 
        public ICommand GetAccountCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnGetAccountCommandExecuted(object p) //логика команды
        {
            if (SelectedClient != null)
            {
                ClientAccounts = StaticMainData.Clients.Data.Where(c => c.Id == SelectedClient.Id).First().Accounts;
            }
        }
        private bool CanGetAccountCommandExecute(object p) => true; //если команда должна быть доступна всегда, то просто возвращаем true                
        #endregion

        #region AddAccountCommand 
        public ICommand AddAccountCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnAddAccountCommandExecuted(object p) //логика команды
        {
            SelectedClient.OpenNewAccount(new AccountPayment(StaticMainData.Accounts.GetNewId(),
                AccountBase.CurrencyEnum.RUR,
                SelectedClient.Id));
            StaticMainData.SaveAllData();
        }
        private bool CanAddAccountCommandExecute(object p)
        {
            if (_selectedClient == null)
                return false;
            return true;
        }
        #endregion

        #endregion 



        public void InitializeCommand()
        {
            GetAccountCommand = new RelayCommand(OnGetAccountCommandExecuted, CanGetAccountCommandExecute);
            AddAccountCommand = new RelayCommand(OnAddAccountCommandExecuted, CanAddAccountCommandExecute);
        }
        public MainWindowViewModel()
        {            
            Clients = StaticMainData.Clients.Data;            
            InitializeCommand();            
        }
    }
}
