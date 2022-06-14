//  Перечисления enum

//  Кроме примитивных типов данных в языке программирования C# есть такой тип как enum или перечисление.
//  Перечисления представляют набор логически связанных констант.

//      enum название_перечисления
//      {
//            // значения перечисления
//            значение1,
//            значение2,
//            .......
//            значениеN
//      }

//  Каждое перечисление фактически определяет новый тип данных, с помощью которых мы также, как и с помощью
//  любого другого типа, можем определять переменные, константы, параметры методов и т.д. В качестве значения
//  переменной, константы и параметра метода, которые представляют перечисление, должна выступать одна из
//  констант этого перечисления, например:

DayTime dayTime = DayTime.Morning;
if (dayTime == DayTime.Morning)
{
    Console.WriteLine("Доброе утро");
}
else
{
    Console.WriteLine("Привет");
}
//enum DayTime
//{
//    Morning,
//    Afternon,
//    Evening,
//    Night
//}

#region Хранение состояния
DayTime now = DayTime.Evening;

PrintMessage(now);                  // Добрый вечер
PrintMessage(DayTime.Afternoon);    // Добрый день
PrintMessage(DayTime.Night);        // Доброй ночи

void PrintMessage(DayTime dayTime)
{
    switch (dayTime)
    {
        case DayTime.Morning:
            Console.WriteLine("Доброе утро");
            break;
        case DayTime.Afternoon:
            Console.WriteLine("Добрый день");
            break;
        case DayTime.Evening:
            Console.WriteLine("Добрый вечер");
            break;
        case DayTime.Night:
            Console.WriteLine("Доброй ночи");
            break;
    }
}
//enum DayTime
//{
//    Morning,
//    Afternoon,
//    Evening,
//    Night
//}

//  Другой пример:
DoOperation(10, 5, Operation.Add);          // 15
DoOperation(10, 5, Operation.Subtract);     // 5
DoOperation(10, 5, Operation.Multiply);     // 50
DoOperation(10, 5, Operation.Divide);

void DoOperation(double x, double y, Operation op)
{
    double result = op switch
    {
        Operation.Add => x + y,
        Operation.Subtract => x - y,
        Operation.Multiply => x * y,
        Operation.Divide => x / y
    };
    Console.WriteLine(result);
}
//enum Operation
//{
//    Add,
//    Subtract,
//    Multiply,
//    Divide
//}
#endregion

#region Тип и значения констант перечисления
//  Константы перечисления могут иметь тип. Тип указывается после названия перечисления через двоеточие:
enum Time : byte
{
    Morning,
    Afternoon,
    Evening,
    Night
}
//  Тип перечисления обязательно должен представлять целочисленный тип (byte, sbyte, short, ushort, int,
//  uint, long, ulong). Если тип явным образом не указан, то по умолчанию используется тип int.
//  По умолчанию каждому элементу перечисления присваивается целочисленное значение, причем первый
//  элемент будет иметь значение 0, второй - 1 и так далее. 

//  Мы можем использовать операцию приведения, чтобы получить целочисленное значение константы перечисления:
//  (int) DateTime.Night   // 3
//  В то же время, несмотря на то, что каждая константа сопоставляется с определенным числом,
//  !! мы НЕ можем присвоить ей числовое значение

//  Можно также явным образом указать значения элементов, либо указав значение первого элемента:

//enum Operation
//{
//    Add = 3,
//    Subtract,
//    Multiply,
//    Divide = 16
//}

//  При этом константы перечисления могут иметь одинаковые значения, либо даже можно присваивать одной
//  константе значение другой константы:

//enum DayTime
//{
//    Morning = 1,
//    Afternoon = 2,
//    Evening = Morning,
//    Night = 2
//}
#endregion

enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide
}
enum DayTime
{
    Morning,
    Afternoon,
    Evening,
    Night
}