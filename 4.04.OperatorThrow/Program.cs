﻿// Обычно система сама генерирует исключения при определенных ситуациях, например, при делении числа на ноль.
// Но язык C# также позволяет генерировать исключения вручную с помощью оператора throw. То есть с помощью
// этого оператора мы сами можем создать исключение и вызвать его в процессе выполнения.

//  Например, в нашей программе происходит ввод имени пользователя, и мы хотим, чтобы, если длина имени
//  меньше 2 символов, то возникало исключение:

try
{
    Console.WriteLine("Введите имя: ");
    string? name = Console.ReadLine();
    if (name == null || name.Length < 2)
    {
        throw new Exception("Слишком короткое имя");
    }
    else
    {
        Console.WriteLine(name);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
//  После оператора throw указывается объект исключения, через конструктор которого мы можем передать
//  сообщение об ошибке. Естественно вместо типа Exception мы можем использовать объект любого другого
//  типа исключений.


//  Подобным образом мы можем генерировать исключения в любом месте программы. Но существует также и другая
//  форма использования оператора throw, когда после данного оператора не указывается объект исключения.
//  В подобном виде оператор throw может использоваться только в блоке catch:
try
{
    try
    {
        Console.Write("Введите имя: ");
        string? name1 = Console.ReadLine();
        if (name1 == null || name1.Length < 2)
        {
            throw new Exception("Длина имени меньше 2 символов");
        }
        else
        {
            Console.WriteLine($"Ваше имя: {name1}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
        throw;    // передаст исключение дальше, внешнему блоку.
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
//  В данном случае при вводе имени с длиной меньше 2 символов возникнет исключение,
//  которое будет обработано внутренним блоком catch. Однако поскольку в этом блоке
//  используется оператор throw, то исключение будет передано дальше внешнему блоку
//  catch, который получит то же смое исключение и выведет то же самое сообщение на консоль.
