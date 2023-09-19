using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Data.Log;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.ViewModels
{
    internal class LogWindowViewModel : ViewModel
    {
        #region Fields and properties

        #region LogRecords. База клиентов
        public ObservableCollection<LogMessage> LogRecords
        {
            get => _logRecords;
            set => Set(ref _logRecords, value);
        }
        private ObservableCollection<LogMessage> _logRecords;
        #endregion

        #endregion


        public LogWindowViewModel()
        {
            _logRecords = StaticMainData.Log.Data;
        }
    }
}
