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

public partial class Сотрудники : Window
{
    public Сотрудники()
    {
        InitializeComponent();
        string fullTable = "SELECT Код, Фамилия, Имя, Отчество, Пол_сотрудника, Телефон FROM работник;";
        ShowTable(fullTable);
    }
    private List<Employee> employees;
    private string connStr = "server=localhost;database=service;port=3306;User Id=root;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    public void ShowTable(string sql)
    {
        employees = new List<Employee>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentEmployee = new Employee()
            {
                Код = reader.GetInt32("Код"),
                Фамилия = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол_сотрудника = reader.GetInt32("Пол_сотрудника"),
                Телефон = reader.GetString("Телефон"),
            };
            employees.Add(currentEmployee);
        }
        conn.Close();
        EmployeeGrid.ItemsSource = employees;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Servis.main_client form2 = new Servis.main_client();
        Close();
        form2.Show();
    }
}