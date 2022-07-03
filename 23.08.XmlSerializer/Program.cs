//  Сериализация в XML. XmlSerializer

//  Для удобного сохранения и извлечения объектов из файлов xml может использоваться класс XmlSerializer из пространства имен
//  System.Xml.Serialization. Данный класс упрощает сохранение сложных объектов в формат xml и последующее их извлечение.

//  Для создания объекта XmlSerializer можно применять различные конструкторы, но почти все они требуют указания типа данных,
//  которые будут сериализоваться и десериализоваться:
//          XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
//          //[Serializable]
//          class Person { }

//  В данном случае XmlSerializer будет работать только с объектами класса Person.

//  Следует учитывать, что XmlSerializer предполагает некоторые ограничения. Например, класс, подлежащий сериализации,
//  должен иметь стандартный конструктор без параметров и должен иметь модификатор доступа public. Также сериализации
//  подлежат только открытые члены. Если в классе есть поля или свойства с модификатором private, то при сериализации они
//  будут игнорироваться. Кроме того, свойства должны иметь общедоступные геттеры и сеттеры.

#region Сериализация
//  Для сериализации(то есть сохранения в форма xml) применяется метод Serialize(). Данный метод имеет ряд версий.
//  Возьмем самую простую из них:
//  void Serialize(Stream stream, object? o);

//В качестве первого параметра передается поток Stream (например, объект FileStream), в который будет идти сериализация.
//А второй параметр представляет собственно тот объект, который будет сохраняться в формат xml. Например:
using System.Xml.Serialization;
Person person = new Person("Tom", 37);

// передаем в конструктор тип класса Person
XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));

// получаем поток, куда будем записывать сериализованный объект
using (FileStream fs = new FileStream("person.xml",FileMode.OpenOrCreate))
{
    xmlSerializer.Serialize(fs, person);
    Console.WriteLine("Object has been serialized");
}
//  Итак, класс Person общедоступный, имеет общедоступные свойства и конструктор без параметров, поэтому объекты этого
//  класса подлежат сериализации. При создании объекта XmlSerializer передаем в конструктор тип класса Person.

//  В метод Serialize передается файловый поток для сохранения данных в файл person.xml и сохраняемый в этот файл объект Person.
#endregion

#region Десериализация
//  Для десериализации данных xml в объект кода C# применяется метод Deserialize(). Отметим одну из версий этого метода:
//      object? Deserialize (Stream stream);

//  В качестве параметра в метод передается объект Stream, который содержит данные в формате xml. Результатом метода является
//  десериализованный объект.

//  Например, десериализуем данные из выше созданного файла person.xml:
using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
{
    Person? person2 = xmlSerializer.Deserialize(fs) as Person;
    Console.WriteLine($"Name: {person2?.Name}  Age: {person2?.Age}");
}
#endregion

#region Сериализация и десериализация коллекций
//  Подобным образом мы можем сериализовать массив или коллекцию объектов:
Person[] people = new Person[]
{
    new Person("Tom", 37),
    new Person("Bob", 41)
};

XmlSerializer formatter = new XmlSerializer(typeof(Person[]));
// сохранение массива в файл
using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
{
    formatter.Serialize(fs, people);
}
// восстановление массива из файла
using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
{
    Person[]? newpeople = formatter.Deserialize(fs) as Person[];

    if (newpeople != null)
    {
        foreach (Person person3 in newpeople)
        {
            Console.WriteLine($"Name: {person3.Name} --- Age: {person3.Age}");
        }
    }
}
#endregion
//[Serializable]
public class Person
{
    public string Name { get; set; } = "Undefined";
    public int Age { get; set; } = 1;

    public Person() { }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}