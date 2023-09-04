using Homework_12_notMVVM.Model.Workers;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.ViewModels
{
    internal class DataWindowViewModel : ViewModel
    {
        #region Fields and properties

        #region Worker
        private Worker _worker;

        /// <summary>
        /// Выбранный работник. В виде текста textblock
        /// </summary>
        public Worker SelectedWorker
        {
            get => _worker;
            set => Set(ref _worker, value);
        }

        #endregion
        #endregion
    }
}
