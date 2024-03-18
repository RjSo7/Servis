using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using Servis;
using System;

namespace Servis;

public partial class AddUpdZZ : Window
{
    public AddUpdZZ()
    {
        InitializeComponent();
    }
    private List<Orders> Orderss;
    private Orders _currentOrders;
    public AddUpdZZ( Orders currentOrders, List<Orders> orderss)
    {
        InitializeComponent();
        _currentOrders = currentOrders;
        this.DataContext = _currentOrders;
        Orderss = orderss;
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var orderss = Orderss.FirstOrDefault(x => x.Код == _currentOrders.Код);
        if (orderss == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO заказ VALUES (" + Convert.ToInt32(Код.Text)+ ", " + Convert.ToInt32(Код_клиента.Text ) + "," + Convert.ToInt32(Код_услуги.Text ) + ", '" + Дата.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE заказ SET Код_клиента = '" + Convert.ToInt32(Код_клиента.Text) + "',Код_услуги = "+ Convert.ToInt32(Код_услуги.Text) + ", Дата = '" + Дата.Text + "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Servis.ЗаказыZ orderss = new Servis.ЗаказыZ();
        this.Close();
        orderss.Show(); 
    }
}