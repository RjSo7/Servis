using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;

namespace Servis;

public  class Services
{
    public int Код { get; set; }
    public string Наименование { get; set; }
    public string Время_выполнения { get; set; }
    public double Стоимость_услуги { get; set; }
    public int Скидка { get; set; }
    public double Стоимость_со_скидкой { get; set; }
    public Bitmap? Фото_услуги { get; set; }
    public string ImagePath { get; set; }
}
public  class Car
{
    public int Код { get; set; }
    public string Марка { get; set; }
    public string Модель { get; set; }
    public string Год_выпуска { get; set; }
}
public  class Client
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public int Код_пол_клиента { get; set; }
    public string Телефон { get; set; }
    public int Код_машина_клиента { get; set; }
}
public  class Employee
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public int Пол_сотрудника { get; set; }
    public string Телефон { get; set; }
}
public  class Orders
{
    public int Код { get; set; }
    public int Код_клиента { get; set; }
    public int Код_услуги { get; set; }
    public string Дата { get; set; }
    
}
public  class Repair
{
    public int Код { get; set; }
    public int Код_работника { get; set; }
    public int Код_заказа { get; set; }
    public string Описание { get; set; }
}
public  class Zaivka
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Телефон { get; set; }
    public string Описание_проблемы { get; set; }
}