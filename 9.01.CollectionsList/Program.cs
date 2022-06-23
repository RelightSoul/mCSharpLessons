// Коллекции. Список List<T>

// Хотя в языке C# есть массивы, которые хранят в себе наборы однотипных объектов, но работать с ними не
// всегда удобно. Например, массив хранит фиксированное количество объектов, однако что если мы заранее
// не знаем, сколько нам потребуется объектов. И в этом случае намного удобнее применять коллекции. Еще
// один плюс коллекций состоит в том, что некоторые из них реализует стандартные структуры данных, например,
// стек, очередь, словарь, которые могут пригодиться для решения различных специальных задач. Большая часть
// классов коллекций содержится в пространстве имен System.Collections.Generic.

//  Класс List<T> из пространства имен System.Collections.Generic представляет простейший список однотипных
//  объектов. Класс List типизируется типом, объекты которого будут хранится в списке.

//Мы можем создать пустой список:
using System.Collections.Generic;

List<string> people = new List<string>();
//В данном случае объект List типизируется типом string. А это значит, что хранить в этом списке
//мы можем только строки.

//Можно сразу при создании списка инициализировать его начальными значениями. В этом случае элементы списка
//помещаются после вызова конструктора в фигурных скобках
List<string> people2 = new List<string>() { "Roman","Sarra","Mark"};
//В данном случае в список помещаются три строки

//Также можно при создании списка инициализировать его элементами из другой коллекции, например, другого списка:
var people3 = new List<string>(people2);

//Можно совместить оба способа:
var people4 = new List<string>(people3) { "Kira", "Rita"};

//  Подобным образом можно работать со списками других типов, например:
List<Person> listOfPersons = new List<Person>
{
    new Person("Roman"),
    new Person("Sarra"),
    new Person("Rita"),
    new Person("Kira"),
};

#region Установка начальной емкости списка
//  Еще один конструктор класса List принимает в качестве параметра начальную емкость списка:
List<string> people5 = new List<string>(16);
Console.WriteLine(people5.Capacity);
people5.Capacity = 25;
Console.WriteLine(people5.Capacity);
//  Указание начальной емкости списка позволяет в будущем увеличить производительность и уменьшить
//  издержки на выделение памяти при добавлении элементов. Поскольку динамическое добавление в список
//  может приводить на низком уровне к дополнительному выделению памяти, что снижает производительность.
//  Если же мы знаем, что список не будет превышать некоторый размер, то мы можем передать этот размер
//  в качестве емкости списка и избежать дополнительных выделений памяти.

// Также начальную емкость можно установить с помощью свойства Capacity, которое имеется у класса List.
#endregion

#region Обращение к элементам списка
//  Как и массивы, списки поддерживают индексы, с помощью которых можно обратиться к определенным элементам:
List<string> people6 = new List<string>() { "Roman", "Sarra", "Mark" };
string element = people6[0];  // получаем первый элемент
Console.WriteLine(element);
people6[0] = "Kristofer";     // изменяем первый элемент
Console.WriteLine(people6[0]);
#endregion

#region Длина списка
// С помощью свойства Count можно получить длину списка:
Console.WriteLine(people6.Count);
#endregion

#region Перебор списка
//  C# позволяет осуществить перебор списка с помощью стандартного циклов
foreach (string str in people6)
{
    Console.Write("{0}\t",str);
}
Console.WriteLine();
for (int i = 0; i < people6.Count; i++)
{
    Console.Write($"{people6[i]}\t");
}
Console.WriteLine();
#endregion

#region Методы списка
//Среди его методов можно выделить следующие:

//void Add(T item): добавление нового элемента в список

//void AddRange(IEnumerable<T> collection): добавление в список коллекции или массива

//int BinarySearch(T item): бинарный поиск элемента в списке. Если элемент найден, то метод возвращает индекс
//этого элемента в коллекции. При этом список должен быть отсортирован.

//void CopyTo(T[] array): копирует список в массив array

//void CopyTo(int index, T[] array, int arrayIndex, int count): копирует из списка начиная с индекса index
//элементы, количество которых равно count, и вставляет их в массив array начиная с индекса arrayIndex

//bool Contains(T item): возвращает true, если элемент item есть в списке

//void Clear(): удаляет из списка все элементы

//bool Exists(Predicate<T> match): возвращает true, если в списке есть элемент, который соответствует делегату
//match

//T? Find(Predicate<T> match): возвращает первый элемент, который соответствует делегату match. Если элемент
//не найден, возвращается null

//T? FindLast(Predicate<T> match): возвращает последний элемент, который соответствует делегату match. Если
//элемент не найден, возвращается null

//List<T> FindAll(Predicate<T> match): возвращает список элементов, которые соответствуют делегату match

//int IndexOf(T item): возвращает индекс первого вхождения элемента в списке

//int LastIndexOf(T item): возвращает индекс последнего вхождения элемента в списке

//List<T> GetRange(int index, int count): возвращает список элементов, количество которых равно count, начиная
//с индекса index.

//void Insert(int index, T item): вставляет элемент item в список по индексу index. Если такого индекса в списке
//нет, то генерируется исключение

//void InsertRange(int index, collection): вставляет коллекцию элементов collection в текущий список начиная с
//индекса index. Если такого индекса в списке нет, то генерируется исключение

//bool Remove(T item): удаляет элемент item из списка, и если удаление прошло успешно, то возвращает true. Если
//в списке несколько одинаковых элементов, то удаляется только первый из них

//void RemoveAt(int index): удаление элемента по указанному индексу index. Если такого индекса в списке нет, то
//генерируется исключение

//void RemoveRange(int index, int count): параметр index задает индекс, с которого надо удалить элементы, а
//параметр count задает количество удаляемых элементов.

//int RemoveAll((Predicate<T> match)): удаляет все элементы, которые соответствуют делегату match. Возвращает
//количество удаленных элементов

//void Reverse(): изменяет порядок элементов

//void Reverse(int index, int count): изменяет порядок на обратный для элементов, количество которых равно count,
//начиная с индекса index

//void Sort(): сортировка списка

//void Sort(IComparer<T>? comparer): сортировка списка с помощью объекта comparer, который передается в качестве
//параметра

//   -----  Добавление в список  -----
List<string> _people = new List<string> { "Tom" };

_people.Add("Bob");                                // добавление элемента
// people = { "Tom", "Bob" };

_people.AddRange(new[] {"Sam","Alice"});           // добавляем массив
// people = { "Tom", "Bob", "Sam", "Alice" };
// также можно было бы добавить другой список
// people.AddRange(new List<string>(){ "Sam", "Alice" });

_people.Insert(0, "Eugene");                       // вставляем по индексу
// people = { "Eugene", "Tom", "Bob", "Sam", "Alice" };

_people.InsertRange(1, new[] { "Mike", "Kate" });  // вставляем массив с индекса 1
// people = { "Eugene", "Mike", "Kate", "Tom", "Bob", "Sam", "Alice" };

//   -----  Удаление из списка  -----

_people.RemoveAt(1);                               //удаляем второй элемент
// people = { "Eugene", "Kate", "Tom", "Bob", "Sam", "Tom", "Alice" };

_people.Remove("Tom");                             //  удаляем элемент "Tom"
// people = { "Eugene", "Kate", "Bob", "Sam", "Tom", "Alice" };

_people.RemoveAll(p => p.Length == 3);             // удаляем из списка все элементы, длина строки которых равна 3
// people = { "Eugene", "Kate", "Alice" };

_people.RemoveRange(1, 2);                         // удаляем из списка 2 элемента начиная с индекса 1
// people = { "Eugene"};

_people.Clear();                                    // полностью очищаем список
// people = {  };

//   -----  Поиск и проверка элемента  -----
_people.AddRange(new List<string> {"Eugene", "Mike", "Kate", "Tom", "Bob", "Sam" });

var contBob = _people.Contains("Bob");   //true
var contBill = _people.Contains("Bill"); //false

// проверяем, есть ли в списке строки с длиной 3 символа
var exitsLength3 = _people.Exists(p => p.Length == 3);

// проверяем, есть ли в списке строки с длиной 7 символов
var exitsLength7 = _people.Exists(p => p.Length == 7);

// получаем первый элемент с длиной в 3 символа
var firstElWithL3 = _people.Find(p => p.Length == 3);

// получаем последний элемент с длиной в 3 символа
var lastElWithL3 = _people.FindLast(p => p.Length == 3);

// получаем все элементы с длиной в 3 символа в виде списка
List<string> allWithL3 = _people.FindAll(p => p.Length == 3);

//   -----  Получение диапазона и копирование в массив  -----
_people.Clear();
_people.AddRange(new List<string> { "Eugene", "Tom", "Mike", "Sam", "Bob" });

// получаем диапазон со второго по четвертый элемент
var range = _people.GetRange(1, 3);
// range = { "Tom", "Mike", "Sam"};
Console.WriteLine(String.Join(", ",range));

// копируем в массив первые три элемента
string[] partOfPeople = new string[3];
_people.CopyTo(0, partOfPeople, 0, 3);
// partOfPeople = { "Eugene", "Tom", "Mike"};

//   -----  Расположение элементов в обратном порядке  -----

Console.WriteLine(string.Join("  ", _people));   // people = { "Eugene", "Tom", "Mike", "Sam", "Bob"};
// переворачиваем весь список
_people.Reverse();
Console.WriteLine(string.Join("  ", _people));   // people = { "Bob", "Sam", "Mike", "Tom", "Eugene"};

// переворачиваем часть только 3 элемента с индекса 1
_people.Reverse(1, 3);
Console.WriteLine(string.Join("  ", _people));   // people = { "Bob", "Tom", "Mike", "Sam", "Eugene"};
#endregion

#region Конец кода
class Person
{
    public string Name { get; set; }
    public Person(string name) => Name = name;
}
#endregion