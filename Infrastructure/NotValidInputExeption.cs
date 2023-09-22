using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Infrastructure
{
    internal class NotValidInputExeption : Exception
    {
        private readonly string _message = "Неверный ввод!";
        public override string Message => _message;
        
    }
}
