// XPath представляет язык запросов в XML. Он позволяет выбирать элементы, соответствующие определенному селектору.

//Рассмотрим некоторые наиболее распространенные селекторы:
//  . выбор текущего узла
//  ..  выбор родительского узла
//  * выбор всех дочерних узлов текущего узла
//  person    выбор всех узлов с определенным именем, в данном случае с именем "person"
//  @name выбор атрибута текущего узла, после знака @ указывается название атрибута (в данном случае "name")
//  @+    выбор всех атрибутов текущего узла
//  element[3]  выбор определенного дочернего узла по индексу, в данном случае третьего узла
//  //person  выбор в документе всех узлов с именем "person"
//  person[@name='Tom']   выбор элементов с определенным значением атрибута. В данном случае выбираются все элементы "person"
//  с атрибутом name='Tom'
//  person[company='Microsoft']   выбор элементов с определенным значением вложенного элемента. В данном случае выбираются
//  все элементы "person", у которых дочерний элемент "company" имеет значение 'Microsoft'
//  //person/company  выбор в документе всех узлов с именем "company", которые находятся в элементах "person"

//  Действие запросов XPath основано на применении двух методов класса XmlElement:
//  SelectSingleNode(): выбор единственного узла из выборки. Если выборка по запросу содержит несколько узлов, то выбирается
//  первый
//  SelectNodes(): выборка по запросу коллекции узлов в виде объекта XmlNodeList

using System.Xml;

XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
XmlElement? xRoot = xDoc.DocumentElement;

// выбор всех дочерних узлов
XmlNodeList? nodes = xRoot?.SelectNodes("*");
if (nodes is not null)
{
    foreach (XmlNode node in nodes)
        Console.WriteLine(node.OuterXml);
}

//Выберем узел, у которого атрибут name имеет значение "Tom":
XmlNode childnode = xRoot.SelectSingleNode("person[@name='Tom']");
if (childnode != null)
    Console.WriteLine(childnode.OuterXml);

//  Выберем узел, у которого вложенный элемент "company" имеет значение "Microsoft":
XmlNode? tomNode = xRoot?.SelectSingleNode("person[@name='Tom']");
Console.WriteLine(tomNode?.OuterXml);

//  Допустим, нам надо получить только компании. Для этого надо осуществить выборку вниз по иерархии элементов:
XmlNodeList? companyNodes = xRoot?.SelectNodes("//person/company");
if (companyNodes is not null)
{
    foreach (XmlNode node in companyNodes)
        Console.WriteLine(node.InnerText);
}