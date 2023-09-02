using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    internal class ClientsData : DataBase<Client> //реализовать СЧЕТА!
    {                       

        /// <summary>
        /// Удаление клиента по id
        /// </summary>
        /// <param name="client"></param>
        public void RemoveId(int id)
        {
            Data.Remove(Data.Find(c => c.Id == id));            
            Save();
        }

        /// <summary>
        /// Получает уникальный Id нового клиента
        /// </summary>
        /// <returns></returns>
        public override int GetNewId()
        {
            return Data.Max(c => c.Id) + 1;
        }

        public ClientsData(string pathToData) :
            base(pathToData)        
        {
            if (!File.Exists(pathToData))                
            {
                CreateRandomDB(10);
                Save();
            }
        }

        /// <summary>
        /// Метод создания случайной базы клиентов
        /// </summary>
        /// <param name="numberOfClients"></param>
        private void CreateRandomDB(int numberOfClients)
        {            
            for (int i = 0; i < numberOfClients; i++) 
            {
                Data.Add(new Client(i + 1, $"Имя_{i + 1}"));
            }
        }
    }
}
