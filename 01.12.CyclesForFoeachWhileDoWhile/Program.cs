﻿#region for
//for ([действия_до_выполнения_цикла]; [условие] ; [действия_после_выполнения])
//{
//    // действия
//}

//Объявление цикла for состоит из трех частей. Первая часть объявления цикла - некоторые действия,
//которые выполняются один раз до выполнения цикла. Обычно здесь определяются переменные, которые
//будут использоваться в цикле.

//Вторая часть - условие, при котором будет выполняться цикл. Пока условие равно true, будет
//выполняться цикл.

//И третья часть - некоторые действия, которые выполняются после завершения блока цикла.
//Эти действия выполняются каждый раз при завершении блока цикла.

//После объявления цикла в фигурных скобках помещаются сами действия цикла.

for (int i = 1; i < 4; i++)
{
    Console.WriteLine(i);
}
//Если блок цикла for содержит одну инструкцию, то мы можем его сократить, убрав фигурные свобки:
for (int i = 1; i < 4; i++)
    Console.WriteLine(i);
//или
for (int i = 1; i < 4; i++) Console.WriteLine(i);

//При этом необязательно именно в первой части цикла объявлять переменную, а в третий части изменять
//ее значение - это могут быть любые действия. Например:
var j = 1;

for (Console.WriteLine("Начало выполнения цикла"); j < 4; Console.WriteLine($"i = {j}"))
{
    j++;
}

//Нам необязательно указывать все условия при объявлении цикла. Например, мы можем написать так:
//  int i = 1;
//  for ( ; ; )
//  {
//      Console.WriteLine($"i = {i}");      цикл будет работать вечно - бесконечный цикл
//      i++;
//  }

//Можно определять несколько переменных в объявлении цикла
for (int i1 = 1, j1 = 1; i1 < 10; i1++, j1++)
    Console.WriteLine($"{i1 * j1}");
#endregion

#region do while
//В цикле do сначала выполняется код цикла, а потом происходит проверка условия в
//инструкции while. И пока это условие истинно, цикл повторяется.
//  do
//  {
//     действия цикла
//  }
//  while (условие)
int h = 6;
do
{
    Console.WriteLine(h);
    h--;
}
while (h > 0);
//Здесь код цикла сработает 6 раз, пока h не станет равным нулю. Но важно отметить, что цикл do
//гарантирует хотя бы единократное выполнение действий, даже если условие в инструкции while
//не будет истинно. То есть мы можем написать:
int h1 = -1;
do
{
    Console.WriteLine(h1);
    h1--;
}
while (h1 > 0);
//Хотя у нас переменная h1 меньше 0, цикл все равно один раз выполнится.
#endregion

#region while
//В отличие от цикла do цикл while сразу проверяет истинность некоторого условия,
//и если условие истинно, то код цикла выполняется:
//  while (условие)
//  {
//       действия цикла
//  }
int m = 6;
while (m > 0)
{
    Console.WriteLine(m);
    m--;
}
#endregion

#region foreach
//  foreach (тип_данных переменная in коллекция)
//  {
//       // действия цикла
//  }
foreach (char c in "Tom")
{
    Console.WriteLine(c);    //Строка по сути - это коллекция символов. И .NET позволяет перебрать все элементы строки
}
//  В реальности не всегда бывает очевидно, какой тип представляют элементы коллекции. 
//  В этом случае мы можем определить переменную с помощью оператора var
foreach (var c1 in "Tom")
{
    Console.WriteLine(c1);
}
#endregion

#region continue и break
// Иногда возникает ситуация, когда требуется выйти из цикла, не дожидаясь его завершения.
// В этом случае мы можем воспользоваться оператором break.

// Цикл сработает 5 раз. Так как при достижении счетчиком q значения 5, сработает оператор break, и цикл завершится.
for (int q = 0; q < 9; q++)
{
    if (q == 5)        
        break;
    Console.WriteLine(q);     
}

//еперь поставим себе другую задачу. А что если мы хотим, чтобы при проверке цикл не завершался, а просто пропускал
//текущую итерацию. Для этого мы можем воспользоваться оператором continue:

//В этом случае цикл, когда дойдет до числа 5, которое не удовлетворяет условию проверки,
//просто пропустит это число и перейдет к следующей итерации:
for (int q1 = 0; q1 < 9; q1++)
{
    if (q1 == 5)
        continue;
    Console.WriteLine(q1);
}

// Операторы break и continue можно применять в любом типе циклов.
#endregion

#region Вложенные циклы
//  Одни циклы могут быть вложенными в другие. Например:
for (int i = 1; i < 10; i++)
{
    for (int j2 = 1; j2 < 10; j2++)
    {
        Console.Write($"{i * j2} \t");
    }
    Console.WriteLine();
}
//В данном случае цикл for (int i = 1; i < 10; i++) выполняется 9 раз, то есть имеет 9 итераций.
//Но в рамках каждой итерации выполняется девять раз вложенный цикл for (int j = 1; j < 10; j++).
//В итоге данная программа выведет таблицу умножения.
#endregion