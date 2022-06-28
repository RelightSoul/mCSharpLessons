//  Проверка наличия и получение элементов
//  Ряд методов в LINQ позволяют проверить наличие элементов в коллекции и получить их.

#region All
//  Метод All() проверяет, соответствуют ли все элементы условию. Если все элементы соответствуют условию,
//  то возвращается true.
using System.Diagnostics.CodeAnalysis;

string[] people = { "Tom", "Tim", "Bob", "Sam" };
// проверяем, все ли элементы имеют длину в 3 символа
bool allHas3Chars = people.All(p => p.Length == 3); // true
// проверяем, все ли строки начинаются на T
bool allStartWithT = people.All(p => p.StartsWith("T")); //false
#endregion

#region Any
//  Метод Any() действует подобным образом, только возвращает true, если хотя бы один элемент коллекции
//  определенному условию:
bool anyHasMore3Chars = people.Any(p => p.Length > 3); //false
bool anyStartWithT = people.Any(p => p.StartsWith("T")); //true
#endregion

#region Contains
//  Метод Contains() возвращает true, если коллекция содержит определенный элемент.
bool hasTom = people.Contains("Tom");  //true
bool hasMike = people.Contains("Mike"); //false

//  Стоит отметить, что для сравнения объектов применяется реализация метода Equals. Соответственно если
//  мы работаем с объектами своих типов, то мы можем реализовать данный метод.
Person[] people2 = { new Person("Tom"), new Person("Sam"), new Person("Bob") };
var tom = new Person("Tom");
var hasTom2 = people2.Contains(tom);  //true

//  о стоит отметить, что Contains не всегда может вернуть ожидаемые данные. 
bool hasTom3 = people.Contains("tom"); //false
//  В данном случае в массиве нет строки "Tom", а есть строка "tom". Поэтому вызов people.Contains("Tom")
//  возвратит false.Но подобное поведение не всегда может быть желательным. И в этом случае мы можем задать
//  логику сравнения с помощью реализации интерфейса IComparer и затем передать ее в качестве второго параметра
//  в метод Contains:
#endregion

#region First/FirstOrdefault
//  Метод First() возвращает первый элемент последовательности:
var firstPeople = people.First();  // Tom
//  Также в метод First можно передать метод, который представляет условие. В этом случае метод возвращает
//  первый элемент, который соответствует условию:
string[] people3 = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
var firstPeopleWith4Chars = people3.First(p => p.Length == 4);  // Kate
//  Стоит учитывать, что если коллекция пуста или в коллекции нет элементов, который соответствуют условию,
//  то будет сгенерировано исключение.
#endregion

#region FirstOrdefault
//  Метод FirstOrDefault() также возвращает первый элемент и также может принимать условие, только если
//  коллекция пуста или в коллекции не окажется элементов, которые соответствуют условию, то метод возвращает
//  значение по умолчанию:
var firstPeopleWith5Chars = people3.FirstOrDefault(p => p.Length == 5);  // пусто
//  Но мы можем настроить значение по умолчанию, передав его в качестве одного из аргументов:
var firstPeopleWith5Chars2 = people3.FirstOrDefault(p => p.Length == 5, "Undefined");  // Undefined
#endregion

#region Last и LastOrDefault
//  Метод Last() аналогичен по работе методу First, только возвращает последний элемент. Если коллекция не
//  содержит элемент, который соответствуют условию, или вообще пуста, то метод генерирует исключение.
//  Метод LastOrDefault() возвращает последний элемент или значение по умолчанию, если коллекция не содержит
//  элемент, который соответствуют условию, или вообще пуста:
#endregion

class CustomStringComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        if (x is null || y is null)
        {
            return false;
        }
        else
        {
            return x.ToLower() == y.ToLower();
        }
    }

    public int GetHashCode([DisallowNull] string obj)
    {
        return obj.ToLower().GetHashCode();
    }
}
//  Интерфейс IEqualityComparer типизируется типом сравниваемых данных (в данном случае типом String).
//  Для реализации этого интерфейса необходимо определить методы Equals и GetHashCode. В методе Equals
//  сравниваем строки в нижнем регистре, а в методе GetHashCode возвращаем возвращаем хеш-код строки в
//  нижнем регистре.

class Person
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Person p)
        {
            return Name == p.Name;
        }    
        return false;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}