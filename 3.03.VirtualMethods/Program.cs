//  При наследовании нередко возникает необходимость изменить в классе-наследнике функционал метода,
//  который был унаследован от базового класса. В этом случае класс-наследник может переопределять
//  методы и свойства базового класса

//  Методы и свойства, которые мы хотим сделать доступными для переопределения, в базовом классе
//  помечается модификатором virtual

//  переопределить метод в классе-наследнике, этот метод определяется с модификатором override.
//  Переопределенный метод в классе-наследнике должен иметь тот же набор параметров, что и виртуальный
//  метод в базовом классе.

class Person
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;
    }
    public virtual void Print()
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
    public override void Print()
    {
        Console.WriteLine($"{Name} работает в {Company}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Person tom = new Person("Tom");
        tom.Print();

        Employee sam = new Employee("Sam", "BMW");
        sam.Print();

        Employee2 sam2 = new Employee2("Sam", "BMW");
        sam2.Print();
    }
}

//  При переопределении виртуальных методов следует учитывать ряд ограничений:

//  Виртуальный и переопределенный методы должны иметь один и тот же модификатор доступа.
//  То есть если виртуальный метод определен с помощью модификатора public, то и переопредленный
//  метод также должен иметь модификатор public.

//  Нельзя переопределить или объявить виртуальным статический метод.

#region Ключевое слово base
//  Кроме конструкторов, мы можем обратиться с помощью ключевого слова base к другим членам
//  базового класса. В нашем случае вызов base.Print(); будет обращением к методу Print() в классе Person:
class Employee2 : Person
{
    public string Company { get; set; }
    public Employee2(string name, string company) : base(name)
    {
        Company = company;
    }
    public override void Print()
    {
       base.Print();
       Console.WriteLine($" работает в {Company}");
    }
    
}
#endregion

#region Переопределение свойств
//  Также как и методы, можно переопределять свойства:
class Person2
{
    public virtual string Name { get; set; }
    public Person2(string name)
    {
        Name = name;
    }
    public virtual void Print()
    {
        Console.WriteLine(Name);
    }
}
class Employee3 : Person2
{
    public override string Name
    {
        get => base.Name;
        set
        {
            if (Name.Contains(" "))
            {
                Console.WriteLine("Неверное имя");
            }
            else
            {
                Name = value;
            }
        }
    }
    public string Company { get; set; }
    public Employee3(string name, string company) : base(name)
    {
        Company = company;
    }
    public override void Print()
    {
        Console.WriteLine($"{Name} работает в {Company}");
    }
}
#endregion

#region Запрет переопределения методов
//  Можно запретить переопределение методов и свойств. В этом случае их надо объявлять с модификатором sealed:
class Employee5 : Person
{
    public string Company { get; set; }

    public Employee5(string name, string company)
                : base(name)
    {
        Company = company;
    }

    public override sealed void Print()
    {
        Console.WriteLine($"{Name} работает в {Company}");
    }
}
//  В этом случае мы не сможем переопределить метод Print в классе, унаследованном от Employee.
//  !! sealed применяется в паре с override
#endregion