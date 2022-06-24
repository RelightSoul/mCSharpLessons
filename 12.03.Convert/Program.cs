// Преобразование типов и класс Convert

#region Методы Parse и TryParse
//  Все примитивные типы имеют два метода, которые позволяют преобразовать строку к данному типу.
//  Это методы Parse() и TryParse().

//  Метод Parse() в качестве параметра принимает строку и возвращает объект текущего типа. Например:
using System.Globalization;

int a = int.Parse("34");
double d = double.Parse("334,3");
decimal c = decimal.Parse("12,45");
byte b = byte.Parse("4");
//  Стоит отметить, что парсинг дробных чисел зависит от настроек текущей культуры. В частности, для
//  получения числа double я передаю строку "23,56" с запятой в качестве разделителя. Если бы я передал
//  точку вместо запятой, то приложение выдало ошибку выполнения. На компьютерах с другой локалью,
//  наоборот, использование запятой вместо точки выдало бы ошибку.

//  Чтобы не зависеть от культурных различий мы можем установить четкий формат с помощью класса
//  NumberFormatInfo и его свойства NumberDecimalSeparator:

IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
double d2 = double.Parse("334.3", formatter);
double d3 = double.Parse("334,3");
Console.WriteLine(d2 + " = " + d3);

//  В данном случае в качестве разделителя устанавливается точка. Однако тем не менее потенциально при
//  использовании метода Parse мы можем столкнуться с ошибкой, например, при передачи алфавитных символов
//  вместо числовых. И в этом случае более удачным выбором будет применение метода TryParse(). Он пытается
//  преобразовать строку к типу и, если преобразование прошло успешно, то возвращает true. Иначе возвращается
//  false:

Console.WriteLine();
Console.WriteLine("Введите строку: ");
string? input = Console.ReadLine();

bool result = int.TryParse(input, out a);
if (result == true)
{
    Console.WriteLine(a);
}
else if (result == false)
{
    Console.WriteLine("Неудачно");
}
//  Если преобразование пройдет неудачно, то исключения никакого не будет выброшено, просто метод TryParse
//  возвратит false, а переменная number будет содержать значение по умолчанию.
#endregion

#region Convert
//Класс Convert представляет еще один способ для преобразования значений. Для этого в нем определены
//следующие статические методы:

//ToBoolean(value)
//ToByte(value)
//ToChar(value)
//ToDateTime(value)
//ToDecimal(value)
//ToDouble(value)
//ToInt16(value)
//ToInt32(value)
//ToInt64(value)
//ToSByte(value)
//ToSingle(value)
//ToUInt16(value)
//ToUInt32(value)
//ToUInt64(value)

//  В качестве параметра в эти методы может передаваться значение различных примитивных типов,
//  необязательно строки:
int n = Convert.ToInt32("345");
double doub = Convert.ToDouble(true);
Console.WriteLine(n +" "+ doub);

//  Однако опять же, как и в случае с методом Parse, если методу не удастся преобразовать значение к нужному
//  типу, то он выбрасывает исключение FormatException.
#endregion