using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWork_Library.Log
{
    public class LogData : DataBase<LogMessage>
    {
        
        public LogData(string pathToData) 
            : base(pathToData)
        {
          
        }

        //переопределения метода Add для добавления лога с конца
        public new void Add(LogMessage data)
        {
            Data.Insert(0,data);
            Save();
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
