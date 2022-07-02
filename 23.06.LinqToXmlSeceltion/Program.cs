//  Выборка элементов в LINQ to XML
//  Большим преимуществом LINQ to XML является то, что эта технология позволяет легко извлекать нужные элементы
//  из документа xml.

using System.Xml.Linq;

XDocument xdoc = XDocument.Load("people.xml");
// получаем корневой узел
XElement? people = xdoc.Element("people");
if (people is not null)
{
    // проходим по всем элементам person
    foreach (XElement person in people.Elements("person"))
    {

        XAttribute? name = person.Attribute("name");
        XElement? company = person.Element("company");
        XElement? age = person.Element("age");

        Console.WriteLine($"Person: {name?.Value}");
        Console.WriteLine($"Company: {company?.Value}");
        Console.WriteLine($"Age: {age?.Value}");

        Console.WriteLine(); //  для разделения при выводе на консоль
    }
}
//  Чтобы начать работу с имеющимся xml-файлом, надо сначала загрузить его с помощью статического метода XDocument.Load(),
//  в который передается путь к файлу.

//  XDocument xdoc = XDocument.Load("people.xml");

//  Поскольку xml хранит иерархически выстроенные элементы, то и для доступа к элементам надо идти начиная с высшего уровня
//  в этой иерархии и далее вниз. Так, для получения элементов person и доступа к ним надо сначала обратиться к корневому
//  элементу, а через него уже к элементам person:

//  получаем корневой узел
//  XElement? people = xdoc.Element("people");
//  if (people is not null)
//  {
//      // проходим по всем элементам person
//      foreach (XElement person in people.Elements("person")) { }

//  Метод Element("имя_элемента") возвращает первый найденный элемент с таким именем.Метод Elements("имя_элемента")
//  возвращает коллекцию одноименных элементов. В данном случае мы получаем коллекцию элементов person и поэтому можем
//  перебрать ее в цикле.

//  Спускаясь дальше по иерархии вниз, мы можем получить атрибуты или вложенные элементы, например, получение элемента<company>
//  XElement? company = person.Element("company");

//  Значение простых элементов, которые содержат один текст, можно получить с помощью свойства Value:
//  string? companyValue = person.Element("company")?.Value;

//  Сочетая операторы Linq и LINQ to XML можно довольно просто извлечь из документа данные и затем обработать их. Например:

var microsoft = xdoc.Element("people.xml")?.
    Elements("person")
    .Where(p => p.Element("company")?.Value == "Microsoft")
    .Select(p => new
    {
        name = p.Attribute("name")?.Value,
        age = p.Element("age")?.Value,
        company = p.Element("company")?.Value
    });
if (microsoft is not null)
{
    foreach (var person in microsoft)
    {
        Console.WriteLine($"Name: {person.name}  Age: {person.age}");
    }
}
// В данном случае выбираем все элементы person, у которых вложенный элемент "company" равен "Microsoft". Далее на основе
// полученной выборке создаем набор анонимных объектов с тремя свойствами. Под вывод также можно было бы создать специально
// какой-нибудь класс или структуру и использовать их вместо анонимного объекта.
