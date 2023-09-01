using Homework_12_notMVVM.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Workers
{
    internal abstract class Worker
    {
        private readonly ClientBase _clientsBase;  //истинные данные клиентов из базы данных

        public Worker()
        {
            
        }
    }
}
