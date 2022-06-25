//  Класс Array и массивы

//  Все массивы в C# построены на основе класса Array из пространства имен System. Этот класс определяет
//  ряд свойств и методов, которые мы можем использовать при работе с массивами. Основные свойства и методы:

//  Свойство Length возвращает длину массива

//  Свойство Rank возвращает размерность массива

//  int BinarySearch (Array array, object? value) выполняет бинарный поиск в отсортированном массиве
//  и возвращает индекс найденного элемента

//  void Clear (Array array) очищает массив, устанавливая для всех его элементов значение по умолчанию

//  void Copy (Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
//  копирует из массива sourceArray начиная с индекс sourceIndex length элементов в массив destinationArray
//  начиная с индекса destinationIndex

//  bool Exists<T> (T[] array, Predicate<T> match) проверяет, содержит ли массив array элементы, которые
//  удовлеворяют условию делегата match

//  void Fill<T> (T[] array, T value) заполняет массив array значением value

//  T? Find<T> (T[] array, Predicate<T> match) находит первый элемент, который удовлеворяет определенному
//  условию из делегата match. Если элемент не найден, то возвращается null

//  T? FindLast<T> (T[] array, Predicate<T> match) находит последний элемент, который удовлеворяет
//  определенному условию из делегата match. Если элемент не найден, то возвращается null

//  int FindIndex<T> (T[] array, Predicate<T> match) возвращает индекс первого вхождения элемента, который
//  удовлеворяет определенному условию делегата match

//  int FindLastIndex<T> (T[] array, Predicate<T> match) возвращает индекс последнего вхождения элемента,
//  который удовлеворяет определенному условию

//  T[] FindAll<T>(T[] array, Predicate < T > match) возвращает все элементы в виде массива, которые
//  удовлеворяет определенному условию из делегата match

//  int IndexOf (Array array, object? value) возвращает индекс первого вхождения элемента в массив

//  int LastIndexOf (Array array, object? value) возвращает индекс последнего вхождения элемента в массив

//  void Resize<T> (ref T[]? array, int newSize) изменяет размер одномерного массива

//  void Reverse (Array array) располагает элементы массива в обратном порядке

//  void Sort (Array array) сортирует элементы одномерного массива

#region Поиск индекса элемента
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };

// находим индекс элемента "Bob"
int bobIndex = Array.BinarySearch(people, "Bob");
// находим индекс первого элемента "Tom"
int tomFirstIndex = Array.IndexOf(people, "Tom");
// находим индекс последнего элемента "Tom"
int tomLastIndex = Array.LastIndexOf(people, "Tom");
// находим индекс первого элемента, у которого длина строки больше 3
int lengthFirstIndex = Array.FindIndex(people, person => person.Length > 3);
// находим индекс последнего элемента, у которого длина строки больше 3
int lengthLastIndex = Array.FindLastIndex(people, person => person.Length > 3);

Console.WriteLine($"bobIndex: {bobIndex}");                 // 2
Console.WriteLine($"tomFirstIndex: {tomFirstIndex}");       // 0
Console.WriteLine($"tomLastIndex: {tomLastIndex}");         // 4
Console.WriteLine($"lengthFirstIndex: {lengthFirstIndex}"); // 3
Console.WriteLine($"lengthLastIndex: {lengthLastIndex}");   // 5
//  Если элемент не найден в массиве, то методы возвращают -1.
#endregion

#region Поиск элемента по условию
// находим первый и последний элементы
// где длина строки больше 3 символов
string? first = Array.Find(people, person => person.Length > 3);
Console.WriteLine(first); // Kate
string? last = Array.FindLast(people, person => person.Length > 3);
Console.WriteLine(last); // Alice

//  находим элементы, у которых длина строки равна 3
string[] group = Array.FindAll(people, person => person.Length == 3);
foreach (var person in group) Console.WriteLine(person);
//  Tom Sam Bob Tom
#endregion

#region Изменение порядка элементов массива
Array.Reverse(people);
//  "Alice", "Tom", "Kate", "Bob", "Sam", "Tom"

//  изменяем порядок 3 элементов начиная c индекса 1  
Array.Reverse(people, 1, 3);
//  В данном случае изменяем порядок только 3 элементов начиная c индекса 1.
#endregion

#region Изменение размера массива
//  Для изменения размера массива применяется метод Resize. Его первый параметр - изменяемый массив,
//  а второй параметр - количество элементов, которые должны быть в массиве. Если второй параметр меньше
//  длины массива, то массив усекается. Если значение параметра, наоборот, больше, то массив дополняется
//  дополнительными элементами, которые имеют значение по умолчанию. Причем первый параметр передается по
//  ссылке:
var people2 = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };

// уменьшим массив до 4 элементов
Array.Resize(ref people2, 4);

foreach (var person in people2)
    Console.Write($"{person} ");
// "Tom", "Sam", "Bob", "Kate"
#endregion

#region Копирование массива
//  Метод Copy копирует часть одного массива в другой:
var people3 = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };

var employees = new string[3];

// копируем 3 элемента из массива people c индекса 1  
// и вставляем их в массив employees начиная с индекса 0
Array.Copy(people3, 1, employees, 0, 3);

foreach (var person in employees)
    Console.Write($"{person} ");
// Sam Bob Kate
#endregion

#region Сортировка массива
var people4 = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };

// В алфавитном порядке
Array.Sort(people4);

foreach (var person in people4)
    Console.Write($"{person} ");
// Alice Bob Kate Sam Tom Tom

//  Этот метод имеет много перегрузок. Например, одна из версий позволяет отсортировать только часть массива:
// сортируем с 1 индекса 3 элемента
Array.Sort(people4, 1, 3);
#endregion