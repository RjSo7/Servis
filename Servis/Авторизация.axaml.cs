using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Servis;

public partial class Авторизация : Window
{
    
    public Авторизация()
    {
        InitializeComponent();
    }
    
    
    public void Authorization(object? sender, RoutedEventArgs e)
    {
        if (Login.Text == "client" && Password.Text == "client")
        {
            var form2 = new Servis.main_client();
            Hide();
            form2.Show(); 
        }
        else
        {
            Console.Write("Неверный логин или пароль");
            LogErr.IsVisible = true;
        }
        if (Login.Text == "admin" && Password.Text == "admin")
        {
            var form2 = new Servis.main_admin();
            Hide();
            form2.Show(); 
        }
        else
        {
            Console.Write("Неверный логин или пароль");
            LogErr.IsVisible = true;
        }
    }
    
    public void Exit_PR(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
    
}