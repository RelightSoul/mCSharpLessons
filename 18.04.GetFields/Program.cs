// Исследование полей и свойств с помощью рефлексии

#region Получение информации о полях
//  Для извлечения всех полей применяется метод GetFields(), который возвращает массив объектов класса FieldInfo.

//  Некоторые основные свойства и методы класса FieldInfo:
//  Свойство IsAssembly: возвращает true, если поле имеет модификатор доступа protected
//  Свойство IsFamilyAndAssembly: возвращает true, если поле имеет модификатор доступа private protected
//  Свойство IsFamilyOrAssembly: возвращает true, если поле имеет модификатор доступа protected internal
//  Свойство IsAssembly: возвращает true, если поле имеет модификатор доступа internal
//  Свойство IsPrivate: возвращает true, если поле имеет модификатор доступа private
//  Свойство IsPublic: возвращает true, если поле имеет модификатор доступа public
//  Свойство IsStatic: возвращает true, если поле статическое
//  Метод GetValue(): возвращает значение поля
//  Метод SetValue(): устанавливает значение поля
using System.Reflection;

Type myType = typeof(Person);

Console.WriteLine("Fields: ");
foreach (FieldInfo field in myType.GetFields(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
//  Чтобы получить и статические, и не статические, и публичные, и непубличные поля, в метод GetFields()
//  передается набор флагов
{
    string modificator = "";
    if (field.IsPublic)
        modificator += "public ";
    else if (field.IsPrivate)
        modificator += "private ";
    else if (field.IsAssembly)
        modificator += "internal ";
    else if (field.IsFamily)
        modificator += "protected ";
    else if (field.IsFamilyAndAssembly)
        modificator += "private protected ";
    else if (field.IsFamilyOrAssembly)
        modificator += "protected internal ";

    // если поле статическое
    if (field.IsStatic) modificator += "static ";

    Console.WriteLine($"{modificator}{field.FieldType.Name} {field.Name}");
}
#endregion

#region Получение и изменение значения поля
//  Для получения одного поля по имени применяется метод GetField(), в который передается имя поля:
//          var name = myType.GetField("name", BindingFlags.Instance | BindingFlags.NonPublic);
//  В качестве второго необязательного параметра передается набор флагов.

//  Причем рефлексия позволяет получать значения и изменять их даже у привтаных полей. Например,
//  получим и изменим значение поля name:
Type myType2 = typeof(Person2);
Person2 tom = new Person2("Tom", 37);

// получаем приватное поле name
var name = myType2.GetField("name", BindingFlags.Instance | BindingFlags.NonPublic);

// получаем значение поля name
var value = name?.GetValue(tom);
Console.WriteLine(value);   // Tom

// изменяем значение поля name
name?.SetValue(tom, "Bob");
tom.Print();    // Bob - 37
#endregion

#region Свойства
//  Для извлечения всех свойств типа применяется соответственно метод GetProperties(), который возвращает
//  массив объектов PropertyInfo. Для получения одного свойства по имени применяется метод GetProperty(),
//  в который передается название свойства и который возвращает объект PropertyInfo?.

//  Некоторый основной функционал класса PropertyInfo:
//  Свойство Attributes: возвращает коллекцию атрибутов свойства
//  Свойство CanRead: возвращает true, если свойство доступно для чтения
//  Свойство CanWrite: возвращает true, если свойство доступно для записи
//  Свойство GetMethod: возвращает get-акссесор в виде объекта MethodInfo?
//  Свойство SetMethod: возвращает set-акссесор в виде объекта MethodInfo?
//  Свойство PropertyType: возвращает тип свойства
//  Метод GetValue(): возвращает значение свойства
//  Метод SetValue(): устанавливает значение свойства#endregion

Type myType3 = typeof(Person3);
foreach (PropertyInfo prop in myType3.GetProperties(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
{
    Console.Write($"{prop.PropertyType} {prop.Name} {{");

    // если свойство доступно для чтения
    if (prop.CanRead) Console.Write("get;");
    // если свойство доступно для записи
    if (prop.CanWrite) Console.Write("set;");
    Console.WriteLine("}");
}
//  С помощью методов PropertyInfo можно манипулировать значением свойства. Например, получим и изменим
//  значение свойства:
Person3 tom3 = new Person3("Tom", 37);
// получаем свойство Age
var ageProp = myType3.GetProperty("Age");
// получаем значение свойства Age у объекта tom
var age = ageProp?.GetValue(tom3);
Console.WriteLine(age); // 37
// устанавливаем новое значение для свойства Age объекта tom
ageProp?.SetValue(tom3, 22);
tom3.Print();    // Tom - 22
//  Для получения значения свойства в метод GetValue() объекта PropertyInfo передается объект, у которого
//  вызывается свойства. Результатом метода является значение свойства. Для установки значения в метод SetValue()
//  объекта PropertyInfo передается объект, у которого устанавливается свойство, и собственно новое значение
//  свойства.
#endregion

class Person
{
    static int minAge = 0;
    string name;
    int age;
    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
    public void Print() => Console.WriteLine($"{name} - {age}");
}
class Person2
{
    static int minAge = 1;
    string name;
    int age;
    public Person2(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
    public void Print() => Console.WriteLine($"{name} - {age}");
}
class Person3
{
    public string Name { get; }
    public int Age { get; set; }
    public Person3(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public void Print() => Console.WriteLine($"{Name} - {Age}");
}