//  Работа с XML с помощью классов System.Xml

//  Для работы с XML в C# можно использовать несколько подходов. В первых версиях фреймворка основной функционал
//  работы с XML предоставляло пространство имен System.Xml. В нем определен ряд классов, которые позволяют
//  манипулировать xml-документом:
//  XmlNode: представляет узел xml. В качестве узла может использоваться весь документ, так и отдельный элемент
//  XmlDocument: представляет весь xml-документ
//  XmlElement: представляет отдельный элемент. Наследуется от класса XmlNode
//  XmlAttribute: представляет атрибут элемента
//  XmlText: представляет значение элемента в виде текста, то есть тот текст, который находится в элементе
//  между его открывающим и закрывающим тегами
//  XmlComment: представляет комментарий в xml
//  XmlNodeList: используется для работы со списком узлов

//  Ключевым классом, который позволяет манипулировать содержимым xml, является XmlNode, поэтому рассмотрим
//  некоторые его основные методы и свойства:
//  Свойство Attributes возвращает объект XmlAttributeCollection, который представляет коллекцию атрибутов
//  Свойство ChildNodes возвращает коллекцию дочерних узлов для данного узла
//  Свойство HasChildNodes возвращает true, если текущий узел имеет дочерние узлы
//  Свойство FirstChild возвращает первый дочерний узел
//  Свойство LastChild возвращает последний дочерний узел
//  Свойство InnerText возвращает текстовое значение узла
//  Свойство InnerXml возвращает всю внутреннюю разметку xml узла
//  Свойство Name возвращает название узла. Например, <user> - значение свойства Name равно "user"
//  Свойство ParentNode возвращает родительский узел у текущего узла

//  Применим эти классы и их функционал. И вначале для работы с xml создадим новый файл. Назовем его people.xml
//  и определим в нем следующее содержание:

//<? xml version = "1.0" encoding = "utf-8" ?>
//< people >
//  < person name = "Tom" >
//    < company > Microsoft </ company >
//    < age > 37 </ age >
//  </ person >
//  < person name = "Bob" >
//    < company > Google </ company >
//    < age > 41 </ age >
//  </ person >
//</ people >

//  Теперь пройдемся по этому документу и выведем его данные на консоль:
using System.Xml;

XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
// получим корневой элемент
XmlElement? xRoot = xDoc.DocumentElement;
if (xRoot != null)
{
    // обход всех узлов в корневом элементе
    foreach (XmlElement xnode in xRoot)
    {
        // получаем атрибут name
        XmlNode? attr = xnode.Attributes.GetNamedItem("name");
        Console.WriteLine(attr?.Value);

        // обходим все дочерние узлы элемента user
        foreach (XmlNode childnode in xnode.ChildNodes)
        {
            // если узел - company
            if (childnode.Name == "company")
            {
                Console.WriteLine($"Company: {childnode.InnerText}");
            }
            // если узел age
            if (childnode.Name == "age")
            {
                Console.WriteLine($"Age: {childnode.InnerText}");
            }
        }
        Console.WriteLine();
    }
}
//Чтобы начать работу с документом xml, нам надо создать объект XmlDocument и затем загрузить в него xml-файл:
//xDoc.Load("people.xml");

//При разборе xml для начала мы получаем корневой элемент документа с помощью свойства xDoc.DocumentElement.
//Далее уже происходит собственно разбор узлов документа.

//В цикле foreach(XmlNode xnode in xRoot) пробегаемся по всем дочерним узлам корневого элемента. Так как
//дочерние узлы представляют элементы <person>, то мы можем получить их атрибуты: XmlNode attr =
//xnode.Attributes.GetNamedItem("name"); и вложенные элементы: foreach (XmlNode childnode in xnode.ChildNodes)

//    Чтобы определить, что за узел перед нами, мы можем сравнить его название: if (childnode.Name == "company")

//    Подобным образом мы можем создать объекты классов и структур по данным из xml:
var people = new List<Person>();

if (xRoot != null)
{
    foreach (XmlElement xnode in xRoot)
    {
        Person person = new Person();
        XmlNode? attr = xnode.Attributes.GetNamedItem("name");
        person.Name = attr?.Value;

        foreach (XmlNode childnode in xnode.ChildNodes)
        {
            if (childnode.Name == "company")
                person.Company = childnode.InnerText;

            if (childnode.Name == "age")
                person.Age = int.Parse(childnode.InnerText);
        }
        people.Add(person);
    }
    foreach (var person in people)
        Console.WriteLine($"{person.Name} ({person.Company}) - {person.Age}");
}

class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Company { get; set; }
}

//  В данном случае определен класс Person с тремя свойствами. При переборе узлов файла xml значения
//  элементов и их атрибутов передается объекту класса Person.
