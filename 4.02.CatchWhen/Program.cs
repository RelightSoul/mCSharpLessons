﻿#region Определение блока catch
//  За обработку исключения отвечает блок catch, который может иметь следующие формы:

//      catch
//      {
//           // выполняемые инструкции
//      }

// Обрабатывает только те исключения, которые соответствуют типу, указаному в скобках после оператора catch.
//      catch (тип_исключения)
//      {
//           // выполняемые инструкции
//      }

try
{
    int x1 = 5;
    int y1 = x1 / 0;
    Console.WriteLine($"Результат: {y1}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Ошибка");
}
//  Однако если в блоке try возникнут исключения каких-то других типов, отличных от DivideByZeroException,
//  то они не будут обработаны.


//      catch (тип_исключения имя_переменной)
//      {
//          // выполняемые инструкции
//      }
//  Обрабатывает только те исключения, которые соответствуют типу, указаному в скобках после оператора catch.
//  А вся информация об исключении помещается в переменную данного типа. Например:
try
{
    int x2 = 5;
    int y2 = x2 / 0;
    Console.WriteLine($"Результат: {y2}");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"Возникло исключение {ex.Message}");
}
//  Фактически этот случай аналогичен предыдущему за тем исключением, что здесь используется переменная.
//  В данном случае в переменную ex, которая представляет тип DivideByZeroException, помещается информация
//  о возникшем исключени. И с помощью свойства Message мы можем получить сообщение об ошибке.

//  Если нам не нужна информация об исключении, то переменную можно не использовать как в предыдущем случае.
#endregion

#region Фильтры исключений
//  Фильтры исключений позволяют обрабатывать исключения в зависимости от определенных условий.
//  Для их применения после выражения catch идет выражение when, после которого в скобках указывается условие:

//      catch when (условие)
//      {

//      }

int x = 1;
int y = 0;
try
{
    int result1 = x / y;
    int result2 = y / x;
}
catch (DivideByZeroException) when (y == 0)
{
    Console.WriteLine("переменная [ y ] не должна быть равна 0 ");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine(ex.Message);
}
#endregion