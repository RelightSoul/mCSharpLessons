// Дополнительные возможности ООП в C#

//  Определение операторов
//  Наряду с методами в классах и структурах мы можем также определять операторы.
//      В частности, мы можем определить логику для следующих операторов:

//      унарные операторы +x, -x, !x, ~x, ++, --, true, false

//      бинарные операторы +, -, *, /, %

//      операции сравнения ==, !=, <, >, <=, >=

//      поразрядные операторы &, |, ^, <<, >>

//      логические операторы &&, ||

//      Кроме того, есть несколько операторов, которые надо определять парами:

//      == и !=

//      < и >

//      <= и >=

class Program
{
    static void Main(string[] args)
    {
        Counter count1 = new Counter { Value = 44 };
        Counter count2 = new Counter { Value = 20 };


        // bool resutl = count1 > count2;          // до определения операций
        //   на данный момент ни операция сравнения, ни операция сложения для объектов Counter не доступны.
        //   Эти операции могут использоваться для ряда примитивных типов.

        bool result = count1 > count2;             //после определения операций
        Console.WriteLine(result);        //true

        Counter count3 = count1 + count2;
        Console.WriteLine(count3.Value);  //64

        int result2 = count1 + 27;        // 71
        Console.WriteLine(result2);

        Console.WriteLine("-----Определение инкремента и декремента-----");
        Counter count4 = new Counter {Value = 10};
        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine(count4.Value++);
        }
        Console.WriteLine("-----Определение true/false-----");
        Counter count5 = new Counter() { Value = 0 };
        if (count5)
            Console.WriteLine(true);
        else
            Console.WriteLine(false);

        if (!count5)
            Console.WriteLine(true);
        else
            Console.WriteLine(false);

    }
}
//  Определение операторов заключается в определении в классе, для объектов которого мы хотим определить оператор,
//  специального метода

//          public static возвращаемый_тип operator оператор(параметры) { }
//
//  Этот метод должен иметь модификаторы public static, так как перегружаемый оператор будет использоваться для
//  всех объектов данного класса.

class Counter
{
    public int Value { get; set; }

    public static Counter operator +(Counter x, Counter y)
    {
        return new Counter { Value = x.Value + y.Value };
    }
    public static bool operator >(Counter x, Counter y)
    {
        return x.Value > y.Value;
    }
    public static bool operator <(Counter x, Counter y)
    {
        return x.Value < y.Value;
    }

    //  Стоит отметить, что так как по сути определение оператора представляет собой метод, то этот метод мы
    //  также можем перегрузить, то есть создать для него еще одну версию.
    public static int operator +(Counter x, int y)
    {
        return  x.Value + y;
    }
    
    //  Следует учитывать, что в коде оператора не должны изменяться те объекты, которые передаются в оператор
    //  через параметры.
    public static Counter operator ++(Counter x)
    {
        return new Counter { Value = x.Value + 1 };
    }
    //  То есть возвращается новый объект, который содержит в свойстве Value инкрементированное значение.
    //  При этом нам не надо определять отдельно операторы для префиксного и для постфиксного инкремента
    //  (а также декремента), так как одна реализация будет работать в обоих случаях.

    //  Отдельно стоит отметить определение операторов true и false. Эти операторы определяются, когда мы
    //  хотим использовать объект типа в качестве условия.
    public static bool operator true(Counter x)
    {
        return x.Value != 0;
    }
    public static bool operator false(Counter x)
    {
        return x.Value == 0;
    }

    //  Операция отрицания фактически синонимична операции false, поэтому содержит аналогичное условие.
    public static bool operator !(Counter x)
    {
        return x.Value == 0;
    }   
}