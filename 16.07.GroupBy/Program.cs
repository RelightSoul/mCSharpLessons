//  Группировка
//  Для группировки данных по определенным параметрам применяется оператор group by и метод GroupBy().

#region Оператор group by
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
    new Person("Kate", "JetBrains"), new Person("Alice", "Microsoft"),
};

var companies = from person in people
                group person by person.Company;

foreach (var company in companies)
{
    Console.WriteLine(company.Key);
    foreach (var person in company)
    {
        Console.WriteLine(person.Name);
    }
    Console.WriteLine();
}
//  Если в выражении LINQ последним оператором, выполняющим операции над выборкой, является group, то
//  оператор select не применяется.

//  в данном случае группировка идет по свойству Company. Результатом оператора group является выборка,
//  которая состоит из групп. Каждая группа представляет объект IGrouping<K, V>: параметр K указывает
//  на тип ключа - тип свойства, по которому идет группировка (здесь это тип string). А параметр V
//  представляет тип сгруппированных объектов - в данном случае группируем объекты Person.

//  Каждая группа имеет ключ, который мы можем получить через свойство Key: g.Key.Здесь это будет название компании.

//  Все элементы внутри группы можно получить с помощью дополнительной итерации. Элементы группы имеют тот
//  же тип, что и тип объектов, которые передавались оператору group, то есть в данном случае объекты типа Person.
#endregion

#region GroupBy
//  В качестве альтернативы можно использовать метод расширения GroupBy. Он имеет ряд перегрузок,
//  возьмем самую простую из них:
//  GroupBy<TSource,TKey> (Func<TSource,TKey> keySelector);

//  Данная версия получает делегат, которые в качестве параметра принимает каждый элемент коллекции
//  и возвращает критерий группировки.
var companies2 = people.GroupBy(p => p.Company);
foreach (var company in companies2)
{
    Console.WriteLine(company.Key);
    foreach (var person in company)
    {
        Console.WriteLine(person.Name);
    }
    Console.WriteLine();
}
#endregion

#region Создание нового объекта при группировке
//  Теперь изменим запрос и создадим из группы новый объект.
var companies3 = from p in people
                 group p by p.Company into g
                 select new { Name = g.Key, Count = g.Count()};
foreach (var company in companies3)
{
    Console.WriteLine($"{company.Name} - {company.Count}");
}
Console.WriteLine("------------------------------------");
//  group p by p.Company into g
//  определяет переменную g, которая будет содержать группу. С помощью этой переменной мы можем затем
//  создать новый объект анонимного типа (хотя также можно под данную задачу определить новый класс):
//  select new { Name = g.Key, Count = g.Count() }

//  Теперь результат запроса LINQ будет представлять набор объектов таких анонимных типов,
//  у которых два свойства Name и Count.
#endregion

#region Вложенные запросы
var companies4 = from person in people
                 group person by person.Company into g
                 select new
                 {
                     Name = g.Key,
                     Count = g.Count(),
                     Employees = from p in g select p
                 };
foreach (var company in companies4)
{
    Console.WriteLine($"{company.Name} - {company.Count}");
    foreach (var p in company.Employees)
    {
        Console.WriteLine(p.Name);
    }
    Console.WriteLine();
}
#endregion

record class Person(string Name, string Company);