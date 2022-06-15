// Псевдонимы типов и статический импорт
#region Псевдонимы
//  Для различных классов и структур мы можем использовать псевдонимы. Затем в программе вместо
//  названия типа используется его псевдоним. Например, для вывода строки на экран применяется
//  метод Console.WriteLine(). Но теперь зададим для класса Console псевдоним:
using printer = System.Console;
using User = Person;
using static System.Console;
using static Operation;

printer.WriteLine("lalala");
User sam = new User("Sam");
//  Для различных классов и структур мы можем использовать псевдонимы. Затем в программе вместо
//  названия типа используется его псевдоним. Например, для вывода строки на экран применяется
//  метод Console.WriteLine(). Но теперь зададим для класса Console псевдоним: printer
#endregion

#region Статический импорт
//  Также в C# имеется возможность импорта статической функциональности классов. Например,
//  импортируем возможности класса Console:

//  using static System.Console;
//  WriteLine("Hello from C# 8.0");

//  Выражение using static подключает в программу все статические методы и свойства, а также константы.
//  И после этого мы можем не указывать название класса при вызове метода.

WriteLine(Sum(4, 5));  //выше подключен using static System.Console; using static class Operation
WriteLine(Subtract(5, 4));
WriteLine(Multiply(5, 4));
#endregion

static class Operation
{
    public static int Sum(int a, int b) => a + b;
    public static int Subtract(int a, int b) => a - b;
    public static int Multiply(int a, int b) => a * b;
}
class Person
{
    public string Name { get; set; }
    public Person(string name) => Name = name;
}