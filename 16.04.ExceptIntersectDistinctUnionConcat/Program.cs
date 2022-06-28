//  Объединение, пересечение и разность коллекций

//  LINQ предоставляет несколько методов для работы с коллекциями как с множествами, а именно находить их
//  разность, объединение и пересечение.

#region Разность последовательностей
//  С помощью метода Except() можно получить разность двух последовательностей:
string[] soft = { "Microsoft", "Google", "Apple" };
string[] hard = { "Apple", "IBM", "Samsung" };

var result = soft.Except(hard);
foreach (string s in result)
{
    Console.WriteLine(s);    // Microsoft, Google
}
//  В данном случае из массива soft убираются все элементы, которые есть в массиве hard.
#endregion

#region Пересечение последовательностей
//  Для получения пересечения последовательностей, то есть общих для обоих наборов элементов, применяется
//  метод Intersect:
var result2 = soft.Intersect(hard);
foreach (string s in result2)
{
    Console.WriteLine(s);    // Apple
}
//  Так как оба набора имеют только один общий элемент, то соответственно только он и попадет
//  в результирующую выборку
#endregion

#region Удаление дубликатов
//  Для удаления дублей в наборе используется метод Distinct:
string[] soft2 = { "Microsoft", "Google", "Apple", "Microsoft", "Google" };

var result3 = soft2.Distinct();
foreach (string s in result3)
{
    Console.WriteLine(s);       // Microsoft, Google, Apple
}
#endregion

#region Объединение последовательностей
//  Для объединения двух последовательностей используется метод Union. Его результатом является новый набор,
//  в котором имеются элементы, как из первой, так и из второй последовательности. Повторяющиеся элементы
//  добавляются в результат только один раз:
var result4 = soft.Union(hard);
foreach (string s in result4)
{
    Console.WriteLine(s);    //  Microsoft, Google, Apple, IBM, Samsung
}

//  Если же нам нужно простое объединение двух наборов, то мы можем использовать метод Concat:

var result5 = soft.Concat(hard);
foreach (string s in result5)
{
    Console.WriteLine(s);    //  Microsoft, Google, Apple, Apple, IBM, Samsung
}
//  В этом случае те элементы, которые встречаются в обоих наборах, дублируются в резутирующей последовательности.
#endregion

#region Работа со сложными объектами
//  Для сравнения объектов в последовательностях применяются реализации методов GetHeshCode() и Equals(). Поэтому
//  если мы хотим работать с последовательностями, которые содержат объекты своих классов и структур, то нам
//  необходимо определить для них подобные методы:
Person[] students = { new Person("Tom"), new Person("Bob"), new Person("Sam") };
Person[] employees = { new Person("Tom"), new Person("Bob"), new Person("Mike") };

var people = students.Union(employees);
foreach (Person person in people)
{
    Console.WriteLine(person.Name);
}

class Person
{
    public string Name { get; set; }
    public Person(string name) => Name = name;
    
    // сравниваем по имени
    public override bool Equals(object? obj)
    {
        if (obj is Person p)
        {
            return Name == p.Name;
        }
        else
        {
            return false;
        }
    }

    // получаем хэш имени для сравнения
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }    
}
#endregion