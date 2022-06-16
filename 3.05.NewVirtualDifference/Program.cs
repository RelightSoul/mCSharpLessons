// Различие переопределения и скрытия методов
#region Переопределение
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
//  Используем классы в программе
#endregion

class Program
{
    static void Main(string[] args)
    {
        Person tom = new Employee("Tom","BMW");
        tom.Print();
        //  При вызове tom.Print() выполняется реализация метода Print из класса Employee,
        //  несмотря на то, что переменная tom - переменная типа Person.

        Person2 tom2 = new Employee2("Tom","BMW");
        tom2.Print();
        //  Переменная tom2 представляет тип Person2, но хранит ссылку на объект Employee2.
        //  Однако при вызове метода Print будет выполняться та версия метода, которая
        //  определена именно в классе Person2, а не в классе Employee2. Почему? Класс Employee2
        //  никак не переопределяет метод Print, унаследованный от базового класса, а фактически
        //  определяет новый метод. Поэтому при вызове tom.Print() вызывается метод Print из класса Person.
    }
}

#region Скрытие
class Person2
{
    public string Name { get; set; }
    public Person2(string name)
    {
        Name = name;
    }
    public void Print()
    {
        Console.WriteLine(Name);
    }
}
class Employee2 : Person2
{
    public string Company { get; set; }
    public Employee2(string name, string company) : base(name)
    {
        Company = company;
    }
    public new void Print()
    {
        Console.WriteLine($"{Name} работает в {Company}");
    }
}
#endregion