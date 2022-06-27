// Фильтрация коллекции

//  Для выбора элементов из некоторого набора по условию используется метод Where:
//      Where<TSource> (Func<TSource,bool> predicate)
//  Этот метод принимает делегат Func<TSource,bool>, который в качестве параметра принимает каждый элемент
//  последовательности и возвращает значение bool. Если элемент соответствует некоторому условию, то
//  возвращается true, и тогда этот элемент передается в коллекцию, которая возвращается из метода Where.

//  Например, выберем все строки, длина которых равна 3:
string[] people = { "Tom", "Alice", "Bob", "Sam", "Tim", "Tomas", "Bill" };

var selected = people.Where(p => p.Length == 3);
foreach (string p in selected)
{
    Console.WriteLine(p);
}
//  Если выражение в методе Where для определенного элемента будет равно true (в данном случае выражение p.Length == 3), то данный элемент попадает в результирующую выборку.

//  Аналогичный запрос с помощью операторов LINQ:
var selected2 = from p in people
                where p.Length == 3
                select p;

int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
//  var selected3 = numbers.Where(i => i % 2 == 0 && i > 10);    Аналогичный запрос 
var selected3 = from numb in numbers
                where numb > 10 && numb % 2 == 0
                select numb;
foreach (int n in selected3)
{
    Console.WriteLine(n);
}

#region Выборка сложных объектов
//  Допустим, у нас есть класс пользователя:
//              record class Person(string Name, int Age, List<string> Languages);
//  Свойство Name представляет имя, свойство Age - возраст пользователя, а список Languages - список языков,
//  которыми владеет пользователь.

//  Создадим набор пользователей и выберем из них тех, которым больше 25 лет:
var _people = new List<Person>
{
    new Person("Tom", 23 , new List<string> { "english", "german" }),
    new Person ("Bob", 27, new List<string> {"english", "french" }),
    new Person ("Sam", 29, new List<string>  { "english", "spanish" }),
    new Person ("Alice", 24, new List<string> {"spanish", "german" })
};
// var selectedPeopleAge = _people.Where(p => p.Age>25);   Аналогичный запрос 
var selectedPeopleByAge = from p in _people
                          where p.Age > 25
                          select p;
foreach (Person p in selectedPeopleByAge)
{
    Console.WriteLine($"{p.Name} - {p.Age}");
}
#endregion

#region Сложные фильтры
//  Теперь рассмотрим более сложные фильтры. Например, в классе пользователя есть список языков, которыми
//  владеет пользователь. Что если нам надо отфильтровать пользователей по языку:
var selectedPeopleByLang = from p in _people
                           from lang in p.Languages
                           where p.Age < 28
                           where lang == "english"
                           select p;
//  Для создания аналогичного запроса с помощью методов расширения применяется метод SelectMany:
var selectedPeopleBtLang2 = _people.SelectMany(u => u.Languages,
    (u, l) => new { Peson = u , Lang = l})
    .Where(u => u.Lang == "english" && u.Peson.Age <28).Select(u => u.Peson);
//  users.Where(x => x.Languages.Contains("английский"));
#endregion

#region Фильтрация по типу данных
//  Дополнительный метод расширения - OfType() позволяет отфильтровать данные коллекции по определенному типу:
var people33 = new List<Person2>
{
    new Student("Tom"),
    new Person2("Sam"),
    new Student("Bob"),
    new Employee("Mike")
};

var students = people33.OfType<Student>();
foreach (Student student in students)
{
    Console.WriteLine(student);
}

#endregion
record class Person(string Name, int Age, List<string> Languages);
record class Person2(string Name);
record class Student(string Name) : Person2(Name);
record class Employee(string Name) : Person2(Name);