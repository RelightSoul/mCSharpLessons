//Напишите консольную программу, в которую пользователь вводит с клавиатуры два числа.
//А программа сранивает два введенных числа и выводит на консоль результат сравнения
//(два числа равны, первое число больше второго или первое число меньше второго).
Console.WriteLine("Сравнение двух целых чисел: ");
int a;
int b;
try
{
    Console.Write("Введите первое число: ");
    a = int.Parse(Console.ReadLine());
    Console.Write("Введите второе число: ");
    b = int.Parse(Console.ReadLine());
    if (a == b)
    {
        Console.WriteLine($"Число {a} равно числу {b}");
    }
    else if (a > b)
    {
        Console.WriteLine($"Число {a} больше числа {b}");
    }
    else
    {
        Console.WriteLine($"Число {a} меньше числа {b}");
    }
}
catch (FormatException ex)
{
    Console.WriteLine(ex.Message);
}
