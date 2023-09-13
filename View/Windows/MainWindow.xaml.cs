﻿using System;
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
using View.Implementations.DialogServices.MessageBoxes;
using View.Implementations.DialogServices.Windows;

using ViewModel.VMs;

namespace View.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow(AddVariableWindowDialogService addVariableDialog,
            InformationMessageBoxDialogService errorDialog)
        {
            InitializeComponent();

            DataContext = new MainVM(addVariableDialog, errorDialog);
        }
    }
}