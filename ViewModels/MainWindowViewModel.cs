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

        #region Clients. База работников
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }
        private ObservableCollection<Client> _clients;
        #endregion
        #region SelectedClient. Выбранный работник
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

        #endregion

        

        public void InitializeCommand()
        {
            GetAccountCommand = new RelayCommand(OnGetAccountCommandExecuted, CanGetAccountCommandExecute);
        }
        public MainWindowViewModel()
        {            
            Clients = StaticMainData.Clients.Data;            
            InitializeCommand();
        }
    }
}
