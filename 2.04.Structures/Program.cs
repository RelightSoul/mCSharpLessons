// Наряду с классами структуры представляют еще один способ создания собственных типов
// данных в C#. Более того многие примитивные типы, например, int, double и т.д., по
// сути являются структурами.

#region Определение структуры
//  Для определения структуры применяется ключевое слово struct:
//  struct имя_структуры
//  {
//      // элементы структуры
//  }
//После слова struct идет название структуры и далее в фигурных скобках размещаются элементы
//структуры - поля, методы и т.д.
#endregion

namespace _2._04.Structures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person somePers = new Person();   //будут значения по умолчанию

            Person sam;
            sam.Name = "Sam";
            sam.Age = 13;

            somePers.Print();
            sam.Print();

            Person2 person2 = new Person2();
            person2.Print();

        }
    }
}

struct Person
{
    public string Name;//= "Tom" // можно объявить значения по умолчанию, он необходимо явно определить конструктор
    public int Age ;//= 14

    public Person(string name, int age) //string name = "Sara", int age = 1  ////можно объявить значения по умолчанию
    {   
        Name = name; Age = age; 
    }
    public void Print()
    {
        Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
    }
}

struct Person2
{
    public string Name;
    public int Age;
    //  начиная с версии C# 10 мы можем определить свой конструктор без параметров:
    //  при определении конструктора без параметров необходимо инициализировать все поля структуры
    public Person2() 
    {
        Name = "Undefined"; Age = 0;  // по умолчанию
    }
    public void Print()
    {
        Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
    }
}

