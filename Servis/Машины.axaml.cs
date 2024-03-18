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

public partial class Машины : Window
{
    public Машины()
    {
        InitializeComponent();
        string fullTable = "SELECT Код, Марка, Модель, Год_выпуска FROM машина;";
        ShowTable(fullTable);
    }
    private List<Car> car;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    public void ShowTable(string sql)
    {
        car = new List<Car>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentСar = new Car()
            {
                Код = reader.GetInt32("Код"),
                Марка = reader.GetString("Марка"),
                Модель = reader.GetString("Модель"),
                Год_выпуска = reader.GetString("Год_выпуска")
            };
            car.Add(currentСar);
        }
        conn.Close();
        CarGrid.ItemsSource = car;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Servis.main_client form2 = new Servis.main_client();
        Close();
        form2.Show();
    }
}