//  Исследование методов и конструкторов с помощью рефлексии

#region Получение информации о методах
//  Для получения получении информации отдельно о методах применяется метод GetMethods(). Этот метод возвращает
//  все методы типа в виде массива объектов MethodInfo. Его свойства предоставляют информацию о методе. Отметим
//  некоторые из его свойств:
//      IsAbstract: возвращает true, если метод абстрактный
//      IsFamily: возвращает true, если метод имеет модификатор доступа protected
//      IsFamilyAndAssembly: возвращает true, если метод имеет модификатор доступа private protected
//      IsFamilyOrAssembly: возвращает true, если метод имеет модификатор доступа protected internal
//      IsAssembly: возвращает true, если метод имеет модификатор доступа internal
//      IsPrivate: возвращает true, если метод имеет модификатор доступа private
//      IsPublic: возвращает true, если метод имеет модификатор доступа public
//      IsConstructor: возвращает true, если метод предоставляет конструктор
//      IsStatic: возвращает true, если метод статический
//      IsVirtual: возвращает true, если метод виртуальный
//      ReturnType: возвращает тип возвращаемого значения

//  Некоторые из методов MethodInfo:
//      GetMethodBody(): возвращает тело метода в виде объекта MethodBody
//      GetParameters(): возвращает массив параметров, где каждый параметр представлен объектом типа ParameterInfo
//      Invoke(): вызывает метод
using System.Reflection;

Type myType = typeof(Printer);
Console.WriteLine("Methods: ");
foreach (MethodInfo method in myType.GetMethods())
{
    string mod = "";
    if (method.IsStatic)
    {
        mod += "static ";
    }
    if (method.IsVirtual)
    {
        mod += "virtual";
    }
    Console.WriteLine($"{mod}{method.ReturnParameter.Name} {method.Name}()");
}
//  Как видно из вывода в категорию методов также попадают и свойства, которые по сути представляют два метода:
//  get и set. Если подобная ситуация не устраивает, то можно дополнительно фильтровать список методов:
//foreach (MethodInfo method in myType.GetMethods().
//         Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_"))){}
#endregion

#region BindingFlags
//  В примере выше использовалась простая форма метода GetMethods(), которая извлекает все общедоступные
//  публичные методы. Но мы можем использовать и другую форму метода: MethodInfo[] GetMethods(BindingFlags).
//  Объединяя значения BindingFlags можно комбинировать вывод. Например, получим только методы самого класса
//  без унаследованных, как публичные, так и все остальные:
Type myType2 = typeof(Printer2);

Console.WriteLine("Методы:");
foreach (MethodInfo method in myType2.GetMethods(BindingFlags.DeclaredOnly
            | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
    Console.WriteLine($"{method.ReturnType.Name} {method.Name} ()");
}
//  Теперь метод Print в классе Person является приватным, а метод SayMessage имеет модификатор protected internal.

//  Для получения всех непубличных методов в метод GetMethods() передается набор флагов BindingFlags.Instance |
//  BindingFlags.NonPublic | BindingFlags.Public, то есть получаем все методы экземпляра, как публичные, так и
//  непубличные, но исключаем статические. 
#endregion

#region Исследование параметров
//  С помощью метода GetParameters() можно получить все параметры метода в виде массива объектов ParameterInfo.
//  Отметим некоторые из свойств ParameterInfo, которые позволяют получить информацию о параметрах:

//  Attributes: возвращает атрибуты параметра

//  DefaultValue: возвращает значение параметра по умолчанию

//  HasDefaultValue: возвращает true, если параметр имеет значение по умолчанию

//  IsIn: возвращает true, если параметр имеет модификатор in

//  IsOptional: возвращает true, если параметр является необязательным

//  IsOut: возвращает true, если параметр является выходным, то есть имеет модификатор out

//  Name: возвращает имя параметра

//  ParameterType: возвращает тип параметра
foreach (MethodInfo method in typeof(Printer3).GetMethods())
{
    Console.Write($"{method.ReturnType.Name} {method.Name} (");
    //получаем все параметры
    ParameterInfo[] parameters = method.GetParameters();
    for (int i = 0; i < parameters.Length; i++)
    {
        var param = parameters[i];
        // получаем модификаторы параметра
        string modificator = "";
        if (param.IsIn) modificator = "in";
        else if (param.IsOut) modificator = "out";

        Console.Write($"{param.ParameterType.Name} {modificator} {param.Name}");
        // если параметр имеет значение по умолчанию
        if (param.HasDefaultValue) Console.Write($"={param.DefaultValue}");
        // если не последний параметр, добавляем запятую
        if (i < parameters.Length - 1) Console.Write(", ");
    }
    Console.WriteLine(")");
}
//  Стоит отметить, что если параметр имеет модификатор ref, in, out, то в конце названия типа добавляется
//  амперсанд - String&.
#endregion

#region Вызов методов
//  С помощью метода Invoke() можно вызвать метод:
//               public object? Invoke (object? obj, object?[]? parameters);
//  Первый параметр представляет объект, для которого вызывается метод. Второй объект представляет массив
//  значений, которые передаются параметрам метода. И также метод может возвращать результат в виде значения
//  object?.
var myPrinter = new Printer4("Hello");
var print = typeof(Printer4).GetMethod("Print");  // получаем метод Print

print?.Invoke(myPrinter, parameters: null);   // Hello
//  Метод GetMethod() возвращает метод, который имеет определенное имя - в данном случае метод Print. Далее
//  используя полученный метод, его можно вызвать. Здесь при вызове в качестве первого параметра передается
//  объект, для которого вызывается метод Print - объект myPrinter. И поскольку метод Print не принимает
//  параметров, параметру parameters передается значение null.

//  Если метод непубличный, то для получения метода мы можем передать флаги в вызов GetMethod:
//  var print = typeof(Printer).GetMethod("Print",
//              BindingFlags.Instance |
//              BindingFlags.Public |
//              BindingFlags.NonPublic);

//  Передача параметров:
var myPrinter5 = new Printer5();
var printMess = typeof(Printer5).GetMethod("PrintMessage");
printMess?.Invoke(myPrinter5, new object[] {"Hi all",3 });
//  Здесь метод PrintMessage имеет два параметра - messsage (некоторое соощение) и times (сколько раз надо
//  вывести сообщение на консоль). И для этих параметров передаем массив аргументов new object[] {"Hi all", 3}.
//  Таким образом, метод три раза выведет строку "Hi all".

//  Вызов обобщенного метода:
var myPrinter6 = new Printer6();
// получаем метод PrintValue
var printValue = typeof(Printer6).GetMethod("PrintValue");
// получаем обобщенную версию метода для типа string
var printStringValue = printValue?.MakeGenericMethod(typeof(string));
// вызываем метод PrintValue, передавая ему строку
printStringValue?.Invoke(myPrinter6, new object[] { "Hello world" });
#endregion

#region Получение конструкторов
//  Для получения конструкторов применяется метод GetConstructors(), который возвращает массив объектов
//  класса ConstructorInfo. Этот класс во многом похож на MethodInfo и имеет ряд общей функциональности.
//  Некоторые основные свойства и методы:
//  Свойство IsFamily: возвращает true, если конструктор имеет модификатор доступа protected
//  Свойство IsFamilyAndAssembly: возвращает true, если конструктор имеет модификатор доступа private protected
//  Свойство IsFamilyOrAssembly: возвращает true, если конструктор имеет модификатор доступа protected internal
//  Свойство IsAssembly: возвращает true, если конструктор имеет модификатор доступа internal
//  Свойство IsPrivate: возвращает true, если конструктор имеет модификатор доступа private
//  Свойство IsPublic: возвращает true, если конструктор имеет модификатор доступа public
//  Метод GetMethodBody(): возвращает тело конструктора в виде объекта MethodBody
//  Метод GetParameters(): возвращает массив параметров, где каждый параметр представлен объектом типа ParameterInfo
//  Метод Invoke(): вызывает конструктор
Type myTypePerson = typeof(Person);

Console.WriteLine("Конструкторы:");
foreach (ConstructorInfo ctor in myTypePerson.GetConstructors(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
    string modificator = "";

    // получаем модификатор доступа
    if (ctor.IsPublic)
        modificator += "public";
    else if (ctor.IsPrivate)
        modificator += "private";
    else if (ctor.IsAssembly)
        modificator += "internal";
    else if (ctor.IsFamily)
        modificator += "protected";
    else if (ctor.IsFamilyAndAssembly)
        modificator += "private protected";
    else if (ctor.IsFamilyOrAssembly)
        modificator += "protected internal";

    Console.Write($"{modificator} {myTypePerson.Name}(");
    // получаем параметры конструктора
    ParameterInfo[] parameters = ctor.GetParameters();
    for (int i = 0; i < parameters.Length; i++)
    {
        var param = parameters[i];
        Console.Write($"{param.ParameterType.Name} {param.Name}");
        if (i < parameters.Length - 1) Console.Write(", ");
    }
    Console.WriteLine(")");
}

#endregion
class Person
{
    public string Name { get; }
    public int Age { get; }
    public Person(string name, int age)
    {
        Name = name; Age = age;
    }
    public Person(string name) : this(name, 1) { }
    private Person() : this("Tom") { }
}
class Printer
{
    public string DefaultMessage { get; set; } = "Hello";
    public void PrintMessage(string message, int times = 1)
    {
        while (times-- > 0) Console.WriteLine(message);
    }
    public string CreateMessage() => DefaultMessage;
}
class Printer2
{
    public string DefaultMessage { get; set; } = "Hello";
    protected internal void PrintMessage(string message, int times = 1)
    {
        while (times-- > 0) Console.WriteLine(message);
    }
    private string CreateMessage() => DefaultMessage;
}
class Printer3
{
    public void PrintMessage(string message, int times = 1)
    {
        while (times-- > 0) Console.WriteLine(message);
    }
    public void CreateMessage(out string message) => message = "Hello Metanit.com";
}
class Printer4
{
    public string Text { get; }
    public Printer4(string text) => Text = text;
    public void Print() => Console.WriteLine(Text);
}
class Printer5
{
    public void PrintMessage(string message, int times)
    {
        while (times-- > 0) Console.WriteLine(message);
    }
}
class Printer6
{
    public void PrintValue<T>(T value)
    {
        Console.WriteLine(value);
    }
}

