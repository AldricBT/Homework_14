using Homework_12_notMVVM.Infrastructure.Commands;
using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Log;
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
    internal class AddClientWindowViewModel : ViewModel
    {
        private readonly AddClientWindow _window;

        public string ClientName 
        {             
            set => Set(ref _clientName, value); 
        }
        private string _clientName;



        #region AddClientDialogCommand. Команда добавления клиента
        public ICommand AddClientDialogCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnAddClientDialogCommandExecuted(object p) //логика команды
        {
            Client newClient = new Client(StaticMainData.Clients.GetNewId(), _clientName);

            StaticMainData.Clients.AddClientLog += (newClientId, newCLientName) =>
            {
                StaticMainData.Log.Add(new LogMessage($"Добавлен новый клиент #{newClientId}: {newCLientName}"));
            };

            StaticMainData.Clients.Add(newClient);
            _window.DialogResult = true;            
        }
        private bool CanAddClientDialogCommandExecute(object p)
        {
            if (string.IsNullOrEmpty(_clientName))
                return false;
            return true;
        }

        #endregion



        public AddClientWindowViewModel(AddClientWindow window)
        {
            _window = window;
            AddClientDialogCommand = new RelayCommand(OnAddClientDialogCommandExecuted, CanAddClientDialogCommandExecute);


        }

        
    }
}
