class Program
{
    static void Main(string[] args)
    {
        var number = 5;
        var result = Square(number);
        Console.WriteLine($"Квадрат {number} равен {result}");
    }
    static int Square(int n) => n * n;
}


//Person tom = new Person("Tom");
//Console.WriteLine($"Hello, {tom.Name}");

//class Person
//{
//    public string Name { get; }
//    public Person(string name) => Name = name;
//}
