public partial class Person
{
    partial void Check();
    public partial void Read()
    {
        Console.WriteLine("Читает");
    }
    public void Eat()
    {
        Console.WriteLine("I am eating");
    }
}