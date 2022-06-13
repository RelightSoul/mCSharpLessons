// Обычно определяемые классы и другие типы в .NET не существуют сами по себе, а заключаются
// в специальные контейнеры - пространства имен. Пространства имен позволяют организовать код
// программы в логические блоки, поволяют объединить и отделить от остального кода некоторую
// функциональность, которая связана некоторой общей идеей или которая выполняет определенную задачу.

//      namespace имя_пространства_имен
//      {
//          // содержимое пространства имен
//      }
using Base;
using NewBase;

FewSpaces.One.Winner here = new FewSpaces.One.Winner("Paul");

Person2 roma = new Person2("Рома");  //Переменная из области NewBase
namespace _2._07.Namespace
{
    class Program
    {
        static void Main(string[] args)
        {
            // Person tom = new Person("Tom"); // Ошибка - Visual Studio не видит класс Person
            Base.Person tom = new Base.Person("Tom");
            // Первый способ обратиться напрямую к пространству имён
            // Второй подключить  пространство имен using Base;
            Person tom2 = new Person("Tom2");
        }
    }
}

namespace Base
{
    class Person
    {
        string name;
        public Person(string name) => this.name = name;
        public void Print() => Console.WriteLine($"Name = {name}");
    }
}
// Одни пространства имен могут содержать другие. Например:
namespace FewSpaces
{
    namespace One
    {
        class Winner
        {
            string nameOne;
            public Winner(string name) => this.nameOne = name;
            public void Print() => Console.WriteLine($"Name = {nameOne}");
        }
    }
    namespace Two
    {
        class Loser
        {
            string nameTwo;
            public Loser(string name) => this.nameTwo = name;
            public void Print() => Console.WriteLine($"Name = {nameTwo}");
        }
    }
}
//  Для обращения к этим классам вне пространства Base необходимо использовать всю цепочку пространств имен.
//  Пример выше, перед using;

#region Пространства имен уровня файла
//  Начиная с.NET 6 и C# 10 можно определять пространства имен на уровне файла.
//  Например, добавим в проект новый файл с кодом c#. Ctrl + Shitf + A задаём имя NewBase, продолжаем там.
#endregion

