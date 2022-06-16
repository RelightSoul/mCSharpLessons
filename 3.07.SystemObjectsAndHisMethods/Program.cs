// Все классы в .NET, даже те, которые мы сами создаем, а также базовые типы, такие как System.Int32,
// являются неявно производными от класса Object. Поэтому все типы и классы могут реализовать те методы,
// которые определены в классе System.Object.

#region Метод ToString
//  Метод ToString служит для получения строкового представления данного объекта. Для базовых типов
//  просто будет выводиться их строковое значение:

int i = 5;
Console.WriteLine(i.ToString());

//  Для классов же этот метод выводит полное название класса с указанием пространства имен, в котором
//  определен этот класс. И мы можем переопределить данный метод.

Person person = new Person();
Console.WriteLine(person.ToString());

Clock clock = new Clock() { Hours = 12, Minutes = 32, Seconds = 33};
Console.WriteLine(clock.ToString());

//  Стоит отметить, что различные технологии на платформе .NET активно используют метод ToString для
//  разных целей. В частности, тот же метод Console.WriteLine() по умолчанию выводит именно строковое
//  представление объекта. Поэтому, если нам надо вывести строковое представление объекта на консоль,
//  то при передаче объекта в метод Console.WriteLine необязательно использовать метод ToString()
//  - он вызывается неявно:

Console.WriteLine(person);
Console.WriteLine(clock);

var p1 = new Person3 { Name = "Alex" };
var p2 = new Person3 { Name = "Bill" };
var p3 = new Person3 { Name = "Alex" };

bool p1VsP2 = p1.Equals(p2);
Console.WriteLine(p1VsP2);
bool p2VsP3 = p2.Equals(p3);
Console.WriteLine(p2VsP3);
bool p3VsP1 = p3.Equals(p1);
Console.WriteLine(p3VsP1);

class Person
{
    public string Name { get; set; } = "";
}
class Clock
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public override string ToString()
    {
        return $"{Hours} : {Minutes} : {Seconds}";
    }
}
#endregion

#region Метод GetHashCode
//  Метод GetHashCode позволяет возвратить некоторое числовое значение, которое будет соответствовать
//  данному объекту или его хэш-код. По данному числу, например, можно сравнивать объекты. Можно определять
//  самые разные алгоритмы генерации подобного числа или взять реализаци базового типа:
class Person2
{
    public string Name { get; set; } = "";
    public override int GetHashCode()
    {
        return Name.GetHashCode();   // GetHashCode возвращает хеш-код для значения свойства Name.
                                     // В реальности алгоритм может быть самым различным.
    }
}
#endregion

#region Получение типа объекта и метод GetType
//  Метод GetType позволяет получить тип данного объекта:

//      Person person = new Person { Name = "Tom" };
//      Console.WriteLine(person.GetType());    // Person

//      Этот метод возвращает объект Type, то есть тип объекта.

//  С помощью ключевого слова typeof мы получаем тип класса и сравниваем его с типом объекта.
//  И если этот объект представляет тип Client, то выполняем определенные действия.

//      object person = new Person { Name = "Tom" };
//      if (person.GetType() == typeof(Person))
//          Console.WriteLine("Это реально класс Person");

//  В отличие от методов ToString, Equals, GetHashCode метод GetType() не переопределяется.

//  !! Стоит отметить, что проверку типа можно скоратить с помощью оператора is:

//      object person = new Person { Name = "Tom" };
//      if (person is Person)
//          Console.WriteLine("Это реально класс Person");
#endregion

#region Метод Equals
//  Метод Equals позволяет сравнить два объекта на равенство. В качестве параметра он принимает
//  объект для сравнения в виде типа object и возврашает true, если оба объекта равны:

//       public override bool Equals(object? obj) {......}

class Person3
{
    public string Name { get; set; } = "";
    public override bool Equals(object? obj)
    {
        // если параметр метода представляет тип Person
        // то возвращаем true, если имена совпадают
        if (obj is Person3 person) return Name == person.Name;
        return false;
    }
    // вместе с методом Equals следует реализовать метод GetHashCode
    public override int GetHashCode() => Name.GetHashCode();
}
//  Пример сравнения Person3 выше
#endregion