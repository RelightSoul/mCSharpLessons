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

            //Также, как и для класса, можно использовать инициализатор для создания структуры
            Person2 sam2 = new Person2() { Name = "Sam2", Age = 13};

            #region Копирование структуры с помощью with
            //  Если нам необходимо скопировать в один объект структуры значения из другого с небольшими
            //  изменениями, то мы можем использовать оператор with

            Person2 newOldSam = sam2 with { Age = 77 };
            #endregion

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
    public Person2(string name): this(name,0)
    {
        Name = name;
    }
    public Person2(string name,int age)
    {
        Name = name; Age = age;  
    }
    public void Print()
    {
        Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
    }
}
//  В случае если нам необходимо вызывать конструкторы с различным количеством параметров, то мы можем, как
//  и в случае с классами, вызывать их по цепочке

