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

public partial class Клиенты : Window
{
    public Клиенты()
    {
        InitializeComponent();
        string fullTable = "SELECT Код, Фамилия, Имя, Отчество, Код_пол_клиента, Телефон, Код_машина_клиента FROM клиент;";
        ShowTable(fullTable);
    }
    private List<Client> client;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    public void ShowTable(string sql)
    {
        client = new List<Client>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentClient = new Client()
            {
                Код = reader.GetInt32("Код"),
                Фамилия = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Код_пол_клиента = reader.GetInt32("Код_пол_клиента"),
                Телефон = reader.GetString("Телефон"),
                Код_машина_клиента = reader.GetInt32("Код_машина_клиента")
            };
            client.Add(currentClient);
        }
        conn.Close();
        ClientGrid.ItemsSource = client;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Servis.main_client form2 = new Servis.main_client();
        Close();
        form2.Show();
    }
}