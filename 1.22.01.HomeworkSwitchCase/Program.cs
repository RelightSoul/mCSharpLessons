﻿//  Напишите консольную программу, которая выводит пользователю сообщение "Введите номер операции:
//  1.Сложение 2.Вычитание 3.Умножение". Рядом с названием каждой операции указан ее номер, например,
//  операция вычитания имеет номер 2. Пусть пользователь вводит в программу номер операции, и в 
//  зависимости от номера операции программа выводит ему название операции.

//  Для определения операции по введенному номеру используйте конструкцию switch...case.

//  Если введенное пользователем число не соответствует никакой операции (например, число 120),
//  то выведите пользователю сообщение о том, что операция неопределена.

//  После выполнения задания описанного выше, измените программу. Пусть пользователь кроме номера
//  операции вводит два числа, и в зависимости от номера операции с введенными числами выполняются определенные
//  действия (например, при вводе числа 3 числа умножаются). Результа операции выводиться на консоль.

Console.WriteLine("Данная программа выполняет Сложение, Вычитание или Умножение двух целых чисел");
Console.Write("Введите первое число: ");
int x = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите второе число: ");
int y = Convert.ToInt32(Console.ReadLine());
Console.WriteLine(@"Введите операцию для арифметического действия в виде числа:
                    1 - Сложение
                    2 - Вычитание
                    3 - Умножение");

int op = Convert.ToInt32(Console.ReadLine());
int result = 0;

switch (op)
{
    case 1:
        result = x + y;
        Console.WriteLine("Сложение: {0}", result);
        break;
    case 2:
        result = x - y;
        Console.WriteLine("Вычитание: {0}", result);
        break;
    case 3:
        result = x * y;
        Console.WriteLine("Умножение: {0}", result);
        break;
    default:
        Console.WriteLine("Неизвестная операция");
        break;
}


