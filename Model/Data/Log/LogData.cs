using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data.Log
{
    public class LogData : DataBase<LogMessage>
    {
        
        public LogData(string pathToData) 
            : base(pathToData)
        {
          
        }
        /// <summary>
        /// ID не используется в логах. Выбрасывает исключение
        /// </summary>
        /// <returns></returns>
        public override int GetNewId()
        {
            throw new NotImplementedException();
        }
    }
}
