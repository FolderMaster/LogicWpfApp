using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Windows;

using View.Implementations;
using View.Implementations.DialogServices.MessageBoxes;
using View.Implementations.DialogServices.Windows;
using View.Windows;

using ViewModel.Interfaces;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        private IDialogService _errorDialogService;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
            {
                services.AddSingleton<IResourceService, WindowResourceService>();

                services.AddSingleton<ErrorMessageBoxDialogService>();
                services.AddSingleton<InformationMessageBoxDialogService>();
                services.AddSingleton<QuestionMessageBoxDialogService>();
                services.AddSingleton<WarningMessageBoxDialogService>();

                services.AddSingleton<AddVariableWindowDialogService>();

                services.AddSingleton<MainWindow>();
            }).Build();
        }

        /// <summary>
        /// Метод, выполняющийся при запуске.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _host.Start();

            _errorDialogService = _host.Services.GetRequiredService<ErrorMessageBoxDialogService>();
            AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
                _errorDialogService.ShowDialog(e.Exception.Message);

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }

        /// <summary>
        /// Метод, выполняющийся при выходе.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
