using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;


namespace Servis;

public partial class Заявка : Window
{
    public Заявка()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        conn = new MySqlConnection(connStr);
        conn.Open();
        string zaivka = "INSERT INTO заявка(Фамилия, Телефон, Описание_проблемы) VALUES ('" + Фамилия.Text + "', '" + Телефон.Text + "', '" + Описание_проблемы.Text + "');";
        MySqlCommand cmd = new MySqlCommand(zaivka, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    private void GoBack(object? sender, RoutedEventArgs e)
        {
            Servis.Услуга zaivka = new Servis.Услуга();
            this.Close();
            zaivka.Show(); 
        }
}