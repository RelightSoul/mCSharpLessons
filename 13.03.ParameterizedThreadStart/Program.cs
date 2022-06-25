// Потоки с параметрами и ParameterizedThreadStart

//  В предыдущей статье было рассмотрено, как запускать в отдельных потоках методы без параметров. А что,
//  если нам надо передать какие-нибудь параметры в поток?

//  Для этой цели используется делегат ParameterizedThreadStart, который передается в конструктор класса Thread:

//      public delegate void ParameterizedThreadStart(object? obj);

//  Применение делегата ParameterizedThreadStart во многом похоже на работу с ThreadStart. Рассмотрим на примере:

// создаем новые потоки
Thread myThread1 = new Thread(new ParameterizedThreadStart(Print));
Thread myThread2 = new Thread(Print);
Thread myThread3 = new Thread(message => Console.WriteLine(message));

// запускаем потоки
myThread1.Start("Hello");
myThread2.Start("Привет");
myThread3.Start("Salut");

void Print(object? message) => Console.WriteLine(message);
//  При создании потока в конструктор класса Thread передается объект делегата ParameterizedThreadStart
//  new Thread(new ParameterizedThreadStart(Print)), либо непосредственно метод, который соответствует
//  этому делегату (new Thread(Print)), в том числе в виде лямбда-выражения (new Thread(message =>
//  Console.WriteLine(message)))

//  Затем при запуске потока в метод Start() передается значение, которое передается параметру метода Print. 

//  При использовании ParameterizedThreadStart мы сталкиваемся с ограничением: мы можем запускать во втором
//  потоке только такой метод, который в качестве единственного параметра принимает объект типа object?.
//  Поэтому если мы хотим использовать данные других типов, в самом методе необходимо выполнить приведение
//  типов. Например:

int number = 4;
Thread thread = new Thread(Print2);
thread.Start(number);

void Print2(object? obj)
{
    if (obj is int n)
    {
        Console.WriteLine(n * n);
    }
}
//  в данном случае нам надо дополнительно привести переданное значение к типу int, чтобы его
//  использовать в вычислениях.

//  Но что делать, если нам надо передать не один, а несколько параметров различного типа?
//  В этом случае можно определить свои типы:
Thread thread2 = new Thread(PrintPerson);
thread2.Start(new Person("Alex", 44));
void PrintPerson(object? obj)
{
    if (obj is Person p)
    {
        Console.WriteLine(p.Name + " - " + p.Age);
    }
}
//  Сначала определяем специальный класс Person, объект которого будет передаваться во второй
//  поток, а в методе Main передаем его во второй поток.

//  Но тут опять же есть одно ограничение: метод Thread.Start не является типобезопасным, то есть мы можем
//  передать в него любой тип, и потом нам придется приводить переданный объект к нужному нам типу.Для
//  решения данной проблемы рекомендуется объявлять все используемые методы и переменные в специальном
//  классе, а в основной программе запускать поток через ThreadStart. Например:

Person2 tom = new Person2("Tom", 37);
// создаем новый поток
Thread myThread = new Thread(tom.Print);
myThread.Start();

record class Person2(string Name, int Age)
{
    public void Print()
    {
        Console.WriteLine($"Name = {Name}");
        Console.WriteLine($"Age = {Age}");
    }
}

record Person(string Name, int Age);