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

public partial class Работа : Window
{
    public Работа()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM ремонт;";
        ShowTable(fullTable);
    }
    private List<Repair> repair;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    public void ShowTable(string sql)
    {
        repair = new List<Repair>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentRepair = new Repair()
            {
                Код = reader.GetInt32("Код"),
                Код_работника = reader.GetInt32("Код_работника"),
                Код_заказа = reader.GetInt32("Код_заказа"),
                Описание = reader.GetString("Описание")
            };
            repair.Add(currentRepair);
        }
        conn.Close();
        RepairGrid.ItemsSource = repair;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Servis.main_client form2 = new Servis.main_client();
        Close();
        form2.Show();
    }
}