// Копирование объектов. Интерфейс ICloneable

// Поскольку классы представляют ссылочные типы, то это накладывает некоторые ограничения на их использование.
// В частности, допустим, у нас есть следующий класс:
class PersonExample
{
    public string Name { get; set; }
    public int Age { get; set; }
    public PersonExample(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
//  Создадим один объект PersonExample и попробуем скопировать его данные в другой объект PersonExample:
//  var tom = new PersonExample("Tom", 23);
//  var bob = tom;
//  bob.Name = "Bob";
//  Console.WriteLine(tom.Name); // Bob

//  В данном случае объекты tom и bob будут указывать на один и тот же объект в памяти, поэтому изменения свойств
//  для переменной bob затронут также и переменную tom.

//  Чтобы переменная bob указывала на новый объект, но при этом имела значения из переменной tom, мы можем применить
//  клонирование с помощью реализации интерфейса ICloneable:

//  public interface ICloneable
//  {
//       object Clone();
//  }

#region Поверхностное копирование
//  Реализация интерфейса в классе Person могла выглядеть следующим образом:
class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public object Clone()
    {
        return new Person(Name, Age);
    }
}
class Program
{
    static void Main(string[] args)
    {
        // -----  прямое присваивание ссылочных переменных -----
        var tom2 = new PersonExample("Tom", 55);
        var bob2 = tom2;
        bob2.Name = "Bob";
        Console.WriteLine(tom2.Name);  //Bob

        // -----  ручная реализация метода Clone или с помощью метода MemberwiseClone -----
        var tom = new Person("Tom",55);
        var bob = (Person)tom.Clone();
        bob.Name = "Bob";
        Console.WriteLine(tom.Name);  //Tom
        //  Теперь все нормально копируется, изменения в свойствах переменной bob не сказываются на свойствах из переменной tom.

        // -----  ручная реализация метода Clone или с помощью метода MemberwiseClone, класс содержит ссылочные перменные -----
        var tom3 = new Person3("Tom", 55, new Company("Microsoft"));
        var bob3 = (Person3)tom3.Clone();
        bob3.Work.Name = "Google";
        Console.WriteLine(tom3.Work.Name);    //Google, должно быть "Microsoft"

        // -----  Глубокое копирование -----
        var tom4 = new Person4("Tom", 55, new Company2("Microsoft"));
        var bob4 = (Person4)tom4.Clone();
        bob4.Work.Name = "Google"; 
        Console.WriteLine(tom4.Work.Name);    //"Microsoft"   Всё отлично
    }
}
//  Для сокращения кода копирования мы можем использовать специальный метод MemberwiseClone(), который возвращает копию объекта:
class Person2 : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person2(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
}
//  Этот метод реализует поверхностное (неглубокое) копирование. Однако данного копирования может быть недостаточно.
//  Например, пусть класс Person содержит ссылку на объект класса Company:
class Person3 : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Company Work { get; set; }
    public Person3(string name, int age, Company company)
    {
        Name = name;
        Age = age;
        Work = company;
    }
    public object Clone() => MemberwiseClone();
}
class Company
{
    public string Name { get; set; }
    public Company(string name) => Name = name;
}
#endregion

#region Глубокое копирование
//  Поверхностное копирование работает только для свойств, представляющих примитивные типы, но не для сложных объектов.
//  И в этом случае надо применять глубокое копирование:
class Person4 : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Company2 Work { get; set; }
    public Person4(string name, int age, Company2 company)
    {
        Name = name;
        Age = age;
        Work = company;
    }
    public object Clone() => new Person4(Name, Age, new Company2(Work.Name));
}
class Company2
{
    public string Name { get; set; }
    public Company2(string name) => Name = name;
}
#endregion