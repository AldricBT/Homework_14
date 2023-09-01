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
    internal class ClientBase //реализовать СЧЕТА!
    {
        private string _pathToData;
        private List<Client> _clients;

        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="client"></param>
        public void Add(Client client)
        {
            _clients.Add(client);
            Save();
        }

        /// <summary>
        /// Удаление клиента по id
        /// </summary>
        /// <param name="client"></param>
        public void Remove(int id)
        {
            _clients.Remove(_clients.Find(c => c.Id == id));            
            Save();
        }

        /// <summary>
        /// Получает уникальный Id нового клиента
        /// </summary>
        /// <returns></returns>
        public int GetNewId()
        {
            return _clients.Max(c => c.Id) + 1;
        }

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
