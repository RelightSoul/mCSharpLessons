// Records представляют новый ссылочный тип, который появился в C#9. Ключевая особенность records
// состоит в том, что они могут представлять неизменяемый (immutable) тип, который по умолчанию
// обладает рядом дополнительных возможностей по сравнению с классами и структурами. Зачем нам нужны
// неизменяемые типы? Такие типы более безопасны в тех ситуациях, когда нам надо гарантировать, что
// данные объекта не будут изменяться. В .NET в принципе уже есть неизменяемые типы, например, String.

//  Стоит отметить, что начиная с версии C# 10 добавлена поддержка структур record, соответственно мы
//  можем создавать record-классы и record-структуры.

//  Для определения records используется ключевое слово record. Если определяется класс record, то ключевое
//  слово class можно неиспользовать при определении типа:

public record Person
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;
    }
}
//одно и тоже
public record class Person2
{
    public string Name { get; set; }
    public Person2(string name)
    {
        Name = name;
    }
}
//При определении структуры record при объявлении типа надо использовать ключевое слово struct:
public record struct Person3
{
    public string Name { get; set; }
    public Person3(string name)
    {
        Name = name;
    }
}

//Хотя типы record предназначены для создания неизменяемых типов, однако одно только применение ключевого
//слова record не гарантирует неизменяесть объектов record. Они являются неизменяемыми (immutable) только
//при определенных условиях. Например, мы можем написать так:
class Program
{
    static void Main(string[] args)
    {
        // -----------Set-------------
        var person = new Person("Tom");
        person.Name = "Bob";
        Console.WriteLine(person.Name); //Bob  - изменилось

        // -----------Init-------------
        var person4 = new Person4("Tom");
        // person4.Name = "Bob";    // ! ошибка - свойство изменить нельзя

        // -----------Сравнение на равенство-------------
        var person1 = new Person("Sam");
        var person2 = new Person("Sam");
        Console.WriteLine(person1.Equals(person2));  //true
        // В данном случае при сравнении двух объектов record Person мы увидим, что они равны, так как их
        // значения (значения свойств Name) равны. Cравнение records производится по значению.

        var user1 = new User("Tom");
        var user2 = new User("Tom");
        Console.WriteLine(user1.Equals(user2));     // false
        // В случае с объектами класса User, которые имеют те же одинаковые значения мы увидим, что они не равны.
        // Сравниваются ссылки на объект.

        // Кроме того, для record уже по умолчанию реализованы операторы == и !=, которые также сравнивают две
        // record по значению:
        Console.WriteLine(person1 == person2); //true

        Console.WriteLine(user1 == user2); //false

        // -----------Оператор with-------------
        var mary = new Person5("Mary", 21);
        var samuel = mary with {Name = "Samuel"};
        Console.WriteLine($"{samuel.Name} - {samuel.Age}");
        // После record, значения которой мы хотим скопировать, указывается оператор with, после которого в
        // фигурных скобках указываются значения для тех свойств, которые мы хотим изменить. Так, в данном
        // случае переменная sam получает для свойства Age значение из tom, а свойство Name изменяется.

        // Эта возможность может быть особенно актуальна, если в record, которую мы хотим скопировать, множество
        // свойств, из которых мы хотим поменять одно - два.

        //  Если надо скопировать значения всех свойств, то можно оставить пустые фигурные скобки
        var mary2 = mary with { };
        Console.WriteLine(mary2.Name);

        // -----------Позиционные records-------------
        var person6 = new Person6("Mark",40);
        Console.WriteLine(person6.Name);
        var(name, age) = person6;
        Console.WriteLine(name);

        // -----------public record struct Person7(string Name, int Age);-------------
        var person7 = new Person7("Clara",15);
        Console.WriteLine(person7.Name);
        var(name7, age7) = person7;
        Console.WriteLine($"{name7}, {age7}");

        var person8 = new Person8("Clara", 15) { Company = "Microsoft" };
        Console.WriteLine(person8.Company);

        // -----------ToString-------------
        Console.WriteLine(person1);
        Console.WriteLine(person4);
        Console.WriteLine(person7);
    }
}
//  При выполнении этого кода не возникнет никакой ошибки, мы спокойно сможем изменять значения свойств объекта
//  Person. Чтобы сделать его действительно неизменяемым, для свойств вместо обычных сеттеров надо использовать
//  модификатор init.
public record Person4
{
    public string Name { get; init; }
    public Person4(string name)
    {
        Name=name;
    }
}
//  Во многим records похожи на обычные классы и структуры, например, они могут абстрактными, их также можно
//  наследовать либо запрещать наследование с помощью оператора sealed. Тем не менее есть и ряд отличий.
//  Рассмотрим некоторые основные отличия records от стандартных классов и структур.

#region Сравнение на равенство
//  При определении record компилятор генерирует метод Equals() для сравнения с другим объектом. При этом
//  сравнение двух records производится на основе их значений. Например, рассмотрим следующий пример:
public class User
{
    public string Name { get; init; }
    public User(string name) => Name = name;
}
// Пример в программ
#endregion

#region Оператор with
//  В отличие от классов records поддерживают инициализацию с помощью оператора with. Он позволяет создать
//  одну record на основе другой record. Пример в программ
public record Person5
{
    public string Name { get; init; }
    public int Age { get; init; }
    public Person5(string name,int age)
    {
        Name = name;
        Age = age;
    }
}
#endregion

#region Позиционные records
//  Records могут принимать данные для свойств через конструктор, и в этом случае мы можем сократить их
//  определение. Например, пусть у нас есть следующая record Person:
public record struct Person6
{
    public string Name { get; init; }
    public int Age { get; init; }
    public Person6(string name, int age)
    {
        Name = name; Age = age;
    }
    public void Deconstruct(out string name, out int age) => (name, age) = (Name, Age);
}
//  Кроме конструктора здесь реализован деконструктор, который позволяет разложить объект Person на кортеж
//  значений. И мы могли бы применить ее, например, следующим образом, пример в программ.

//  Выше определенную record Person6 можно сократить до позиционной record:
public record struct Person7(string Name, int Age);
//  Это все определение типа. То есть мы говорим, что для типа Person будет создаваться конструктор,
//  который принимает два параметра и присваивает их значения соответственно свойствам Name и Age,
//  и что также автоматически будет создаваться деконструктор. Ее использование будет аналогично.
//  Пример в программ

//  При необходимости также можно совмещать стандартное определение свойств и определение свойств через
//  конструктор:
public record Person8(string Name, int Age)
{
    public string Company { get; set; } = "";
}
#endregion

#region Позиционные структуры для чтения
//  Следует отметить различие между позиционными классами и структурами record. Свойства класса record,
//  которые устанавливаются через параметры конструктора, по умолчанию будут иметь модификатор init. То
//  есть после установки их значений через конструктор, мы больше не сможем их изменить:

//  var person = new Person("Tom", 37);
//  person.Name = "Bob";    // ! Ошибка - значение нельзя изменить

//  public record Person(string Name, int Age);

//  Стоит отметить, что это относится только к тем свойствам, которые устанавливаются через конструктор.

//  Однако для позиционных структур record свойства будут иметь стандартные сеттеры, которые позволят изменять
//  значения свойств:

//  var person = new Person("Tom", 37);
//  person.Name = "Bob";
//  Console.WriteLine(person.Name); // Bob - значение изменилось
//  // структура record
//  public record struct Person(string Name, int Age);

//  Чтобы для подобных свойств структуры record использовался модификатор init вместо обычных сеттеров,
//  такую структуру надо определить с ключевым словом readonly:

//  var person = new Person("Tom", 37);
//  person.Name = "Bob";    // ! Ошибка - значение свойства нельзя изменить

//  // структура record доступна только для чтения
//  public readonly record struct Person(string Name, int Age);
#endregion

#region ToString
//  Небольшим преимуществом типов record также является то, что для них уже по умолчанию реализован метод
//  ToString(), который выводит состояние объекта в отформатированном виде. Пример в программ
#endregion

#region Наследование
//  Как и обычные классы record-классы могут наследоваться:
public record Person9(string Name, int Age);
public record Employee(string Name, int Age, string Company) : Person9(Name, Age);
//  В данном случае класс record Employee наследуется от Person.
#endregion