// Интересные возможности при разработке в C# и .NET с использованием DLR предоставляет пространство имен
// System.Dynamic и в частности класс ExpandoObject. Он позволяет создавать динамические объекты, наподобие
// тех, что используются в javascript:
using System.Dynamic;
// определяем объект, который будет хранять ряд значений
dynamic person = new System.Dynamic.ExpandoObject();
person.Name = "Tom";
person.Age = 46;
person.Languages = new List<string> { "english", "german", "french" };

Console.WriteLine($"{person.Name} - {person.Age}");
foreach (var lang in person.Languages)
    Console.WriteLine(lang);

// объявляем метод
person.IncrementAge = (Action<int>)(x => person.Age += x);
person.IncrementAge(6); // увеличиваем возраст на 6 лет
Console.WriteLine($"{person.Name} - {person.Age}");
//  У динамического объекта ExpandoObject можно объявить любые свойства, например, Name, Age, Languages,
//  которые могут представлять самые различные объекты. Кроме того, можно задать методы с помощью делегатов.

#region DynamicObject
//На ExpandoObject по своему действию похож другой класс - DynamicObject. Он также позволяет задавать
//динамические объекты, но применяется в более изощренных и сложных ситуациях и когда необходим больший
//контроль над динамическими объектами. Тогда как ExpandoObject больше подходит для простых ситуаций, где
//не требуется определять какие-то специфические операции или статические компоненты.

//Для использования DynamicObject надо создать свой класс, унаследовав его от DynamicObject и реализовав
//его методы:

//TryBinaryOperation(): выполняет бинарную операцию между двумя объектами. Эквивалентно стандартным бинарным
//операциям, например, сложению x + y)

//TryConvert(): выполняет преобразование к определенному типу. Эквивалентно базовому преобразованию в C#,
//например, (SomeType) obj

//TryCreateInstance(): создает экземпляр объекта

//TryDeleteIndex(): удаляет индексатор

//TryDeleteMember(): удаляет свойство или метод

//TryGetIndex(): получает элемент по индексу через индексатор. В C# может быть эквивалентно следующему
//выражению int x = collection[i]

//TryGetMember(): получаем значение свойства. Эквивалентно обращению к свойству, например, string n = person.Name

//TryInvoke(): вызов объекта в качестве делегата

//TryInvokeMember(): вызов метода

//TrySetIndex(): устанавливает элемент по индексу через индексатор. В C# может быть эквивалентно следующему
//выражению collection[i] = x;

//TrySetMember(): устанавливает свойство.Эквивалентно присвоению свойству значения, например: person.Name = "Tom"

//TryUnaryOperation(): выполняет унарную операцию подобно унарным операциям в C#: x++

//Каждый из этих методов имеет одну и ту же модель определения: все они возвращают логическое значение,
//показывающее, удачно ли прошла операция. В качестве первого параметра все они принимают объект связывателя
//или binder.Если метод представляет вызов индексатора или метода объекта, которые могут принимать параметры,
//то в качестве второго параметра используется массив object[] - он хранит переданные в метод или индексатор
//аргументы.

//Почти все операции, кроме установки и удаления свойств и индексаторов, возвращают определенное значение
//(например, если мы получаем значение свойства). В этом случае применяется третий параметр out object vaue,
//который предназначен для хранения возвращаемого объекта


// создаем объект
dynamic person2 = new PersonObject();
// устанавливаем ряд свойств
person2.Name = "Tom";
person2.Age = 23;
// определяем метод для изменения свойства Age
Func<int, int> increment = (int n) => { person.Age += n; return person.Age; };
person2.IncrementAge = increment;

Console.WriteLine($"{person.Name} - {person.Age}"); // Tom - 23
person2.IncrementAge(4); // применяем метод
Console.WriteLine($"{person.Name} - {person.Age}"); // Tom - 27

class PersonObject : DynamicObject
{
    // словарь для хранения всех свойств
    Dictionary<string, object> members = new Dictionary<string, object>();

    // установка свойства
    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        if (value is not null)
        {
            members[binder.Name] = value;
            return true;
        }
        return false;
    }
    // получение свойства
    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        result = null;
        if (members.ContainsKey(binder.Name))
        {
            result = members[binder.Name];
            return true;
        }
        return false;
    }
    // вызов метода
    public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
    {
        result = null;
        if (args?[0] is int number)
        {
            // получаем метод по имен
            dynamic method = members[binder.Name];
            // вызываем метод, передавая его параметру значение args?[0]
            result = method(number);
        }
        // если result не равен null, то вызов метода прошел успешно
        return result != null;
    }
}
//  Выражение person.Name = "Tom" будет вызывать метод TrySetMember, в который в качестве второго параметра
//  будет передаваться строка "Tom".

//  Выражение return person.Age; вызывает метод TryGetMember.

//  Также у объекта person определен метод IncrementAge, который представляет действия лямбда-выражения
//  (int n) => { person.Age += n; return person.Age; };. Это выражение принимает число n, увеличивает на
//  это число свойство Age и возвращает новое значение person.Age. И при вызове этого метода будет происходить
//  обращение к методу TryInvokeMember. И, таким образом, произойдет приращение значения свойства person.Age.
#endregion