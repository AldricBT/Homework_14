using Homework_14.ViewModels.Base;
using Model_Library;
using Model_Library.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14.ViewModels
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
