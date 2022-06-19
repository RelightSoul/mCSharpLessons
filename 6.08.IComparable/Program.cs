//  Сортировка объектов. Интерфейс IComparable

//  Большинство встроенных в .NET классов коллекций и массивы поддерживают сортировку. С помощью одного метода,
//  который, как правило, называется Sort() можно сразу отсортировать по возрастанию весь набор данных. Например:

//  int[] numbers = new int[] { 97, 45, 32, 65, 83, 23, 15 };
//  Array.Sort(numbers);
//  foreach (int n in numbers)
//       Console.WriteLine(n);          // 15 23 32 45 65 83 97

//  Однако метод Sort по умолчанию работает только для наборов примитивных типов, как int или string. Для сортировки
//  наборов сложных объектов применяется интерфейс IComparable. Он имеет всего один метод:
public interface IComparable
{
    int CompareTo(object? o);
}

//  Метод CompareTo предназначен для сравнения текущего объекта с объектом, который передается в качестве параметра
//  object? o. На выходе он возвращает целое число, которое может иметь одно из трех значений:

//  Меньше нуля.Значит, текущий объект должен находиться перед объектом, который передается в качестве параметра

//  Равен нулю.Значит, оба объекта равны

//  Больше нуля.Значит, текущий объект должен находиться после объекта, передаваемого в качестве параметра

//  Например, имеется класс Person:

class Person : IComparable
{
    public string Name { get; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name; Age = age;
    }
    public int CompareTo(object? o)
    {
        if (o is Person person) return Name.CompareTo(person.Name);
        else throw new ArgumentException("Некорректное значение параметра");
    }
}
//  Здесь в качестве критерия сравнения выбрано свойство Name объекта Person. Поэтому при сравнении здесь фактически
//  идет сравнение значения свойства Name текущего объекта и свойства Name объекта, переданного через параметр. Если
//  вдруг объект не удастся привести к типу Person, то выбрасывается исключение.

//  Интерфейс IComparable имеет обобщенную версию, поэтому мы могли бы сократить и упростить его применение в классе Person2:
class Person2 : IComparable<Person2>
{
    public string Name { get; }
    public int Age { get; set; }
    public Person2(string name, int age)
    {
        Name = name; Age = age;
    }
    public int CompareTo(Person2? person)
    {
        if (person is null) throw new ArgumentException("Некорректное значение параметра");
        return Name.CompareTo(person.Name);
    }
}
//  Аналогичным образом мы мошли сравнивать по возрасту:
class Person3 : IComparable<Person3>
{
    public string Name { get; }
    public int Age { get; set; }
    public Person3(string name, int age)
    {
        Name = name; Age = age;
    }
    public int CompareTo(Person3? person)
    {
        if (person is null) throw new ArgumentException("Некорректное значение параметра");
        return Age.CompareTo(person.Age);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("------ IComparable ------");
        Console.WriteLine("------ По имени ------");
        var tom = new Person2("Tom", 37);
        var bob = new Person2("Bob", 41);
        var sam = new Person2("Sam", 25);

        Person2[] people = { tom, bob, sam };
        Array.Sort(people);

        foreach (Person2 person in people)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }

        Console.WriteLine("------ По возрасту ------");
        var tom2 = new Person3("Tom", 37);
        var bob2 = new Person3("Bob", 41);
        var sam2 = new Person3("Sam", 25);

        Person3[] people2 = { tom2, bob2, sam2 };
        Array.Sort(people2);

        foreach (Person3 person in people2)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }

        Console.WriteLine("------ IComparer ------");
        Console.WriteLine("------ По длине имени ------");
        var alice = new Person5("Alice",44);
        var newTom = new Person5("Tom", 23);
        var kate = new Person5("Kate", 11);

        Person5[] pArray = {alice, newTom, kate};
        Array.Sort(pArray, new PeopleComparer());
        foreach (Person5 p in pArray)
        {
            Console.WriteLine($"{p.Name} - {p.Age}");
        }
        //  Объект компаратора указывается в качестве второго параметра метода Array.Sort(). При этом не важно,
        //  реализует ли класс Person интерфейс IComparable или нет. Правила сортировки, установленные компаратором,
        //  будут иметь больший приоритет. В начале будут идти объекты Person, у которых имена меньше, а в конце -
        //  у которых имена длиннее.
    }
}

#region Применение компаратора
//  Кроме интерфейса IComparable платформа .NET также предоставляет интерфейс IComparer:
//      public interface IComparer<in T>
//      {
//           int Compare(T? x, T? y);
//      }

//  Метод Compare предназначен для сравнения двух объектов o1 и o2. Он также возвращает три значения, в зависимости
//  от результата сравнения: если первый объект больше второго, то возвращается число больше 0, если меньше - то число
//  меньше нуля; если оба объекта равны, возвращается ноль.

//  Создадим компаратор объектов Person. Пусть он сравнивает объекты в зависимости от длины строки - значения свойства Name:
class PeopleComparer : IComparer<Person5>
{
    public int Compare(Person5? p1, Person5? p2)
    {
        if (p1 is null || p1 is null)
        {
            throw new ArgumentException("Неверное значение");
        }
        return p1.Name.Length - p2.Name.Length;
    }
}
class Person5
{
    public string Name { get; }
    public int Age { get; set; }
    public Person5 (string name, int age)
    {
        Name = name; Age = age;
    }
}
//  В данном случае используется обобщенная версия интерфейса IComparer, чтобы не делать излишних преобразований типов.
#endregion