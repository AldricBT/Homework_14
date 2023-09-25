using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Infrastructure
{
    internal class NotValidInputException : Exception
    {
        private readonly string _message = "Введенного номера счёта не существует!";
        public override string Message => _message;
        
    }
}
