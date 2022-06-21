// Добавьте в класс оператор сложения, который бы позволял объединять государства. А также операторы сравнения
// < и > для сравнения государств по какому-нибудь критерию (например, по населению или территории). Наподобие

State state1 = new State();
State state2 = new State();
State state3 = state1 + state2;
bool isGreater = state1 > state2;

State volgograd = new State { Population = 1001183, Area = 859 };
State krasnodar = new State { Population = 1107000, Area = 294 };
State union = volgograd + krasnodar;
Console.WriteLine($"Волгоград и Краснодар.\n" +
    $"Общая популяция = {union.Population} чел.,Общая площадь = {union.Area} км2 ");

bool isMorePopulation = volgograd > krasnodar;
if (isMorePopulation)
{
    Console.WriteLine("В Волгограде больше жителей");
}
else
{
    Console.WriteLine("В Волгограде меньше жителей");
}

class State
{
    public decimal Population { get; set; } // население
    public decimal Area { get; set; }       // территория

    public static State operator +(State x, State y)
    {
        return new State
        {
            Population = x.Population + y.Population,
            Area = x.Area + y.Area,
        };
    }

    public static bool operator <(State x, State y)
    {
        return x.Population < y.Population;
    }
    public static bool operator >(State x, State y)
    {
        return x.Population > y.Population;
    }
}