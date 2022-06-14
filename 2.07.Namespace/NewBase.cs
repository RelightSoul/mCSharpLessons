namespace NewBase
{
    class Person2
    {
        string name;
        public Person2(string name) => this.name = name;
        public void Print() => Console.WriteLine($"Name = {name}");
    }
}
// подключим в Program.cs  using NewBase;