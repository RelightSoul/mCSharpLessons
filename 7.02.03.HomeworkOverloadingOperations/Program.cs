// Определите операторы преобразования от типа Dollar в Euro и наоборот.

// Допустим, 1 евро стоит 1,14 долларов. При этом один оператор должен подразумевать явное,
// и один - неявное преобразование.

Dollar dWallet = new Dollar { Sum = 5000 };
Euro eWallet = (Euro)dWallet;
Console.WriteLine(eWallet.Sum);

Euro eWallet2 = new Euro { Sum = 3000 };
Dollar dWallet2 = eWallet2;
Console.WriteLine(dWallet2.Sum);

class Dollar
{
    public decimal Sum { get; set; }
    public static implicit operator Dollar(Euro e)
    {
        return new Dollar { Sum = e.Sum * 1.14m };
    }
    public static explicit operator Euro(Dollar d)
    {
        return new Euro { Sum = d.Sum / 1.14m };
    }
}
class Euro
{
    public decimal Sum { get; set; }
}
