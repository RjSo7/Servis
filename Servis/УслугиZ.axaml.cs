using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MySql.Data.MySqlClient;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Servis;

public partial class УслугиZ : Window
{
    public УслугиZ()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM услуга;";
        ShowTable(fullTable);
        FillCmb();
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
        {
            while (reader.Read() && reader.HasRows)
            {
                var currentServices = new Services()
                {
                    Код = reader.GetInt32("Код"),
                    Наименование = reader.GetString("Наименование"),
                    Время_выполнения = reader.GetString("Время_выполнения"),
                    Стоимость_услуги = reader.GetInt32("Стоимость_услуги"),
                    Скидка = reader.GetInt32("Скидка"),
                    Стоимость_со_скидкой = reader.GetInt32("Стоимость_услуги") -
                                           (reader.GetInt32("Стоимость_услуги") * reader.GetInt32("Скидка") / 100),
                    Фото_услуги = LoadImage("avares://Servis/Assets/" + reader.GetString("Фото_услуги"))
                };
                services.Add(currentServices);
            }

            conn.Close();
            ServicesGrid.ItemsSource = services;
            ServicesGrid.LoadingRow += ServicesGrid_LoadingRow;
        }
    }

    public Bitmap LoadImage(string Uri)
    {
        return new Bitmap(AssetLoader.Open(new Uri(Uri)));
    }
    private void DiscountFilter(object? sender, SelectionChangedEventArgs e)
    {
        var discontComboBox = (ComboBox)sender;
        var selectedDiscount = discontComboBox.SelectedItem as string;
            
        int startDiscount = 0;
        int endDiscount = 0;
            
        if (selectedDiscount == "Все скидки")
        {
            ServicesGrid.ItemsSource = services;
        }
        else if (selectedDiscount == "От 0% до 5%")
        {
            startDiscount = 1;
            endDiscount = 5;
        }
        else if (selectedDiscount == "От 5% до 15%")
        {
            startDiscount = 5;
            endDiscount = 15;
        }
        else if (selectedDiscount == "От 15% до 30%")
        {
            startDiscount = 15;
            endDiscount = 30;
        }
        else if (selectedDiscount == "От 30% до 70%")
        {
            startDiscount = 30;
            endDiscount = 71;
        }
            
        if (startDiscount != 0 && endDiscount != 0)
        {
            var filteredUsers = services
                .Where(x => x.Скидка >= startDiscount && x.Скидка < endDiscount)
                .ToList();

            ServicesGrid.ItemsSource = filteredUsers;
        }
    }
    
    public void FillCmb()
    {
        services = new List<Services>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM услуга", conn);
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
        var typecmb = this.Find<ComboBox>(name:"CmbNum");
        typecmb.ItemsSource = services;
        
        var discontComboBox = this.Find<ComboBox>("DiscontComboBox");
        discontComboBox.ItemsSource = new List<string>
        {
            "Все скидки",
            "От 0% до 5%",
            "От 5% до 15%",
            "От 15% до 30%",
            "От 30% до 70%"
        };
    }

    private void TwoSearch_OnClick(object? sender, RoutedEventArgs e)
    {
        string twotxt = "SELECT Код, Наименование, Время_выполнения, Стоимость_услуги, Скидка, Стоимость_со_скидкой, Фото_услуги FROM услуга WHERE Наименование LIKE '%" + SearchName.Text + "%' AND Стоимость_услуги LIKE '%";
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
        string reset = "SELECT * FROM услуга;";
        ShowTable(reset);
        SearchName.Text = string.Empty;
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var TypeCmB = (ComboBox)sender;
        var currentflTrservices = TypeCmB.SelectedItem as Services;
        var fltrservices = services
            .Where(x => x.Код == currentflTrservices.Код)
            .ToList();
        ServicesGrid.ItemsSource = fltrservices;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            Services currentServices = ServicesGrid.SelectedItem as Services;
            if (currentServices == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM услуга WHERE Код = " + currentServices.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            services.Remove(currentServices);
            ShowTable("SELECT * FROM услуга;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        Services newServices = new Services();
        Servis.AddUpdZ addWindow = new Servis.AddUpdZ(newServices, services);
        addWindow.Show();
        this.Close();
    }

    private void EditData(object? sender, RoutedEventArgs e)
    {
        Services currentServices = ServicesGrid.SelectedItem as Services;
        if (ServicesGrid == null)
        {
            return;
        }
        Servis.AddUpdZ editWindow = new Servis.AddUpdZ(currentServices, services);
        editWindow.Show();
        this.Close();
    }
    private void SortAscending(object sender, RoutedEventArgs e)
    {
        var sortedItems = ServicesGrid.ItemsSource.Cast<Services>().OrderBy(s => s.Стоимость_услуги).ToList();
        ServicesGrid.ItemsSource = sortedItems;
    }

    private void SortDescending(object sender, RoutedEventArgs e)
    {
        var sortedItems = ServicesGrid.ItemsSource.Cast<Services>().OrderByDescending(s => s.Стоимость_услуги).ToList();
        ServicesGrid.ItemsSource = sortedItems;
    }

    private void SearchName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var srv = services;
        srv = srv.Where(x => x.Наименование.Contains(SearchName.Text)).ToList();
        ServicesGrid.ItemsSource = srv;
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