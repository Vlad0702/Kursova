﻿using MyFriends.ViewModels;
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
using System.Windows.Shapes;

namespace MyFriends.Views
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow(PasswordViewModel passwordViewModel)
        {

            InitializeComponent();
            passwordViewModel.CloseHandler += (sender, args) => this.DialogResult = true;
            DataContext = passwordViewModel;
        }

        
    }
}
