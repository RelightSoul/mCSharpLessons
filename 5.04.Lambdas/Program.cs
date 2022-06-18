// Лямбда-выражения представляют упрощенную запись анонимных методов.
// Лямбда-выражения позволяют создать емкие лаконичные методы, которые
// могут возвращать некоторое значение и которые можно передать в качестве
// параметров в другие методы.

//  Ламбда-выражения имеют следующий синтаксис: слева от лямбда-оператора =>
//  определяется список параметров, а справа блок выражений, использующий эти параметры:

//      (список_параметров) => выражение
Message hello = () => Console.WriteLine("Hello");
hello();
hello();
hello();
//  delegate void Message();   объявлен в конце кода

//  В данном случае переменная hello представляет делегат Message - то есть некоторое действие,
//  которое ничего не возвращает и не принимает никаких параметров. В качестве значения этой
//  переменной присваивается лямбда-выражение. Это лямбда-выражение должно соответствовать
//  делегату Message - оно то же не принимает никаких параметров, поэтому слева от лямбда-оператора
//  идут пустые скобки. А справа от лямбда-оператора идет выполняемое выражение -
//  Console.WriteLine("Hello")

//  Если лямбда-выражение содержит несколько действий, то они помещаются в фигурные скобки:
Message hello2 = () =>
{
    Console.Write("Hello ");
    Console.WriteLine("sweet");
};
hello2();

//  Выше мы определили переменную hello, которая представляет делегат Message. Но начиная
//  с версии C# 10 мы можем применять неявную типизацию (определение переменной с помощью
//  оператора var) при определении лямбда-выражения:

var hello3 = () => Console.WriteLine("Bye");
hello3();

//  Но какой тип в данном случае представляет переменная hello? При неявной типизации компилятор
//  сам пытается сопоставить лямбда-выражение на основе его опеределения с каким-нибудь делегатом.
//  Например, выше определенное лямбда-выражение hello по умолчанию компилятор будет рассматривать
//  как переменную встроенного делегата Action, который не принимает никаких параметров и ничего
//  не возвращает.
#region Параметры лямбды
//  При определении списка параметров мы можем не указывать для них тип данных:
Operation sum = (x, y) => Console.WriteLine($"{x} + {y} = {x + y}");
//  В данном случае компилятор видит, что лямбда-выражение sum представляет тип Operation,
//  а значит оба параметра лямбды представляют тип int. Поэтому никак проблем не возникнет.
//  Однако если мы применяем неявную типизацию, то у компилятора могут возникнуть трудности,
//  чтобы вывести тип делегата для лямбда-выражения, например, в следующем случае

//  var sum = (x, y) => Console.WriteLine($"{x} + {y} = {x + y}");   // ! Ошибка

//  В этом случае можно указать тип параметров
var sum2 = (int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");
sum2(55,23);

//  Если лямбда имеет один параметр, для которого не требуется указывать тип данных,
//  то скобки можно опустить:
PrintMessage mes44 = message => Console.WriteLine(message);
mes44("Around the world");
//  delegate void PrintMessage(string message);   объявлен в конце кода
#endregion

#region Возвращение результата
//  Лямбда-выражение может возвращать результат. Возвращаемый результат можно указать
//  после лямбда-оператора:

var sum3 = (int x, int y) => x + y;
int sum3Result = sum3(33, 44);
Console.WriteLine(sum3Result);

Operation2 mult = (x, y) => x * y;
int multResult = mult(334, 566);
Console.WriteLine(multResult);

//  Если лямбда-выражение содержит несколько выражение, тогда нужно использовать оператор
//  return, как в обычных методах:
var subtract = (int x, int y) =>
{
    if (x > y) return x - y;
    else return y - x;
};
Console.WriteLine();
#endregion

#region Добавление и удаление действий в лямбда-выражении
//  Поскольку лямбда-выражение представляет делегат, тот как и в делегат, в переменную,
//  которая представляет лямбда-выражение можно добавлять методы и другие лямбды:

var _hello = () => Console.WriteLine("***** (-_-) *****");
var _message = () => Console.Write("Hello ");
_message += () => Console.WriteLine("World");
_message += _hello;
_message += Print;

_message();
Console.WriteLine("----- Удалю несколько методов -----");

_message -= Print;
_message -= _hello;

_message?.Invoke();    // на случай, если в message больше нет действий

void Print() => Console.WriteLine("Weclome to C#");
Console.WriteLine();
#endregion

#region Лямбда-выражение как аргумент метода
//  Как и делегаты, лямбда-выражения можно передавать параметрам метода, которые представляют делегат:
int[] interers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

int result = Sum(interers, x => x > 5);   // найдем сумму чисел больше 5
Console.WriteLine(result);

int result2 = Sum(interers, x => (x % 2) == 0);  // найдем сумму четных чисел
Console.WriteLine(result2);

int Sum(int[] numbers, IsEqual func)
{
    int result = 0;
    foreach (int i in numbers)
    {
        if (func(i))          // получаем значение i, сравнивает с условием внутри делегата и возвращает true/false
        {
            result += i;
        }
    }
    return result;
};
#endregion

#region Лямбда-выражение как результат метода
//  Метод также может возвращать лямбда-выражение. В этом случае возвращаемым типом метода выступает делегат,
//  которому соответствует возвращаемое лямбда-выражение.
Operation2 SelectOperation(OperationType opType)
{
    switch (opType)
    {
        case OperationType.Add: return (x, y) => x + y;
        case OperationType.Subtract: return (x, y) => x - y;
        default: return (x, y) => x * y;
    }
}
// Аналогичный вид записи
Operation2 SelectOperation2(OperationType opType) => opType switch
{
    OperationType.Add => (x, y) => x + y, 
    OperationType.Subtract => (x, y) => x + y, 
    _ => (x, y) => x + y
};
#endregion

#region Конец кода
enum OperationType
{
    Add, Subtract, Multiply
}
delegate bool IsEqual(int x);
delegate void Operation(int x, int y);
delegate int Operation2(int x, int y);
delegate void Message();
delegate void PrintMessage(string message);
#endregion

