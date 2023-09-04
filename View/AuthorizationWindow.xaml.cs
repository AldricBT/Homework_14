using Homework_12_notMVVM.Model;
using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework_12_notMVVM
{
    
    public partial class AuthorizationWindow : Window
    {
        private Worker _worker;

        internal Worker Worker
        {
            get => _worker;
        }
        public AuthorizationWindow()
        {            
            InitializeComponent();            
        }

        private void EntryButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ChooseWorker.Text)
            {
                case "Менеджер":
                    _worker = new Manager();
                    break;

                default:
                    _worker = new Consultant();
                    break;
            }

            DataWindow dataWindow = new DataWindow();
            dataWindow.Worker = _worker;
            dataWindow.Show();            
            Close();            
        }
    }
}
