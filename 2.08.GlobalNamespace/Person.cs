using Base;   // Чтобы использовать класс Company мы подключаем его пространство имен: using Base;

class Person
{
    string name;
    Company company;
    public Person(string name, Company company)
    {
        this.name = name;
        this.company = company;
    }
    public void Print()
    {
        Console.WriteLine($"Имя: {name}");
        company.Print();
    }
}
