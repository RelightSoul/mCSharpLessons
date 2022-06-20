// Добавьте в один из классов оператор сложения, чтобы при объединении хлеба и масла получался бутерброд,
// и, тем самым, компилировался и выполнялся без ошибок следующий код:

Bread bread = new Bread { Weight = 80 };
Butter butter = new Butter { Weight = 20 };
Sandwich sandwich = bread + butter;
Console.WriteLine(sandwich.Weight);  // 100

class Bread
{
    public int Weight { get; set; } // масса
    public static Sandwich operator +(Bread x, Butter y)
    {
        return new Sandwich { Weight = x.Weight + y.Weight };
    }
}
class Butter
{
    public int Weight { get; set; } // масса
}
class Sandwich
{
    public int Weight { get; set; } // масса
}