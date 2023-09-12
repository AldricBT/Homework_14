using Homework_12_notMVVM.Infrastructure.Commands;
using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Account;
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
            ClientAccounts = StaticMainData.Clients.Data.Where(c => c.Id == SelectedClient.Id).First().Accounts; //создает новый список новых объектов, а не ссылок!
            
            _rememberSelectedClient = (Client)SelectedClient.Clone();
            
        }
        private bool CanGetAccountCommandExecute(object p)
        {
            if (SelectedClient != null)
                return true;
            return false;
        }
        
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

        #region CloseAccountCommand. Команда закрытия счёта 
        public ICommand CloseAccountCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnCloseAccountCommandExecuted(object p) //логика команды
        {
            string messageBoxText, caption;
            MessageBoxButton button;
            MessageBoxImage icon;
            MessageBoxResult result;
            if (_selectedAccount.Money > 0)
            {
                messageBoxText = $"Можно закрыть только пустой счёт!";
                caption = $"Неудалось закрыть счёт";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                return;
            }

            messageBoxText = $"Закрыть счёт номер: {_selectedAccount.Id}?";
            caption = $"Закрытие счёта {_selectedAccount.Id}";
            button = MessageBoxButton.YesNo;
            icon = MessageBoxImage.Question;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (MessageBoxResult.No == result)
                return;

            _selectedClient.RemoveAccount(_selectedAccount);            
            StaticMainData.SaveAllData();

            
        }
        private bool CanCloseAccountCommandExecute(object p)
        {
            if (_selectedAccount is null)
                return false;
            return true;
        }
        #endregion

        #region AddMoneyAccountCommand. Команда внесения денег на счёт
        public ICommand AddMoneyAccountCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnAddMoneyAccountCommandExecuted(object p) //логика команды
        {   
            AddMoneyWindow _addMoneyWindow = new AddMoneyWindow();
            AddMoneyWindowViewModel _addMoneyWindowVM = new AddMoneyWindowViewModel(_selectedAccount, _selectedClient, _addMoneyWindow);
            _addMoneyWindow.DataContext = _addMoneyWindowVM;
            _addMoneyWindow.ShowDialog();
            
        }
        private bool CanAddMoneyAccountCommandExecute(object p)
        {
            if (_selectedAccount is null)
                return false;
            return true;
        }
        #endregion

        #region TransferMoneyCommand. Команда внесения денег на счёт
        public ICommand TransferMoneyCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnTransferMoneyCommandExecuted(object p) //логика команды
        {
            TransferMoneyWindow _transferMoneyWindow = new TransferMoneyWindow();
            TransferMoneyWindowViewModel _transferMoneyWindowVM = new TransferMoneyWindowViewModel(_selectedAccount, _selectedClient, _transferMoneyWindow);
            _transferMoneyWindow.DataContext = _transferMoneyWindowVM;
            _transferMoneyWindow.ShowDialog();

        }
        private bool CanTransferMoneyCommandExecute(object p)
        {
            if (!(_selectedAccount is null) && (_selectedAccount.Money > 0))
                return true;
            return false;
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
            CloseAccountCommand = new RelayCommand(OnCloseAccountCommandExecuted, CanCloseAccountCommandExecute);
            AddMoneyAccountCommand = new RelayCommand(OnAddMoneyAccountCommandExecuted, CanAddMoneyAccountCommandExecute);
            TransferMoneyCommand = new RelayCommand(OnTransferMoneyCommandExecuted, CanTransferMoneyCommandExecute);
        }
        #endregion 

        public MainWindowViewModel()
        {            
            Clients = StaticMainData.Clients.Data;
            InitializeCommand();            
        }
    }
}
