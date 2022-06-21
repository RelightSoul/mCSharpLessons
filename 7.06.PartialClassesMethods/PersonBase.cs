public partial class Person
{
    partial void Check() { }
    public partial void Read();
    public void Move()
    {
        Console.WriteLine("I am moving");
    }
}
