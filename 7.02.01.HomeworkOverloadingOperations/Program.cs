//  В программе мы можем из числа получить количество часов и, наоборот, из количество часов значение типа int:

Clock clock = new Clock();
int val = 34;
clock.Hours = val % 24;
val = clock.Hours;

Clock clock2 = new Clock { Hours = 55 };
int x = (int)clock2;
Console.WriteLine(x); // 55
int y = 435;
Clock clock3 = y;
Console.WriteLine(clock3.Hours);  // 3

//  Добавьте в класс Clock оператор для неявного преобразования от типа int к типу Clock, и оператор явного
//  преобразования от типа Clock к типу int.

class Clock
{
    public int Hours { get; set; }
    public static implicit operator Clock(int x)
    {
        return new Clock { Hours = x % 24 };
    }
    public static explicit operator int(Clock x)
    {
        return x.Hours;
    }
}
