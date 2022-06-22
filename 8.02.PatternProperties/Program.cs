// Паттерн свойств позволяет сравнивать со значениями определенных свойств объекта.
// Например, пусть у нас будет следующий класс:
class Person
{
    public string Name { get; set; } = "";
    public string Status { get; set; } = "";
    public string Language { get; set; } = "";
}
//  Например, в зависимости от языка пользователя выведем ему определенное сообщение, применив паттерн свойств:
class Program
{
    static void Main()
    {
        Person tom = new Person { Name = "Tom", Status = "user", Language = "english" };
        Person pierre = new Person { Name = "Pierre", Status = "user", Language = "french" };
        Person admin = new Person { Name = "Admin", Status = "admin", Language = "english" };
        Person adminGer = new Person { Name = "Admin", Status = "admin", Language = "german" };

        SayHello(tom);
        SayHello(pierre);

        void SayHello(Person person)
        {
            if (person is Person {Language: "french" })
            {
                Console.WriteLine("Salut");
            }
            else
            {
                Console.WriteLine("Hello");
            }
        }
        //  Здесь метод SayHello в качестве параметра принимает объект Person и сопоставляет его с некоторым
        //  паттерном. В качестве паттерна выступает выражение Person { Language: "french" }. То есть параметр
        //  person должен представлять объект Person, у которого значение свойства Language равно "french".

        // --------------------------------------

        //  При этом можно задействовать набор свойств. Например, добавим проверку по свойству Status:
        SayHello2(admin);    // Hello, admin
        SayHello2(tom);      // Hello
        SayHello2(pierre);   // Salut

        void SayHello2(Person person)
        {
            if (person is Person { Language: "english", Status: "admin"})
            {
                Console.WriteLine("Hello, admin!!");
            }
            else if (person is Person {Language: "french" })
            {
                Console.WriteLine("Salut");
            }
            else
            {
                Console.WriteLine("Hello");
            }
        }
        //  Теперь выражение if проверяет, соответствует ли параметр person объекту Person, у которого
        //  свойства Language и Status имеют определенные значения.

        // --------------------------------------

        //  Подобным образом можно применять паттерн свойств в конструкции switch:
        string GetMessage(Person? p) => p switch
        {
            { Language: "english" } => "Hello",
            { Language: "french" } => "Salut",
            { Language: "german", Status: "admin"} => "Hallo, admin",
            { } => "Undefined",
            null => "null"   // если Person p = null
        };
        Console.WriteLine();
        //  Паттерны свойств предполагают использование фигурных скобок, внутри которых указываются свойства
        //  и через двоеточие их значение {свойство: значение}. И со значением свойства в фигурных скобках
        //  сравнивается свойство передаваемого объекта. При этом в фигурных скобках мы можем указать несколько
        //  свойств и их значений { Language: "german", Status: "admin" } - тогда свойства передаваемого объекта
        //  должны соответствовать всем этим значениям.

        //  Можно оставить пустые фигурные скобки, как в последнем случае { } => "undefined!" - передаваемый
        //  объект будет соответствовать пустым фигурным скобкам, если он не соответствует всем предыдущим
        //  значениям, или например, если его свойства не указаны или имеют значение null.

        // --------------------------------------

        //  Кроме того, мы можем определять в паттерных свойств переменные, передавать этим переменным значения
        //  объекта и использовать при возвращении значения:
        Person pablo = new Person { Language = "spanish", Status = "user", Name = "Pablo" };

        Console.WriteLine(GetMessage2(pablo));
        Console.WriteLine(GetMessage2(pierre));
        Console.WriteLine(GetMessage2(adminGer));
        Console.WriteLine(GetMessage2(tom));

        string GetMessage2(Person? p) => p switch 
        {
            { Language: "english", Name: var name } => $"Hello, {name}",
            { Language: "german", Name: var name} => $"Hallo, {name}",
            { Language: var lang} => $"{lang}",
            null => "null"
        };
        Console.WriteLine();
        //  Так, подвыражение Name: var name говорит, что надо передать в переменную name значение свойства Name.
        //  Затем ее можно применить при генерации выходного значения: => $"Salut, {name}!"

        // --------------------------------------

        //  Стоит отметить, что начиная с версии C# 10 было упрощено сопоставление со свойствами вложенных
        //  объектов. Класс Company определяет свойство Title, которое хранит название компании.
        //  Класс Employee определяет сотрудника компании и в свойстве Company хранит компанию.
        //  Применим паттерн свойств на основе свойств вложенного объекта Company:

        var microsoft = new Company("Microsoft");
        var google = new Company("Google");
        var richard = new Employee("Richard", microsoft);
        var bob = new Employee("Bob", google);

        PrintCompany(richard);
        PrintCompany(bob);

        void PrintCompany(Employee emp)
        {
            if (emp is Employee { Company: { Title: "Microsoft"} })
            {
                Console.WriteLine($"{emp.Name} works in Microsoft");
            }
            else
            {
                Console.WriteLine($"{emp.Name} works somewhere");
            }
        }
        //  В методе PrintCompany объект employee сопоставляется с паттерном
        //  Employee { Company:{Title: "Microsoft" } }. То есть сотрудник компании должен представлять
        //  объект Employee, у которого название компании равно "Microsoft"

        //  Однако мы также можем сократить данный паттерн следующим образом:
        void PrintCompany2(Employee emp)
        {
            if (emp is Employee { Company.Title: "Microsoft"})
            {
                Console.WriteLine($"{emp.Name} works in {emp.Company}");
            }
            else
            {
                Console.WriteLine($"{emp.Name} works somewhere");
            }
        }
    }
}
class Employee
{
    public string Name { get; }
    public Company Company { get; set; }
    public Employee(string name, Company company)
    {
        Name = name;
        Company = company;
    }
}
class Company
{
    public string Title { get; }
    public Company(string title) => Title = title;
}