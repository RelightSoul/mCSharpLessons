// Математические вычисления и класс Math

//  Для выполнения различных математических операций в библиотеке классов .NET предназначен класс Math.
//  Он является статическим, поэтому все его методы также являются статическими.

//  Рассмотрим основные методы класса Math:

//  Abs(double value): возвращает абсолютное значение для аргумента value
double result = Math.Abs(-12.4); // 12.4

//  Acos(double value): возвращает арккосинус value. Параметр value должен иметь значение от -1 до 1
double result2 = Math.Acos(1); // 0

//Asin(double value): возвращает арксинус value. Параметр value должен иметь значение от -1 до 1

//Atan(double value): возвращает арктангенс value

//BigMul(int x, int y): возвращает произведение x * y в виде объекта long
double result3 = Math.BigMul(100, 9340); // 934000

//Ceiling(double value): возвращает наименьшее целое число с плавающей точкой, которое не меньше value
double result4 = Math.Ceiling(2.34); // 3

//Cos(double d): возвращает косинус угла d

//Cosh(double d): возвращает гиперболический косинус угла d

//DivRem(int a, int b, out int result): возвращает результат от деления a/b, а остаток помещается в параметр
//result
int result5;
int div = Math.DivRem(14, 5, out result5);
// result5 = 4
// div = 2

//Exp(double d): возвращает основание натурального логарифма, возведенное в степень d

//Floor(decimal d): возвращает наибольшее целое число, которое не больше d
double result6 = Math.Floor(2.56); // 2

//IEEERemainder(double a, double b): возвращает остаток от деления a на b
double result7 = Math.IEEERemainder(26, 4); // 2 = 26-24

//Log(double d): возвращает натуральный логарифм числа d

//Log(double a, double newBase): возвращает логарифм числа a по основанию newBase

//Log10(double d): возвращает десятичный логарифм числа d

//Max(double a, double b): возвращает максимальное число из a и b

//Min(double a, double b): возвращает минимальное число из a и b

//Pow(double a, double b): возвращает число a, возведенное в степень b

//Round(double d): возвращает число d, округленное до ближайшего целого числа
double result8 = Math.Round(20.56); // 21
double result9 = Math.Round(20.46); //20

//Round(double a, int b): возвращает число a, округленное до определенного количества знаков после запятой,
//представленного параметром b
double result10 = Math.Round(20.567, 2); // 20,57
double result11 = Math.Round(20.463, 1); //20,5

//Sign(double value): возвращает число 1, если число value положительное, и -1, если значение value отрицательное.
//Если value равно 0, то возвращает 0
int result12 = Math.Sign(15); // 1
int result13 = Math.Sign(-5); //-1

//Sin(double value): возвращает синус угла value

//Sinh(double value): возвращает гиперболический синус угла value

//Sqrt(double value): возвращает квадратный корень числа value
double result14 = Math.Sqrt(16); // 4

//Tan(double value): возвращает тангенс угла value

//Tanh(double value): возвращает гиперболический тангенс угла value

//Truncate(double value): отбрасывает дробную часть числа value, возвращаяя лишь целое значние
double result15 = Math.Truncate(16.89); // 16

//Также класс Math определяет две константы: Math.E и Math.PI. Например, вычислим площадь круга:
double radius = 50;
double area = Math.PI * Math.Pow(radius, 2);
Console.WriteLine($"Площадь круга с радиусом {radius} равна {Math.Round(area, 2)}");
//Площадь круга с радиусом 50 равна 7853,98