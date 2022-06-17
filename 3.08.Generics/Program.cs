// С помощью универсальных параметров мы можем типизировать обобщенные классы любым типом.

//  Обобщенные типы позволяют указать конкретный тип, который будет использоваться.

class Person<T>
{
    public T Id { get; set; }
    public string Name { get; set; }
    public Person(T id, string name)
    {
        Id = id;
        Name = name;
    }
}
//  Угловые скобки в описании class Person<T> указывают, что класс является обобщенным, а тип T,
//  заключенный в угловые скобки, будет использоваться этим классом. Необязательно использовать
//  именно букву T, это может быть и любая другая буква или набор символов. Причем сейчас на этапе
//  написания кода нам неизвестно, что это будет за тип, это может быть любой тип. Поэтому параметр
//  T в угловых скобках еще называется универсальным параметром, так как вместо него можно подставить любой тип.

//  Мы могли бы использовать var, спровоцировав операции упаковки (boxing) и распаковки (unboxing) и т.д,
//  что введёт к снижению производительности. Обобщенный тип, решает эту проблему.

class Program
{
    static void Main(string[] args)
    {
        Person<int> pers1 = new Person<int>(3334, "Sam");
        Person<string> pers2 = new Person<string>("4503", "Alex");
        Console.WriteLine(pers1.Id);
        Console.WriteLine(pers2.Id);

        Company<Person<int>> BMW = new Company<Person<int>>(pers1);
        Console.WriteLine(BMW.CEO.Id);

        Person2<int> per3 = new Person2<int>(450603, "Mark");
        Person2<int>.code = 1543;
        Console.WriteLine(Person2<int>.code);

        Person3<int, string> per4 = new Person3<int, string>(12345, "Пароль", "Cesar");
        Console.WriteLine($"ID = {per4.Id}, PWD = {per4.Password}");

        void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }
        int x = 5;
        int y = -505;
        Swap<int>(ref x, ref y);
        Console.WriteLine($"X = {x}, Y = {y}");
    }
}
//  Таким образом, используя обобщенный вариант класса, мы снижаем время на выполнение
//  и количество потенциальных ошибок.

//  Универсальный параметр также может представлять обобщенный тип:
class Company<P>
{
    public P CEO { get; set; }
    public Company(P ceo)
    {
        CEO = ceo;
    }
}
// пример выше в Программ

#region Статические поля обобщенных классов
//  При типизации обобщенного класса определенным типом будет создаваться свой набор статических членов.
//  Например, в классе Person определено следующее статическое поле:
class Person2<T>
{
    public static T? code;
    public T Id { get; set; }
    public string Name { get; set; }
    public Person2(T id, string name)
    {
        Id = id;
        Name = name;
    }
}
//  В итоге для Person2<string> и для Person2<int> будет создана своя переменная code.
#endregion

#region Использование нескольких универсальных параметров
//  Обобщения могут использовать несколько универсальных параметров одновременно, которые
//  могут представлять одинаковые или различные типы
class Person3 <T,K>
{
    public T Id { get;}
    public K Password { get; set; }
    public string Name { get;}
    public Person3(T id, K password, string name)
    {
        Id = id;
        Password = password;
        Name = name;    
    }
}
// Пример в программ
#endregion

#region Обобщенные методы
//  Кроме обобщенных классов можно также создавать обобщенные методы, которые точно также будут
//  использовать универсальные параметры. Например:

    //      void Swap<T> (ref T x, ref T y)
    //      {
    //           T temp = x;
    //          x = y;
    //          y = temp;
    //      }
//  Пример в программ
#endregion