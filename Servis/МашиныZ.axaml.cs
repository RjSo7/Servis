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

public partial class МашиныZ : Window
{
    public МашиныZ()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM машина;";
        ShowTable(fullTable);
        FillCmb();
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
    
    public void FillCmb()
    {
        car = new List<Car>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT Код, Марка, Модель, Год_выпуска FROM машина", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCar = new Car()
            {
                Код = reader.GetInt32("Код"),
                Марка = reader.GetString("Марка"),
                Модель = reader.GetString("Модель"),
                Год_выпуска = reader.GetString("Год_выпуска")
            };
            car.Add(currentCar);
        }
        conn.Close();
        var typecmb = this.Find<ComboBox>(name:"CmbNum");
        typecmb.ItemsSource = car;
    }

    private void TwoSearch_OnClick(object? sender, RoutedEventArgs e)
    {
        string twotxt = "SELECT Код, Марка, Модель, Год_выпуска FROM машина WHERE Марка LIKE '%" + SearchMarc.Text + "%' AND Год_выпуска LIKE '%" + SearchGod.Text + "%'";
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
        string reset = "SELECT Код, Марка, Модель, Год_выпуска FROM машина;";
        ShowTable(reset);
        SearchMarc.Text = string.Empty;
        SearchGod.Text = string.Empty;
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var TypeCmB = (ComboBox)sender;
        var currentflTrcar = TypeCmB.SelectedItem as Car;
        var fltrcar = car
            .Where(x => x.Код == currentflTrcar.Код)
            .ToList();
        CarGrid.ItemsSource = fltrcar;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            Car currentCar = CarGrid.SelectedItem as Car;
            if (currentCar == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM машина WHERE Код = " + currentCar.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            car.Remove(currentCar);
            ShowTable("SELECT Код, Марка, Модель, Год_выпуска FROM машина;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        Car newCar = new Car();
        Servis.AddUpd addWindow = new Servis.AddUpd(newCar, car);
        addWindow.Show();
        this.Close();
    }

    private void EditData(object? sender, RoutedEventArgs e)
    {
        Car currentCar = CarGrid.SelectedItem as Car;
        if (CarGrid == null)
        {
            return;
        }
        Servis.AddUpd editWindow = new Servis.AddUpd(currentCar, car);
        editWindow.Show();
        this.Close();
    }
}