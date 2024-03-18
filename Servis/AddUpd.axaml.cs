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

public partial class AddUpd : Window
{
    public AddUpd()
    {
        InitializeComponent();
    }
    private List<Car> Carr;
    private Car _currentCar;
    public AddUpd( Car currentCar, List<Car> carr)
    {
        InitializeComponent();
        _currentCar = currentCar;
        this.DataContext = _currentCar;
        Carr = carr;
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var carr = Carr.FirstOrDefault(x => x.Код == _currentCar.Код);
        if (carr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO машина VALUES (" + Convert.ToInt32(Kod.Text)+ ", '" + Marca.Text + "', '" + Model.Text + "', '" + God.Text +"');";
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
                string upd = "UPDATE машина SET Марка = '" + Marca.Text + "', Модель = '" + Model.Text + "', Год_выпуска = '" + God.Text + "' WHERE Код = " + Convert.ToInt32(Kod.Text) + ";";
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
        Servis.МашиныZ carr = new Servis.МашиныZ();
        this.Close();
        carr.Show(); 
    }
}