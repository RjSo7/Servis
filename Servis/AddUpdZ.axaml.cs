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

public partial class AddUpdZ : Window
{
    public AddUpdZ()
    {
        InitializeComponent();
    }
    private List<Services> Servicess;
    private Services _currentServices;
    public AddUpdZ( Services currentServices, List<Services> servicess)
    {
        InitializeComponent();
        _currentServices = currentServices;
        this.DataContext = _currentServices;
        Servicess = servicess;
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var servicess = Servicess.FirstOrDefault(x => x.Код == _currentServices.Код);
        if (servicess == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO услуга VALUES (" + Convert.ToInt32(Код.Text)+ ", '" + Наименование.Text + "', '" + Время_выполнения.Text + "', " + Convert.ToInt32(Стоимость_услуги.Text ) + "," + Convert.ToInt32(Скидка.Text ) + "," + Convert.ToInt32(Стоимость_со_скидкой.Text ) + ",'"+ Фото_услуги.Text + "');";
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
                string upd = "UPDATE услуга SET Наименование = '" + Наименование.Text + "', Время_выполнения = '" + Время_выполнения.Text + "', Стоимость_услуги = "+ Convert.ToInt32(Стоимость_услуги.Text) + ",Скидка = "+ Convert.ToInt32(Скидка.Text) + ",Стоимость_со_скидкой = "+ Convert.ToInt32(Стоимость_со_скидкой.Text) + ",Фото_услуги = '" + Фото_услуги.Text + "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
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
        Servis.УслугиZ servicess = new Servis.УслугиZ();
        this.Close();
        servicess.Show(); 
    }
    private async void File_Select(object sender, RoutedEventArgs e)
    {
        try
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filters.Add(new FileDialogFilter()
                { Name = "Image Files", Extensions = { "jpg", "jpeg", "png", "gif" } }); 
            string[]? fileNames = await fileDialog.ShowAsync(this);
            if (fileNames != null && fileNames.Length > 0)
            {
                string imagePath = System.IO.Path.GetFileName(fileNames[0]);
                Фото_услуги.Text = imagePath;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}