using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Servis;

public partial class main_client : Window
{
    public main_client()
    {
        InitializeComponent();
    }
    public void Car(object? sender, RoutedEventArgs e)
    {
        Servis.Машины car = new Servis.Машины();
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
        Servis.Услуга services = new Servis.Услуга();
        Close();
        services.Show(); 
    }
    public void Orders(object? sender, RoutedEventArgs e)
    {
        Servis.Заказы orders = new Servis.Заказы();
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