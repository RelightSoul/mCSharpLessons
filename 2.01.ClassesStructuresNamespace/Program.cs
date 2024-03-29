﻿// Классы, структуры и пространства имен

#region Class
//По сути класс представляет новый тип, который определяется пользователем. Класс определяется с помощью ключевого слова сlass:

//      class название_класса
//      {
//          // содержимое класса
//      }

class Employee
{

}
#endregion

#region Поля и методы класса
//  По сути поля класса - это переменные, определенные на уровне класса.
//  Кроме того, класс может определять некоторое поведение или выполняемые действия.
//  Для определения поведения в классе применяются методы.

class Car
{
    public string Make = "BMW";
    public int Speed;

    public void PrintCar()
    {
        Console.WriteLine($"Производитель: {Make}, Скорость: {Speed}");
    }
}
#endregion

#region Создание объекта класса
//  Для создания объекта применяются конструкторы. По сути конструкторы представляют специальные методы
//  , которые называются так же как и класс, и которые вызываются при создании нового объекта класса и
//  выполняют инициализацию объекта.

// 	    new конструктор_класса(параметры_конструктора);
//      Person tom = new Person();
#endregion

#region Обращение к функциональности класса
//  Для обращения к функциональности класса - полям, методам (а также другим элементам класса) применяется
//  точечная нотация точки - после объекта класса ставится точка, а затем элемент класса:

//      объект.поле_класса
//      объект.метод_класса(параметры_метода)
#endregion

#region Добавление класса в Visual Studio
//  ctrl + shift + A, выбираем класс, даём имя
#endregion
