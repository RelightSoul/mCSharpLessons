//  В языке C# предусмотрены специальные методы доступа, которые называют свойства.
//  Они обеспечивают простой доступ к полям классов и структур, узнать их значение
//  или выполнить их установку.

#region Определение свойств

//      [модификаторы] тип_свойства название_свойства
//      {
//           get { действия, выполняемые при получении значения свойства}
//           set { действия, выполняемые при установке значения свойства}
//      }

Person person = new Person();
person.Name = "T om";

string persName = person.Name;
Console.WriteLine(persName);

class Person
{
    private string name = "Undefined";

    public string Name
    {
        get { return name; }
        set
        {
            if (value.Contains(" "))
            {
                Console.WriteLine("Имя не должно содержать пробелы");
            }
            else
            {
                name = value;
            }
        }
    }

}
#endregion

#region Свойства только для чтения и записи
//  Блоки set и get не обязательно одновременно должны присутствовать в свойстве.
//  Если свойство определяет только блок get, то такое свойство доступно только для
//  чтения - мы можем получить его значение, но не установить.

//  И, наоборот, если свойство имеет только блок set, тогда это свойство доступно
//  только для записи - можно только установить значение, но нельзя получить
#endregion

#region Вычисляемые свойства
//  Свойства необзательно связаны с определенной переменной. Они могут вычисляться на основе различных выражений
class Person2
{
    string firstName;
    string lastName;
    public string Name
    {
        get { return $"{firstName} {lastName}"; }
    }
    public Person2(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}
#endregion

#region Модификаторы доступа
class Person3
{
    string name = "";
    public string Name
    {
        get { return name; }

        private set { name = value; }
        //можем использовать только в данном классе - в его методах, свойствах, конструкторе,
        //но никак не в другом классе
    }
    public Person3(string name) => Name = name;
}
//  Модификатор для блока set или get можно установить, если свойство имеет оба блока (и set, и get)

//  Только один блок set или get может иметь модификатор доступа, но не оба сразу

//  Модификатор доступа блока set или get должен быть более ограничивающим, чем модификатор доступа свойства.
//  Например, если свойство имеет модификатор public, то блок set/get может иметь только модификаторы
//  protected internal, internal, protected, private protected и private
#endregion

#region Автоматические свойства
class Person4
{
    //  Тут также создаются поля для свойств, только их создает не программист в коде, а компилятор
    //  автоматически генерирует при компиляции.
    public string Name { get; set; }
    public int Age { get; set; } = 33; //Автосвойствам можно присвоить значения по умолчанию(инициализация автосвойств)
    //  Нельзя создать автоматическое свойство только для записи, но можно только для чтения
    public Person4(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
#endregion

#region Блок init
// Начиная с версии C# 9.0 сеттеры в свойствах могут определяться с помощью оператора init
// (от слова "инициализация" - это есть блок init призван инициализировать свойство).
// Для установки значений свойств с init можно использовать только инициализатор, либо конструктор,
// либо при объявлении указать для него значение. После инициализации значений подобных
// свойств их значения изменить нельзя - они доступны только для чтения.
// В этом плане init-свойства сближаются со свойствами для чтения. Разница состоит в том,
// что init-свойства мы также можем устанавить в инициализаторе (свойства для чтения устанавить
// в инициализаторе нельзя). Например:

//  Person5 person5 = new Person5();

//  person5.Name == "Bob";              //! Ошибка - после инициализации изменить значение нельзя

//  Console.WriteLine(person5.Name);    // Undefined
public class Person5
{
    public string Name { get; init; } = "Undefined";
}
//  Установить свойство init можно несолькими способами

//  По умолчанию как показано выше

//  Через конструктор
//  Person6 person = new("Tom");
public class Person6
{
    public string Name { get; init; }
    public Person6(string name)
    {
        Name = name;
    }
}

//  Через инициализатор
//  Person7 person = new() { Name = "Bob" };
public class Person7
{
    public string Name { get; init; } = "";
}
#endregion

#region Сокращенная запись свойств  - Сахар
class Person8
{
    string name;
    public string Name
    {
        get => name;
        set => name = value;
    }
}
// сокращать все свойство
class Person9
{
    string name;

    // эквивалентно public string Name { get { return name; } }
    public string Name => name;
}
#endregion