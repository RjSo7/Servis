using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MySql.Data.MySqlClient;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Servis;

public partial class Услуга : Window
{
    public Услуга()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM услуга;";
        ShowTable(fullTable);
    }
    
    
    private List<Services> services;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    
    
    public void ShowTable(string sql)
    {
        
        services = new List<Services>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        
        
        while (reader.Read() && reader.HasRows)
        {
            
            
            var currentServices = new Services()
            {
                Код = reader.GetInt32("Код"),
                Наименование = reader.GetString("Наименование"),
                Время_выполнения = reader.GetString("Время_выполнения"),
                Стоимость_услуги = reader.GetInt32("Стоимость_услуги"),
                Скидка = reader.GetInt32("Скидка"),
                Стоимость_со_скидкой = reader.GetInt32("Стоимость_услуги") - (reader.GetInt32("Стоимость_услуги") * reader.GetInt32("Скидка")/100),
                Фото_услуги = LoadImage("avares://Servis/Assets/" + reader.GetString("Фото_услуги"))
            };  
            
            services.Add(currentServices);
        }
        conn.Close();
        ServicesGrid.ItemsSource = services;
        ServicesGrid.LoadingRow += ServicesGrid_LoadingRow;
    }

    public Bitmap LoadImage(string Uri)
    {
        return new Bitmap(AssetLoader.Open(new Uri(Uri)));
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Servis.main_client form2 = new Servis.main_client();
        Close();
        form2.Show();
    }
    public void Zaivka(object? sender, RoutedEventArgs e)
    {
        Servis.Заявка  zaivka = new Servis.Заявка();
        Close();
        zaivka.Show(); 
    }

    private void ServicesGrid_LoadingRow(object sender, DataGridRowEventArgs e)
    {
        Services services = e.Row.DataContext as Services;
        if (services != null)
        {
            if (0 <= services.Скидка && services.Скидка < 5)
            {
                e.Row.Background = Brushes.DarkGreen;
            }
            else if (5 <= services.Скидка && services.Скидка < 15)
            {
                e.Row.Background = Brushes.Green;
            }
            else if (15 <= services.Скидка && services.Скидка < 30)
            {
                e.Row.Background = Brushes.LimeGreen;
            }
            else if (30 <= services.Скидка && services.Скидка <= 70)
            {
                e.Row.Background = Brushes.PaleGreen;
            }
            else
            {
                e.Row.Background = Brushes.Transparent;
            }
        }
    }
}