namespace Base;

class Company
{
    string title;
    public Company(string title) => this.title = title;
    public void Print() => Console.WriteLine($"Компания: {title}");
}