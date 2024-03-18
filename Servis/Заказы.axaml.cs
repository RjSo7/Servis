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

public partial class Заказы : Window
{
    public Заказы()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM заказ;";
        ShowTable(fullTable);
    }
    private List<Orders> orders;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    public void ShowTable(string sql)
    {
        orders = new List<Orders>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentOrders = new Orders()
            {
                Код = reader.GetInt32("Код"),
                Код_клиента = reader.GetInt32("Код_клиента"),
                Код_услуги = reader.GetInt32("Код_услуги"),
                Дата = reader.GetString("Дата")
            };
            orders.Add(currentOrders);
        }
        conn.Close();
        OrdersGrid.ItemsSource = orders;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Servis.main_client form2 = new Servis.main_client();
        Close();
        form2.Show();
    }
}