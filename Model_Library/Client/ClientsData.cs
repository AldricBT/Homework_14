using Model_Library.Account;
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

namespace Model_Library
{
    public class ClientsData : DataBase<Client>
    {

        private Action<int, string> _addClientLog;
        public event Action<int, string> AddClientLog
        {
            add
            {
                _addClientLog -= value;
                _addClientLog += value;
            }
            remove
            {
                _addClientLog -= value;
            }
        }

        public new void Add(Client client)
        {
            base.Add(client);
            _addClientLog?.Invoke(client.Id, client.Name);
        }
               

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
