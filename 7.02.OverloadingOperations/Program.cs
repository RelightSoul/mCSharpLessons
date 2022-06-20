// Перегрузка операций преобразования типов

//  Ранее мы рассматривали явные и неявные преобразования примитивных типов. Например:
//      int x = 50;
//      byte y = (byte)x; // явное преобразование от int к byte
//      int z = y;  // неявное преобразование от byte к int

//  И было бы не плохо иметь возможность определять логику преобразования одних типов в другие.
//  И с помощью перегрузки операторов мы можем это делать. Для этого в классе определяется метод
//  следующей формы:

//      public static implicit|explicit operator Тип_в_который_надо_преобразовать(исходный_тип param)
//      {
//          // логика преобразования
//      }

//  После модификаторов public static идет ключевое слово explicit (если преобразование явное,
//  то есть нужна операция приведения типов) или implicit (если преобразование неявное). Затем
//  идет ключевое слово operator и далее возвращаемый тип, в который надо преобразовать объект.
//  В скобках в качестве параметра передается объект, который надо преобразовать.

class Counter
{
    public int Seconds { get; set; }

    public static implicit operator Counter(int x)
    {
        return new Counter { Seconds = x };
    }
    public static explicit operator int(Counter counter)
    {
        return counter.Seconds;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Counter counter1 = new Counter {Seconds = 25};

        //  Поскольку операция преобразования из Counter в int определена с ключевым словом explicit,
        //  то есть как явное преобразование, то в этом случае необходимо применить операцию приведения типов
        int x = (int)counter1;
        Console.WriteLine(x);

        //  Поскольку операция преобразования из Counter в int определена с ключевым словом explicit, то есть
        //  как явное преобразование, то в этом случае необходимо применить операцию приведения типов
        Counter counter2 = x;
        Console.WriteLine(counter2.Seconds);

        Stopwatch stopwatch = new Stopwatch { Seconds = 5423 };
        Timer z = (Timer)stopwatch;
        Console.WriteLine($"{z.Hours}:{z.Minutes}:{z.Seconds}");        
    }
}

//Следует учитывать, что оператор преобразования типов должен преобразовывать из типа или в тип, в котором этот
//оператор определен. То есть оператор преобразования, определенный в типе Counter, должен либо принимать в
//качестве параметра объект типа Counter, либо возвращать объект типа Counter.

//  Рассмотрим также более сложные преобразования, к примеру, из одного составного типа в другой составной тип.
//  Допустим, у нас есть еще класс Timer:
class Timer
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
}
class Stopwatch
{
    public int Seconds { get; set; }

    public static implicit operator Stopwatch(int x)
    {
        return new Stopwatch { Seconds = x };
    }
    public static explicit operator int(Stopwatch x)
    {
        return x.Seconds;
    }

    public static implicit operator Stopwatch(Timer x)
    {
        int hours = x.Hours * 3600;
        int minutes = x.Minutes * 60;
        return new Stopwatch { Seconds = x.Seconds + hours + minutes};
    }
    public static explicit operator Timer(Stopwatch x)
    {
        int hours = x.Seconds / 3600;
        int minutes = (x.Seconds % 3600) / 60;
        int seconds = x.Seconds % 60;
        return new Timer { Hours = hours, Minutes = minutes, Seconds = seconds};
    }
}
//  Класс Timer представляет условный таймер, который хранит часы, минуты и секунды. Класс Counter
//  представляет условный счетчик-секундомер, который хранит количество секунд. Исходя из этого мы
//  можем определить некоторую логику преобразования из одного типа к другому, то есть получение из
//  секунд в объекте Counter часов, минут и секунд в объекте Timer.