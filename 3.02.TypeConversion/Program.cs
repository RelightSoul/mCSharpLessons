//  Мы уже говорили о преобразованиях объектов простых типов.
//  Сейчас затронем тему преобразования объектов классов.
//  Описание классов и их цепи наследования в конце проекта

class Program
{
    static void Main(string[] args)
    {
        #region Восходящие преобразования. Upcasting
        //  Объекты производного типа (который находится внизу иерархии) в то же время представляют и
        //  базовый тип. Например, объект Employee в то же время является и объектом класса Person.
        //  Что в принципе естественно, так как каждый сотрудник (Employee) является человеком (Person).
        //  И мы можем написать, например, следующим образом:
        Employee emp = new Employee("Tom", "Microsoft");
        Person pers = emp;          // преобразование от Employee к Person
        Console.WriteLine(pers.Name);
        emp.Name = "Paul";
        Console.WriteLine(pers.Name);
        //  Переменные employee и person будут указывать на один и тот же объект в памяти, но переменной
        //  person будет доступна только та часть, которая представляет функционал типа Person.
        #endregion

        #region Нисходящие преобразования. Downcasting
        //  Кроме восходящих преобразований от производного к базовому типу есть нисходящие преобразования
        //  или downcasting - от базового типа к производному. Например, в следующем коде переменная person
        //  хранит ссылку на объект Employee:

        //  Автоматически такие преобразования не проходят, ведь не каждый человек(объект Person) является
        //  сотрудником предприятия(объектом Employee).И для нисходящего преобразования необходимо применить
        //  явное преобразование, указав в скобках тип, к которому нужно выполнить преобразование:
        Employee epm3 = new Employee("Mark", "BMW");
        Person pers3 = epm3;
        Employee emp4 = (Employee)pers3;

        Person pers4 = new Employee("Roman", "Mustang");
        Employee epm5 = (Employee)pers4;

        //Person pers2 = new Person("Sam");
        //Employee emp2 = (Employee)pers2;    //Ошибка pers2 не являлся объектом Employee.

        //  В данном случае мы пытаемся преобразовать объект типа Person к типу Employee, а объект
        //  Person не является объектом Employee. Причем Visual Studio не подскжет,
        //  что в данной строке ошибка, и данная строка даже нормально скомилируется, тем не менее
        //  в процессе выполнения программы мы получим ощибку. В этом в том числе и кроектся коварство
        //  преобразований, поэтому в подобных ситуациях надо проявлять осторожность.

        //  Существует ряд способов, чтобы избежать подобных ошибок преобразования.
        #endregion

        #region Способы преобразований  as  /  is
        //  Можно использовать ключевое слово as. С помощью него программа пытается преобразовать
        //  выражение к определенному типу, при этом не выбрасывает исключение. В случае неудачного
        //  преобразования выражение будет содержать значение null:
        Person pers5 = new Person("Paul");

        Employee? emp6 = pers5 as Employee;
        if (emp6 == null)
        {
            Console.WriteLine("Неудачно");
        }
        else
        {
            Console.WriteLine(emp6.Company);
        }

        //  Второй способ заключается в проверке допустимости преобразования с помощью ключевого слова is:
        if (pers5 is Employee emp33)
        {
            Console.WriteLine(emp33.Company);
        }
        else
        {
            Console.WriteLine("Недопустимо");
        }

        //  Оператор is также можно применять и без преобразования, просто проверяя на соответствие типу
        if (pers5 is Employee)
        {
            Console.WriteLine("Является типом Employee");
        }
        else
        {
            Console.WriteLine("Не является типом Employee");
        }
        #endregion
    }
}

class Person
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;
    }
    public void Print()
    {
        Console.WriteLine(Name);
    }
}

class Employee : Person
{
    public string Company { get; set; }
    public Employee(string name, string company) : base(name)
    {
        Company = company;
    }
}

class Client : Person
{
    public string Bank { get; set; }
    public Client(string name, string bank) : base(name)
    {
        Bank = bank;
    }
}
//  мы можем проследить следующую цепь наследования:
//  Object (все классы неявно наследуются от типа Object) -> Person -> Employee|Client
