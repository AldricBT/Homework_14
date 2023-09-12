using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Account;
using Homework_12_notMVVM.View;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.ViewModels
{
    internal class TransferMoneyWindowViewModel : ViewModel
    {
        private TransferMoneyWindow _window;

        private AccountBase _account;
        private Client _client;

        public TransferMoneyWindowViewModel(AccountBase account, Client client, TransferMoneyWindow window)
        {
            _window = window;
            _account = account;
            _client = client;
        }
    }
}
