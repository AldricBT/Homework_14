using Homework_12_notMVVM.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Homework_12_notMVVM.View;
using Homework_12_notMVVM.Services;

namespace Homework_12_notMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider _services;

        //public static IServiceProvider Services => _services ?? (_services = InitializeServices().BuildServiceProvider());

        //private static IServiceCollection InitializeServices()
        //{
        //    var services = new ServiceCollection();

        //    services.AddSingleton<MainWindowViewModel>();
        //    services.AddTransient<NewAccountWindow>();


        //    services.AddTransient(
        //        s =>
        //        {
        //            var model = s.GetRequiredService<MainWindowViewModel>();
        //            var window = new MainWindow { DataContext = model };

        //            return window;
        //        });

        //    services.AddTransient(
        //        s =>
        //        {
        //            var model = s.GetRequiredService<MainWindowViewModel>();
        //            var window = new NewAccountWindow { DataContext = model };

        //            return window;
        //        });

        //    return services;
        //}

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            NewAccountWindow accountWindow = new NewAccountWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.NewAccountWindowObject = accountWindow;
            mainWindow.DataContext = viewModel;
            accountWindow.DataContext = viewModel;
            mainWindow.Show();
            //Services.GetRequiredService<IUserDialog>().OpenMainWindow();
        }
    }
}
