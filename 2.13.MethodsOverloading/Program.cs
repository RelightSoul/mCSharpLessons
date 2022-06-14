// И в языке C# мы можем создавать в классе несколько методов с одним и тем же именем, но разной сигнатурой.
// Сигнатура складывается из следующих аспектов:

//  Имя метода

//  Количество параметров

//  Типы параметров

//  Порядок параметров

//  Модификаторы параметров

//  !! названия параметров в сигнатуру НЕ входят

//  И перегрузка метода как раз заключается в том, что методы имеют разную сигнатуру, в которой
//  совпадает только название метода

class Calculate
{
    public void Add(int a, int b)
    {
        int result = a + b;
        Console.WriteLine(result);
    }
    public void Add(int a,int b, int c)
    {
        int result = a + b +c;
        Console.WriteLine(result);
    }
    public void Add(int a,int b,int c,int d)
    {
        int result = a + b + c + d;
        Console.WriteLine(result);
    }
    public void Add(double a, double b)
    {
        double result = a + b;
        Console.WriteLine(result);
    }
    //int Add(int x, int y)                     Ошибка, совпадает сигнатура Add(int, int)
    //{
    //    return x + y;
    //}
    //int Add(int number1, int number2)         Ошибка, совпадает сигнатура Add(int, int)
    //{
    //    return number1 + number2;
    //}
    //void Add(int x, int y)                    Ошибка, совпадает сигнатура Add(int, int)
    //{
    //    Console.WriteLine(x + y);
    //}
}
//  Мы можем представить сигнатуры данных методов следующим образом

//  Add(int, int)
//  Add(int, int, int)
//  Add(int, int, int, int)
//  Add(double, double)
