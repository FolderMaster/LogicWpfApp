using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

using View.Implementations;
using View.Implementations.DialogServices.MessageBoxes;
using View.Implementations.DialogServices.Windows;
using View.Windows;

using ViewModel;
using ViewModel.Interfaces;

using Model.Logic.Expressions;
using Model.Logic.Operators.PairOperators;
using Model.Logic.Operators.SingleOperators;
using Model.Logic.Variables;
using Model.Calculating;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        private Mutex _mutex;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
            {
                services.AddSingleton<IResourceService, WindowResourceService>();
                services.AddSingleton<IBackgroundWorker, WpfBackgroundWorker>();

                services.AddSingleton((s) => 
                {
                    var result = new Session();

                    var variables = new ObservableCollection<INamedVariable<bool>>()
                    {
                        new NamedBoolVariable("A"),
                        new NamedBoolVariable("B")
                    };
                    var expression = new Expression<bool>();
                    expression.Add(variables[0]);
                    expression.Add(new OrOperator());
                    expression.Add(variables[1]);
                    expression.Add(new AndOperator());
                    expression.Add(variables[0]);
                    expression.Add(new ImplicateOperator());
                    expression.Add(new NotOperator());
                    expression.Add(variables[1]);
                    expression.Add(new XorOperator());
                    expression.Add(variables[0]);
                    expression.Add(new EqualOperator());
                    expression.Add(variables[1]);
                    result.CalculatingOptions = new BoolCalculatingOptions(expression.GetVariables());
                    result.Expression = expression;

                    return result;
                });

                services.AddSingleton<ErrorMessageBoxDialogService>();
                services.AddSingleton<InformationMessageBoxDialogService>();
                services.AddSingleton<QuestionMessageBoxDialogService>();
                services.AddSingleton<WarningMessageBoxDialogService>();

                services.AddSingleton<EditExpressionWindowDialogService>();
                services.AddSingleton<CalculatingOptionWindowDialogService>();

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

            var guid = Assembly.GetExecutingAssembly().GetCustomAttribute<GuidAttribute>().
                Value.ToString();
            var isCreatedNew = false;
            _mutex = new Mutex(true, guid, out isCreatedNew);

            if (!isCreatedNew)
            {
                _host.Services.GetRequiredService<ErrorMessageBoxDialogService>().
                    ShowDialog(_host.Services.GetRequiredService<IResourceService>().
                    GetResource("DoubleLaunchErrorMessage"));
                Current.Shutdown();
                return;
            }

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                _host.Services.GetRequiredService<ErrorMessageBoxDialogService>().
                ShowDialog(e.ExceptionObject);

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

            _mutex.ReleaseMutex();

            base.OnExit(e);
        }
    }
}