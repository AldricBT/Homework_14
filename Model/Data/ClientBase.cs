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
    internal class ClientBase
    {
        private string _pathToData;
        private List<Client> _clients;

        public ClientBase(string pathToData) 
        {
            _pathToData = pathToData;
            _clients = new List<Client>();

            if (File.Exists(_pathToData))
                Load();
            else
            {
                CreateRandomDB(10);
                Save();
            }
        }

        /// <summary>
        /// Выгрузка базы клиентов из файла
        /// </summary>
        private void Load()
        {
            string jsonString = File.ReadAllText(_pathToData);
            if (jsonString != null) 
                _clients = JsonSerializer.Deserialize<List<Client>>(jsonString);            
        }

        /// <summary>
        /// Сохранение базы клиентов в файл
        /// </summary>
        public void Save()
        {
            string jsonString = JsonSerializer.Serialize<List<Client>>(_clients);
            File.WriteAllText(_pathToData, jsonString);
        }

        /// <summary>
        /// Метод создания случайной базы клиентов
        /// </summary>
        /// <param name="numberOfClients"></param>
        public void CreateRandomDB(int numberOfClients)
        {            
            for (int i = 0; i < numberOfClients; i++) 
            {
                _clients.Add(new Client(i + 1, $"Имя_{i + 1}"));
            }
        }
    }
}
