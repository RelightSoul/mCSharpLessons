//  Изменение документа в LINQ to XML

#region Добавление данных
//  Для добавления данных в документ xml у объекта XElement применяется метод Add(), в который передается добавляемый объект:
using System.Xml.Linq;

XDocument xdoc = XDocument.Load("people.xml");
XElement? root = xdoc.Element("people");
if (root!=null)
{
    // добавляем новый элемент
    root.Add(new XElement("person",
        new XAttribute("name", "Sam"),
        new XElement("company", "JetBrains"),
        new XElement("age",28)));
    xdoc.Save("people.xml");
}
// выводим xml-документ на консоль
Console.WriteLine(xdoc);
#endregion

#region Изменение данных
//  Для изменения данных в документ xml необходимо получить элемент, который надо изменить, и затем можно отредактировать
//  значения его отдельных атрибутов или вложенных элементов. Изменим элемент person, в котором атрибут name = "Tom":
// получим элемент person с name = "Tom"
var tom = xdoc.Element("people")?
    .Elements("person")
    .FirstOrDefault(p => p.Attribute("name")?.Value == "Tom");
if (tom!=null)
{
    var name = tom.Attribute("name");
    if (name != null)
    {
        name.Value = "Tomas";
    }
    var age = tom.Element("age");
    if (age != null)
    {
        age.Value = "22";
    }
    xdoc.Save("people.xml");
}
Console.WriteLine(xdoc);
#endregion

#region Удаление данных
//  Для удаления данных в документе xml у удаляемого объекта XElement вызывается метод Remove(). Например,
//  удалим элемент person, в котором атрибут name = "Bob":
if (root != null)
{
    // получим элемент person с name = "Bob"
    var bob = root.Elements("person")
        .FirstOrDefault(p => p.Attribute("name")?.Value == "Bob");
    // и удалим его
    if (bob != null)
    {
        bob.Remove();
    }
    xdoc.Save("people.xlm");
}
// выводим xml-документ на консоль
Console.WriteLine(xdoc);
//  Соответственно, если необходимо удалить атрибут, то у удаляемого объекта XAttribute также вызывается метод Remove.
#endregion

