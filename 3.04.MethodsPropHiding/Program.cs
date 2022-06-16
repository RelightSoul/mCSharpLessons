// Фактически скрытие метода/свойства представляет определение в классе-наследнике метода или свойства,
// которые соответствует по имени и набору параметров методу или свойству базового класса. Для скрытия
// членов класса применяется ключевое слово new.

#region Скрытие методов
class Person
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;    
    }
    public void Print()
    {
        Console.WriteLine(Name);
    }
}
class Employee : Person
{
    public string Company { get; set; }
    public Employee(string name, string company) : base(name)
    {
        Company = company;      
    }
    public new void Print()
    {
        Console.WriteLine($"{Name} {Company}");
    }
    //  Например, в примере выше метод Print в базовом классе не является виртуальным,
    //  мы не можем его переопределить, но, допустим, нас не устраивает его реализация
    //  для производного класса, поэтому мы можем воспользоваться сокрытием, чтобы определить
    //  нужный нам функционал. 
}
#endregion

#region Скрытие свойств
class Person2
{
    public string Name { get; set; }
    public Person2(string name)
    {
        Name = name;    
    }
}
class Employee2 : Person2
{
    public new string Name
    {
        get => $"Mr./Ms. {base.Name}"; 
        set => base.Name = value;    
    }
    public string Company { get; set; }
    public Employee2(string name, string company) : base(name)
    {
        Company = company;  
    }

}
#endregion

#region Скрытие переменных и констант
//  В отличие от переопределения C# позволяет применять скрытие к переменным
//  (как к статическим, так и нестатическим) и константам

class Person3
{
    public const int minAge = 5;
    public readonly string Type = "Person";
}
class Employee3 : Person3
{
    public new const int minAge = 10;
    public new readonly string Type = "Employee";
}
#endregion
