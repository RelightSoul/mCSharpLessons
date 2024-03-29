﻿byte a = 4;
int b = a + 70;

byte a1 = 4;
byte b1 = (byte)(a1 + 70);  //Дело в том, что операция сложения (да и вычитания) возвращает
                            //значение типа int, если в операции участвуют целочисленные типы
                            //данных с разрядностью меньше или равно int.

// (тип_данных_в_который_надо_преобразовать)значение_для_преобразования;

//Сужающие и расширяющие преобразования

byte a2 = 4;
ushort b2 = a; // неявное расширяющие преобразование

ushort a3 = 4;
byte b3 = (byte)a3; // явное сужающее преобразование

//При явных преобразованиях (explicit conversion) мы сами должны
//применить операцию преобразования (операция ())

//Можно преобразовать неявно, в обратную сторону явно.
//byte -> short -> int -> long -> decimal

//int -> double

//short -> float -> double

//char -> int

//Сформулирую своими словами. Если область значения типа в который преобразуется
//поступающий тип включает его область, то преобразование неявное.
//Иначе нужно объявлять явное, как согласие на преобразование.

//Исключение:   double и decimal, double нужно явно приводить к типу decimal
double curDouble = 4.0;
decimal curDecimal = (decimal)curDouble;

// Потеря данных и ключевое слово checked
int numbA = 33;
int numbB = 600;
byte myByte = (byte)(numbA+numbB);
Console.WriteLine(myByte); // 121   Переполнение. 633 не попадает в допустимый
                           // диапазон для типа byte, и старшие биты будут усекаться

//Чтобы избежать подобных ситуаций, в c# имеется ключевое слово checked
try
{
    int numbA1 = 33;
    int numbB1 = 600;
    byte myByte1 = checked((byte)(numbA1 + numbB1));
    Console.WriteLine(myByte1);
}
catch (OverflowException ex)
{
    Console.WriteLine(ex.Message);    
}
//При использовании ключевого слова checked приложение выбрасывает исключение о переполнении.
//Поэтому для его обработки в данном случае используется конструкция try...catch.
