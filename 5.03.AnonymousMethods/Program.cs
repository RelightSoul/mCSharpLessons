﻿// С делегатами тесно связаны анонимные методы.
// Анонимные методы используются для создания экземпляров делегатов.

//  Определение анонимных методов начинается с ключевого слова delegate,
//  после которого идет в скобках список параметров и тело метода в фигурных скобках:

//      delegate (параметры)
//      {
//          // инструкции
//      }

 MessangeHandler handler = delegate (string mes)
 {
    Console.WriteLine(mes);
 };
 // delegate void MessangeHandler(string message);   // объявлен в конце кода

//Анонимный метод не может существовать сам по себе, он используется для инициализации
//экземпляра делегата, как в данном случае переменная handler представляет анонимный
//метод. И через эту переменную делегата можно вызвать данный анонимный метод.

//  Другой пример анонимных методов - передача в качестве аргумента для параметра, который
//  представляет делегат:
ShowMessage("hello", delegate (string mes)
{
   Console.WriteLine(mes);
});
static void ShowMessage(string message, MessangeHandler handler)
{
   handler(message);
}
// delegate void MessangeHandler(string message);   // объявлен в конце кода

//  Если анонимный метод использует параметры, то они должны соответствовать параметрам делегата.
//  Если для анонимного метода не требуется параметров, то скобки с параметрами опускаются.
//  При этом даже если делегат принимает несколько параметров, то в анонимном методе можно
//  вовсе опустить параметры:

MessangeHandler handler2 = delegate
{
    Console.WriteLine("анонимный метод");
};
handler2("Test");   // анонимный метод

//  При этом параметры анонимного метода не могут быть опущены, если один или несколько параметров
//  определены с модификатором out.

//  Также, как и обычные методы, анонимные могут возвращать результат, при этом анонимный метод
//  имеет доступ ко всем переменным, определенным во внешнем коде:

int z = 9;
Operation operation = delegate (int x, int y)
{
    return x + y + z;
};
int result = operation(4, 5);
Console.WriteLine(result);

#region Конец кода
delegate int Operation(int x, int y);
delegate void MessangeHandler(string message);
#endregion

