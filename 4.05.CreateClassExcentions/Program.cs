// Если нас не устраивают встроенные типы исключений, то мы можем создать свои типы.
// Базовым классом для всех исключений является класс Exception, соответственно для
// создания своих типов мы можем унаследовать данный класс.

class Program
{
    static void Main(string[] args)
    {
        // Пример 1:
        try
        {
            Person tom = new Person {Name= "Tom", Age = 17};
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine();

        // Пример 2:
        try
        {
            Person3 alex = new Person3 { Name = "Alex", Age = 16 };
        }
        catch (PersonException3 ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine($"Возраст: {ex.Value}");
        }

    }
}
class Person
{
    private int age;
    public string Name { get; set; }
    public int Age 
    { 
        get => age; 
        set
        {
            if (value < 18)
            {
                throw new Exception("Запрещена регистрация лиц моложе 18");
            }
            else
            {
                age = value;
            }
        }
    }
}
//  В классе Person при установке возраста происходит проверка, и если возраст меньше 18,
//  то выбрасывается исключение. Класс Exception принимает в конструкторе в качестве
//  параметра строку, которое затем передается в его свойство Message

//  Но иногда удобнее использовать свои классы исключений. Например, в какой-то ситуации
//  мы хотим обработать определенным образом только те исключения, которые относятся к
//  классу Person. Для этих целей мы можем сделать специальный класс PersonException:
class PersonException : Exception
{
    public PersonException(string message) : base(message) { }
}
//  По сути класс кроме пустого конструктора ничего не имеет, и то в конструкторе мы просто
//  обращаемся к конструктору базового класса Exception, передавая в него строку message.
//  Но теперь мы можем изменить класс Person, чтобы он выбрасывал исключение именно этого
//  типа и соответственно в основной программе обрабатывать это исключение:
class Person2
{
    private int age;
    public string Name { get; set; }
    public int Age
    {
        get => age;
        set
        {
            if (value < 18)
                throw new PersonException("Запрещена регистрация лиц моложе 18");        
            else
                age = value;            
        }
    }
}

//  необязательно наследовать свой класс исключений именно от типа Exception, можно в
//  зять какой-нибудь другой производный тип. Например, в данном случае мы можем взять
//  тип ArgumentException, который представляет исключение, генерируемое в результате
//  передачи аргументу метода некорректного значения:

class PersonException2 : ArgumentException
{
    public PersonException2(string message) : base(message) { }
}

//  Каждый тип исключений может определять какие-то свои свойства. Например, в данном случае
//  мы можем определить в классе свойство для хранения устанавливаемого значения:

class PersonException3 : ArgumentException
{
    public int Value { get; }
    public PersonException3(string message, int value) : base(message)
    {
        Value = value;
    }
}

class Person3
{
    private int age;
    public string Name { get; set; }
    public int Age
    {
        get => age;
        set
        {
            if (value < 18)
                throw new PersonException3("Запрещена регистрация лиц моложе 18",value);
            else
                age = value;
        }
    }
}
//  Пример 2 в программ