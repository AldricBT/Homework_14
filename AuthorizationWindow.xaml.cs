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
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void EntryButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ChooseWorker.Text)
            {
                case "Менеджер":

                    break;

                default:
                    break;
            }
        }
    }
}
