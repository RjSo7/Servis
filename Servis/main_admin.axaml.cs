using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Servis;

public partial class main_admin : Window
{
    public main_admin()
    {
        InitializeComponent();
    }
    public void Car(object? sender, RoutedEventArgs e)
    {
        Servis.МашиныZ car = new Servis.МашиныZ();
        Close();
        car.Show(); 
    }
    public void Client(object? sender, RoutedEventArgs e)
    {
        Servis.Клиенты client = new Servis.Клиенты();
        Close();
        client.Show(); 
    }
    public void Employee(object? sender, RoutedEventArgs e)
    {
        Servis.Сотрудники employee = new Servis.Сотрудники();
        Close();
        employee.Show(); 
    }
    public void Services(object? sender, RoutedEventArgs e)
    {
        Servis.УслугиZ services = new Servis.УслугиZ();
        Close();
        services.Show(); 
    }
    public void Orders(object? sender, RoutedEventArgs e)
    {
        Servis.ЗаказыZ orders = new Servis.ЗаказыZ();
        Close();
        orders.Show(); 
    }
    public void Repair(object? sender, RoutedEventArgs e)
    {
        Servis.Работа repair = new Servis.Работа();
        Close();
        repair.Show(); 
    }
    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

}