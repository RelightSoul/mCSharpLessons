//  Сортировка
//  Оператор orderby и метод OrderBy

//  Для сортировки набора данных в LINQ можно применять оператор orderby:
int[] numbers = { 3, 12, 4, 10 };
var orderedNumbers = from i in numbers
                     orderby i
                     select i;
foreach (int i in orderedNumbers)
    Console.WriteLine(i);
//  Оператор orderby принимает критерий сортировки. В данном случае в качестве критерия выступает само число.

//  Если числа сортируются стандартным образом, как принято в математике, то строки сортируются исходя
//  из алфавитного порядка:
string[] people = { "Tom", "Bob", "Sam" };
var orderedPeople = from p in people orderby p select p;
foreach (var p in orderedPeople)
    Console.WriteLine(p);

//  Вместо оператора orderby можно применять метод расширения OrderBy():
//      OrderBy(Func < TSource, TKey > keySelector)
//      OrderBy(Func < TSource, TKey > keySelector, IComparer<TKey> ? comparer);
//  Первая версия метода получает делегат, который через параметр получает элемент коллекции и который
//  возвращает значение, применяемое для сортировки. Вторая версия позволяет также задать принцип сортировки
//  через реализацию интерфейса IComparer.

//  Перепишем предыдущие два примера с помощью метода OrderBy:
var orderedNumbers2 = numbers.OrderBy(n => n);
foreach (int i in orderedNumbers)
    Console.WriteLine(i);

var orderedPeople2 = people.OrderBy(p => p);
foreach (var p in orderedPeople)
    Console.WriteLine(p);

#region Сортировка сложных объектов
//  Возьмем посложнее пример. Допустим, надо отсортировать выборку сложных объектов. Тогда в качестве критерия
//  мы можем указать свойство класса объекта:
var _people = new List<Person>
{
    new Person("Tom", 37),
    new Person("Sam", 28),
    new Person("Tom", 22),
    new Person("Bob", 41),
};
// с помощью оператора orderby
var sortedPeople = from p in _people
                   orderby p.Name
                   select p;

foreach (Person p in sortedPeople)
{
    Console.WriteLine($"{p.Name} - {p.Age}");
}
// с помощью метода OrderBy
var sortedPeople2 = _people.OrderBy(p => p.Name);
foreach (Person p in sortedPeople2)
{
    Console.WriteLine($"{p.Name} - {p.Age}");
}
#endregion

#region Сортировка по возрастанию и убыванию
//  По умолчанию оператор orderby и метод OrderBy производят сортировку по возрастанию. С помощью ключевых
//  слов ascending (сортировка по возрастанию) и descending(сортировка по убыванию) для оператора orderby
//  можно явным образом указать направление сортировки. Например, отсортируем массив чисел по убыванию:
int[] _numbers = { 3, 12, 4, 10 };
var _orderedNumbers = from n in _numbers
                      orderby n descending
                      select n;
foreach (int i in _orderedNumbers)
{
    Console.WriteLine(i);
}
//  Для сортировки по убыванию можно применять метод OrderByDescending(), который работает аналогично
//  OrderBy за исключением направления сортировки:
var _orderedNumbers2 = _numbers.OrderByDescending(n => n);
#endregion

#region Множественные критерии сортировки
//  В наборах сложных объектов иногда встает ситуация, когда надо отсортировать не по одному, а сразу
//  по нескольким полям. Для этого в запросе LINQ все критерии указываются в порядке приоритета через запятую:
var people3 = new List<Person>
{
    new Person("Tom", 37),
    new Person("Sam", 28),
    new Person("Tom", 22),
    new Person("Bob", 41),
};

var sortedPeople3 = from p in people3
                    orderby p.Name, p.Age descending // сортировка по возрасту по убыванию
                    //  Для разных критериев сортировки можно установить направление
                    select p;
foreach (var p in sortedPeople3)
{
    Console.WriteLine($"{p.Name} - {p.Age}");
}
//   С помощью методов расширения то же самое можно сделать через метод ThenBy()(для сортировки по возрастанию)
//   и ThenByDescending() (для сортировки по убыванию):
var sortedPeople4 = people3.OrderBy(p => p.Name).ThenByDescending(p => p.Age);  //Результат аналогичен предыдущему
#endregion

#region Переопределение критерия сортировки
//  С помощью реализации IComparer мы можем переопределить критерии сортировки, если они нас не устраивают.
//  Например, строки по умолчанию сортируются в алфавитном порядке. Но что, если мы хотим сортировать строки
//  исходя из их длины?
string[] people33 = new[] { "Kate", "Tom", "Sam", "Mike", "Alice" };
var sortedPeople33 = people33.OrderBy(p => p, new CustromStringComparer());
foreach (var p in sortedPeople33)
{
    Console.WriteLine(p);
}
// сравнение по длине строки
class CustromStringComparer : IComparer<String>
{
    public int Compare(string? x, string? y)
    {
        int xLenght = x?.Length ?? 0;   // если x равно null, то длина 0
        int yLenght = y?.Length ?? 0;
        return xLenght - yLenght;
    }
}
//  Интерфейс IComparer типизируется типов сортируемых данных (в данном случае типом String). Для реализации
//  этого интерфейса необходимо определить метод Compare. Он возвращает число: если первый параметр больше
//  второго, то число больше 0, если меньше - то число меньше 0. Если оба параметра равны, то возвращается 0.

//  В данном случае, если параметр равен null, будем считать что длина строки равна 0. И с помощью разницы
//  длин строк из обоих параметров определяем, какой из них больше.
#endregion
record class Person(string Name, int Age);