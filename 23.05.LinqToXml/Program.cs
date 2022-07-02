//  Linq to Xml. Создание документа XML

//  Еще один подход к работе с Xml представляет технология LINQ to XML. Вся функциональность LINQ to XML содержится
//  в пространстве имен System.Xml.Linq. Рассмотрим основные классы этого пространства имен:
//  XAttribute: представляет атрибут xml - элемента
//  XComment: представляет комментарий
//  XDocument: представляет весь xml - документ
//  XElement: представляет отдельный xml - элемент

//  Ключевым классом является XElement, который позволяет получать вложенные элементы и управлять ими. Среди его методов
//  можно отметить следующие:
//  Add(): добавляет новый атрибут или элемент
//  Attributes(): возвращает коллекцию атрибутов для данного элемента
//  Elements(): возвращает все дочерние элементы данного элемента
//  Remove(): удаляет данный элемент из родительского объекта
//  RemoveAll(): удаляет все дочерние элементы и атрибуты у данного элемента

//  Итак, используем функциональность LINQ to XML и создадим новый XML-документ:
using System.Xml.Linq;

XDocument xdoc = new XDocument();
// создаем первый элемент person
XElement tom = new XElement("person");
// создаем атрибут name
XAttribute tomNameAttr = new XAttribute("name", "Tom");
// создаем два элемента company и age 
XElement tomCompanyElem = new XElement("company", "Microsoft");
XElement tomAgeElem = new XElement("age", 37);
// добавляем атрибут и элементы в первый элемент person
tom.Add(tomNameAttr);
tom.Add(tomCompanyElem);
tom.Add(tomAgeElem);

// создаем второй элемент person
XElement bob = new XElement("person");
// создаем для него атрибут name
XAttribute bobNameAttr = new XAttribute("name", "Bob");
// и два элемента - company и age
XElement bobCompanyElem = new XElement("company", "Google");
XElement bobAgeElem = new XElement("age", 41);
bob.Add(bobNameAttr);
bob.Add(bobCompanyElem);
bob.Add(bobAgeElem);
// создаем корневой элемент
XElement people = new XElement("people");
// добавляем два элемента person в корневой элемент
people.Add(tom);
people.Add(bob);
// добавляем корневой элемент в документ
xdoc.Add(people);
//сохраняем документ
xdoc.Save("people.xml");

Console.WriteLine("Data saved");
//  Чтобы создать документ, нам нужно создать объект класса XDocument. Это объект самого верхнего уровня в хml-документе.

//  Элементы создаются с помощью конструктора класса XElement. Конструктор имеет ряд перегруженных версий. Первый параметр
//  конструктора передает название элемента, например, person. Второй параметр передает значение этого элемента.

//  Создание атрибута аналогично созданию элемента. Затем все атрибуты и элементы добавляются в элементы person с помощью
//  метода Add().

//  Так как документ xml должен иметь один корневой элемент, то затем все элементы person добавляются в один контейнер -
//  элемент people.

//  В конце корневой элемент добавляется в объект XDocument, и этот объект сохраняется на диске в xml-файл с помощью метода
//  Save().

//  Конструктор класса XElement позволяют задать набор объектов, которые будут входить в элемент. И предыдущий пример мы
//  могли бы сократить следующим способом:

XDocument xdoc2 = new XDocument(new XElement("people",
    new XElement("person",
        new XAttribute("name", "Tom"),
        new XElement("company", "Microsoft"),
        new XElement("age", 37)),
    new XElement("person",
        new XAttribute("name", "Bob"),
        new XElement("company", "Google"),
        new XElement("age", 41))));
xdoc2.Save("people2.xml");

Console.WriteLine("Data saved");
