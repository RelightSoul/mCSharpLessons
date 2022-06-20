//  Класс Celcius представляет градусник по Цельсию, а Fahrenheit - градусник по Фаренгейту.
//  Определите операторы преобразования от типа Celcius и наоборот.
//  Преобразование температуры по шкале Фаренгейта (Tf) в температуру по шкале
//  Цельсия (Tc): Tc = 5 / 9 * (Tf - 32).
//  Преобразование температуры по шкале Цельсия в температуру по шкале Фаренгейта: Tf = 9 / 5 * Tc + 32.
Celcius cels = new Celcius { Gradus = 44 };
Fahrenheit fahr = cels;
Console.WriteLine(fahr.Gradus);

Fahrenheit fahr2 = new Fahrenheit { Gradus = 543 };
Celcius cels2 = fahr2;
Console.WriteLine(cels.Gradus);


class Celcius
{
    public double Gradus { get; set; }

    public static implicit operator Celcius(Fahrenheit f)
    {
        return new Celcius { Gradus = 5.0 / 9.0 * (f.Gradus - 32.0) };
    }
    public static implicit operator Fahrenheit(Celcius f)
    {
        return new Fahrenheit { Gradus = 9.0 / 5.0 * f.Gradus + 32.0 };
    }
}
class Fahrenheit
{
    public double Gradus { get; set; }
}