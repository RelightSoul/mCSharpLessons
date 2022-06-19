// В .NET есть несколько встроенных делегатов, которые используются в различных ситуациях. И наиболее
// используемыми, с которыми часто приходится сталкиваться, являются Action, Predicate и Func.

#region Action
//  Делегат Action представляет некоторое действие, которое ничего не возвращает, то есть в качестве
//  возвращаемого типа имеет тип void:

//          public delegate void Action()
//          public delegate void Action<in T>(T obj)

//  Данный делегат имеет ряд перегруженных версий. Каждая версия принимает разное число параметров: от Action<in T1>
//  до Action<in T1, in T2,....in T16>. Таким образом можно передать до 16 значений в метод.

//  Как правило, этот делегат передается в качестве параметра метода и предусматривает вызов определенных действий
//  в ответ на произошедшие действия. Например:

DoOperation(10, 6, Add);
DoOperation(10, 6, Multiply);

void DoOperation(int a, int b, Action<int, int> op) => op(a, b);

void Add(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");
void Multiply(int x, int y) => Console.WriteLine($"{x} * {y} = {x * y}");
#endregion

#region Predicate
//  Делегат Predicate<T> принимает один параметр и возвращает значение типа bool:

//          delegate bool Predicate<in T>(T obj);

//  Как правило, используется для сравнения, сопоставления некоторого объекта T определенному условию. В качестве
//  выходного результата возвращается значение true, если условие соблюдено, и false, если не соблюдено:

Predicate<int> isPosivite = (int x) => x > 0;
Console.WriteLine(isPosivite(20));
Console.WriteLine(isPosivite(-20));
//  В данном случае возвращается true или false в зависимости от того, больше нуля число или нет.
#endregion

#region Func
//  Еще одним распространенным делегатом является Func. Он возвращает результат действия и может принимать параметры.
//  Он также имеет различные формы: от Func<out T>(), где T - тип возвращаемого значения, до Func<in T1, in T2,...in T16,
//  out TResult>(), то есть может принимать до 16 параметров.

//      TResult Func<out TResult>()
//      TResult Func<in T, out TResult>(T arg)
//      TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2)
//      TResult Func<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3)
//      TResult Func<in T1, in T2, in T3, in T4, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)

//  Данный делегат также часто используется в качестве параметра в методах:
int result1 = DoOperation2(4, DoubleN);
Console.WriteLine(result1);

int result2 = DoOperation2(4, SquareN);
Console.WriteLine(result2);

int DoOperation2(int n, Func<int, int> operation) => operation(n);
int DoubleN(int x) => x * 2;
int SquareN(int x) => x * x;

//  Метод DoOperation() в качестве параметра принимает делегат Func<int, int>, то есть ссылку на метод,
//  который принимает число int и возвращает также значение int.

//  При первом вызове метода DoOperation() ему передается ссылка на метод DoubleNumber, который увеличивает
//  число в два раза. Во втором случае передается метод SquareNumber - опять же метод, который принимает
//  параметр типа int и возвращает результат в виде значения int.

//  Другой пример:

Func<int, int, string> createStr = (a,b) => $"{a}{b}";
Console.WriteLine(createStr(1,6));
Console.WriteLine(createStr(44,2));
#endregion