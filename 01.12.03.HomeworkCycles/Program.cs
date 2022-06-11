// Напишите программу, которая выводит на консоль таблицу умножения

for (int a = 1;  a < 10;  a++)
{
    for (int b = 1; b < 10; b++)
    {
        Console.Write($"{a * b}\t");
    }
    Console.WriteLine();
}
