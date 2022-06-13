//  Точкой входа в программу на языке C# является метод Main. Именно с этого метода начинается
//  выполнение программы на C#. И программа на C# должна обязательно иметь метод Main. Однако
//  может возникнуть вопрос, какой еще метод Main, если, например, Visual Studio 2022 по умолчанию
//  создает проект консольного приложения со следующим кодом:

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//  В реальности этот код неявно помещается компилятором в метод Main, который, в свою очередь,
//  помещается в класс Program.

namespace _2._03.ProgramMain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Hello, World!");
        }
    }
}

// При неявном определении Program, опредления типов (в частности классов) должны идти в конце файла
// после инструкций верхнего уровня.