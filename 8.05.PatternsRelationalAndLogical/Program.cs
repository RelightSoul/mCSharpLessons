﻿// Реляционный и логический паттерны

//  В C# 9.0 в язык были добавлены дополнительные паттерны - реляционный (relational pattern) и
//  логический (logical pattern) паттерны.

//  --------- Реляционный паттерн ---------
//  Реляционный паттерн позволяет сравнить передаваемое в конструкцию значение с некоторыми значениями
//  с помощью операций сравнения. Например, в зависимости от суммы вклада проценты по этому вкладу могут
//  отличаться. Рассчитаем сумму процентов в зависимости от суммы вклада с использованием реляционного паттерна:

decimal Calculate(decimal sum)
{
    return sum switch 
    {
        <= 0 => 0,
        < 50000 => sum * 0.05m,
        < 100000 => sum * 0.10m,
        _ => sum * 0.20m
    };
}
Console.WriteLine(Calculate(-200));     // 0
Console.WriteLine(Calculate(0));        // 0 
Console.WriteLine(Calculate(10000));    // 500
Console.WriteLine(Calculate(60000));    // 6000
Console.WriteLine(Calculate(200000));   // 40000

//  --------- Логический паттерн ---------
//  Логический паттерн позволяет использовать логические операторы and (логическое умножение или операция
//  логического И) и or(логическое сложение или операция логического ИЛИ) для объединения операций сравнения.
//  Например, передадим в метод возраст и возвратим соответствующее сообщение:

string CheckAge(int age)
{
    return age switch
    {
        <1 or >110 => "Недействительный возраст",
        >=1 and <18 => "Доступ запрещён",
        _ => "Доступ разрешён"                        // в остальных случаях
    };
}

Console.WriteLine(CheckAge(200));     // Недействительный возраст
Console.WriteLine(CheckAge(0));       // Недействительный возраст 
Console.WriteLine(CheckAge(17));      // Доступ запрещён
Console.WriteLine(CheckAge(18));      // Доступ разрешён

//  Еще один логический оператор - not используется для отрицания и возращает true, если его аргумент равен false:
string CheckAge2(int age) => age switch
{
    not 33 => $"{age}",
    _ => "Вам 33"
};
Console.WriteLine(CheckAge2(33));