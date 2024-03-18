using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MySql.Data.MySqlClient;
using Avalonia.Interactivity;

namespace Servis;

public partial class ЗаказыZ : Window
{
    public ЗаказыZ()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM заказ;";
        ShowTable(fullTable);
        FillCmb();
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
        {
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
    }
    public void FillCmb()
    {
        orders = new List<Orders>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM заказ", conn);
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
        var typecmb = this.Find<ComboBox>(name:"CmbNum");
        typecmb.ItemsSource = orders;
    }

    private void TwoSearch_OnClick(object? sender, RoutedEventArgs e)
    {
        string twotxt = "SELECT Код, Код_клиента, Код_услуги, Дата FROM заказ WHERE Код_клиента LIKE '%" + SearchName.Text + "%' AND Код_услуги LIKE '%" + SearchPrice.Text + "%'";
        ShowTable(twotxt);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Servis.main_admin form2 = new Servis.main_admin ();
        Close();
        form2.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        string reset = "SELECT * FROM заказ;";
        ShowTable(reset);
        SearchName.Text = string.Empty;
        SearchPrice.Text = string.Empty;
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var TypeCmB = (ComboBox)sender;
        var currentflTrorders = TypeCmB.SelectedItem as Orders;
        var fltrorders = orders
            .Where(x => x.Код == currentflTrorders.Код)
            .ToList();
        OrdersGrid.ItemsSource = fltrorders;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            Orders currentOrders = OrdersGrid.SelectedItem as Orders;
            if (currentOrders == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM заказ WHERE Код = " + currentOrders.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            orders.Remove(currentOrders);
            ShowTable("SELECT * FROM заказ;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        Orders newOrders = new Orders();
        Servis.AddUpdZZ addWindow = new Servis.AddUpdZZ(newOrders, orders);
        addWindow.Show();
        this.Close();
    }

    private void EditData(object? sender, RoutedEventArgs e)
    {
        Orders currentOrders = OrdersGrid.SelectedItem as Orders;
        if (OrdersGrid == null)
        {
            return;
        }
        Servis.AddUpdZZ editWindow = new Servis.AddUpdZZ(currentOrders, orders);
        editWindow.Show();
        this.Close();
    }
}