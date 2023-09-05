using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    public class ClientsData : DataBase<Client>
    {            
        private readonly string _pathToData;        

        /// <summary>
        /// Удаление клиента по id
        /// </summary>
        /// <param name="client"></param>
        public void RemoveId(int id)
        {
            Data.Remove(Data.Where(c => c.Id == id).First());            
            Save();
        }

        /// <summary>
        /// Получает уникальный Id нового клиента
        /// </summary>
        /// <returns></returns>
        public override int GetNewId()
        {
            if (Data.Count == 0)
                return 1;
            return Data.Max(c => c.Id) + 1;
        }

        public ClientsData(string pathToData) :
            base(pathToData)        
        {
            _pathToData = pathToData;
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

        public override object Clone() => new ClientsData(_pathToData);
                
    }
}
