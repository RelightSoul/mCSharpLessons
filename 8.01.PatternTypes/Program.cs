//  Паттерн типов

//  Pattern matching фактически выполняет сопоставление некоторого значения с некоторым шаблоном. И если
//  сопоставление прошло успешно, то выполняются определенные действия. Язык C# позволяет выполнять различные
//  типы сопоставлений.

//  Паттерн типов или type pattern позволяет проверить некоторое значение на соответствие некоторому типу:

//          значение is тип переменная_типа

//  Например, у нас есть следующие классы:
class Employee
{
    public virtual void Work() => Console.WriteLine("Employee works");
}
class Manager : Employee
{
    public bool IsOnVacation { get; set; }
    public override void Work()
    {
        Console.WriteLine("Manager works");
    }
}

//  С помощью паттерна типов проверим, представляет ли объект Employee класс Manager:
class Program
{
    static void Main(string[] args)
    {
        Employee tom = new Manager();
        UseEmployee(tom);
        
        void UseEmployee(Employee emp)
        {
            if (emp is Manager manager && manager.IsOnVacation == false)
            {
                manager.Work();
            }
            else
            {
                Console.WriteLine("Передаваемый не опознан");
            }
        }
        // Здесь в методе UseEmployee значение emp сопоставляется с типом Manager. То есть в данном случае
        // в качестве шаблона выступает тип Manager. Если сопоставление прошло успешно (то есть значение
        // emp представляет тип Manager), в переменной manager оказывается объект emp. И далее мы можем
        // вызвать у него методы и свойства.

        //  Также мы можем использовать constant pattern - сопоставление с некоторой константой:
        var message = "Hello";
        if (message is "Hello")
        {
            Console.WriteLine("Привет");
        }

        //  Подобным образом, например, можно проверить значение на null:
        Employee? bob = new Employee();
        Employee? maks = null;
        UseEmployee2(bob);
        UseEmployee2(maks);

        void UseEmployee2(Employee emp)
        {
            if (emp is not null)
            {
                emp.Work();
            }
        }

        //  Кроме конструкции if сопоставление паттернов может применяться в конструкции switch:
        UseEmployee3(tom);  //Manager works
        UseEmployee3(bob);  //Object not a manager
        UseEmployee3(maks); //Object is null

        void UseEmployee3(Employee emp)
        {
            switch (emp)
            {
                case Manager manager:
                    manager.Work();
                    Console.WriteLine("Manager works");
                    break;
                case null:
                    Console.WriteLine("Object is null");
                    break;
                default:
                    Console.WriteLine("Object not a manager");
                    break;
            }
        }

        //  С помощью выражения when можно вводить дополнительные условия в конструкцию case:
        void UseEmployee4(Employee emp)
        {
            switch (emp)
            {
                //case Manager manager when manager.IsOnVacation == false:
                case Manager manager when !manager.IsOnVacation:
                    manager.Work();
                    Console.WriteLine("Manager works");
                    break;
                case null:
                    Console.WriteLine("Object is null");
                    break;
                default:
                    Console.WriteLine("Object not a manager");
                    break;
            }
        }
        //  В этом случае опять же преобразуем объект emp в объект типа Manager и в случае удачного
        //  преобразования смотрим на значение свойства IsOnVacation: если оно равно false, то выполняется
        //  данный блок case.
    }
}

