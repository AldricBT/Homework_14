using Homework_12_notMVVM.Infrastructure.Commands;
using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Account;
using Homework_12_notMVVM.Model.Workers;
using Homework_12_notMVVM.View;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_12_notMVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {       

        private Client _rememberSelectedClient; //запоминание выбранного клиента при открытии нового счёта

        #region Fields and properties

        

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

        

        #region GetAccountCommand. При выборе клиента показывает его счета
        public ICommand GetAccountCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnGetAccountCommandExecuted(object p) //логика команды
        {
            if (SelectedClient != null)
            {
                ClientAccounts = StaticMainData.Clients.Data.Where(c => c.Id == SelectedClient.Id).First().Accounts;
                _rememberSelectedClient = (Client)SelectedClient.Clone();
            }
        }
        private bool CanGetAccountCommandExecute(object p) => true; //если команда должна быть доступна всегда, то просто возвращаем true                
        #endregion

        #region AddAccountMainCommand. Команда добавления нового счёта в главном окне. для открытия диалога
        public ICommand AddAccountMainCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnAddAccountMainCommandExecuted(object p) //логика команды
        {
            NewAccountWindow _newAccountWindow = new NewAccountWindow();
            NewAccountWindowViewModel _newAccountWindowVM = new NewAccountWindowViewModel(_rememberSelectedClient, _newAccountWindow);
            _newAccountWindow.DataContext = _newAccountWindowVM;
            _newAccountWindow.ShowDialog();
        }
        private bool CanAddAccountMainCommandExecute(object p)
        {
            if (_selectedClient is null)
                return false;
            return true;
        }
        #endregion

        #endregion


        #region Приватные методы VM
        


        /// <summary>
        /// Инициализирует команды
        /// </summary>
        private void InitializeCommand() 
        {            
            GetAccountCommand = new RelayCommand(OnGetAccountCommandExecuted, CanGetAccountCommandExecute);
            AddAccountMainCommand = new RelayCommand(OnAddAccountMainCommandExecuted, CanAddAccountMainCommandExecute);         
        }
        #endregion 

        public MainWindowViewModel()
        {            
            Clients = StaticMainData.Clients.Data;
            InitializeCommand();            
        }
    }
}
