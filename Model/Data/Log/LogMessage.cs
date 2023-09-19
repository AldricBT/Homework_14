using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Log
{
    public class LogMessage
    {
        private readonly DateTime _date;
        private readonly string _message;

        public DateTime Date { get => _date; }
        public string Message { get => _message; }

        public LogMessage(string message)
        {
            _date = DateTime.Now;
            _message = message;
        }

        [JsonConstructor]
        public LogMessage(DateTime date, string message)
        {
            _date = date;
            _message = message;
        }


    }
}
