﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;

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
using Model.Parsing;
using Model;

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
                    for (int n = 0; n < 24; n++)
                    {
                        variables.Add(new NamedBoolVariable("V" + n));
                    }
                    result.Variables = variables;

                    var expression = new Expression<bool>();
                    expression.Add(result.Variables[0]);
                    expression.Add(new OrOperator());
                    expression.Add(result.Variables[1]);
                    expression.Add(new AndOperator());
                    expression.Add(result.Variables[0]);
                    expression.Add(new ImplicateOperator());
                    expression.Add(new NotOperator());
                    expression.Add(result.Variables[1]);
                    expression.Add(new XorOperator());
                    expression.Add(result.Variables[0]);
                    expression.Add(new EqualOperator());
                    expression.Add(result.Variables[1]);
                    result.CalculatingOptions = new BoolCalculatingOptions(variables);
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

            var lexemes = new List<ILexeme>()
            {
                new BaseLexeme("0|1|true|false", (value, context) => (value) switch
                {
                    "false" => new BoolLiteral(false),
                    "0" => new BoolLiteral(false),
                    "true" => new BoolLiteral(true),
                    "1" => new BoolLiteral(true),
                    _ => throw new NotImplementedException()
                }),
                new BaseLexeme(@"\w+", (value, context) => {
                    var variable = (INamedVariable<bool>)null;
                    foreach (var element in (IEnumerable<IValue<bool>>)context)
                    {
                        if(element is NamedBoolVariable namedVariable)
                        {
                            if (value == namedVariable.Name)
                            {
                                variable = namedVariable;
                            }
                        }
                        if (variable != null)
                        {
                            break;
                        }
                    }
                    if(variable == null)
                    {
                        return new NamedBoolVariable(value);
                    }
                    else
                    {
                        return variable;
                    }
                }),
                new BaseLexeme(@"\!|\^|\||&|=>|=", (value, context) => (value) switch
                {
                    "!" => new NotOperator(),
                    "^" => new XorOperator(),
                    "|" => new OrOperator(),
                    "&" => new AndOperator(),
                    "=>" => new ImplicateOperator(),
                    "=" => new EqualOperator(),
                    _ => throw new NotImplementedException()
                }),
            };
            var tokenizator = new Tokenizator(lexemes);
            var parser = new Parser();
            var expression = parser.Parse(tokenizator.Parse("a => a = !b & 1"));

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

            AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
                _host.Services.GetRequiredService<ErrorMessageBoxDialogService>().
                ShowDialog(e.Exception.Message);

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