using Homework_12_notMVVM.Model.Data.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    internal class Client
    {
        private int _id;
        private string _name;
        private ObservableCollection<AccountBase> _accounts;

        public int Id 
        {
            get => _id; 
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Client(int id, string name)
        {         
            _id = id;
            _name = name;
        }
    }
}
