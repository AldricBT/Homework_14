using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Homework_12_notMVVM.Infrastructure.Commands
{
    internal abstract class Command : ICommand
    {
        /// <summary>
        /// Передаем управление событием классу CommandManager. WPF генерирует CommandManager каждый раз, когда происходит какое-то событие 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; //добавляем обработчик событий
            remove => CommandManager.RequerySuggested -= value; //удаляем обработчик событий
        }

        /// <summary>
        /// Проверяет можно ли выполнить команду, если нет то ложь
        /// </summary>
        /// <param _name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

    }
}
