using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Infrastructure
{
    internal class TransferCurrencyException : Exception
    {
        private readonly string _message = "Выбранный счёт открыт на другую валюту!";
        public override string Message => _message;
    }
}
