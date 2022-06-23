// Двухсвязный список LinkedList<T>

//  Класс LinkedList<T> представляет двухсвязный список, в котором каждый элемент хранит ссылку одновременно
//  на следующий и на предыдущий элемент.

#region Создание связанного списка
//  Для создания связного списка можно принименять один из его конструктора. Например, создадим пустой
//  связный список:
//  LinkedList<string> people = new LinkedList<string>();

//  Также можно в конструктор передать коллекцию элементов, например, список List, по которому будет создан
//  связный список:
var employers = new List<string> { "Tom","Sam","Bob"};

LinkedList<string> people = new LinkedList<string>(employers);
#endregion

#region LinkedListNode
//  Если в простом списке List<T> каждый элемент представляет объект типа T, то в LinkedList<T> каждый узел
//  представляет объект класса LinkedListNode<T>. А добавляемые в связанный список элементы T фактически
//  обертываются в объект LinkedListNode.

// Класс LinkedListNode имеет следующие свойства:
// Value: возвращает или устанавливает само значение узла, представленное типом T
// Next: возвращает ссылку на следующий элемент типа LinkedListNode<T> в списке. Если следующий элемент
// отсутствует, то имеет значение null
// Previous: возвращает ссылку предыдущий элемент типа LinkedListNode<T> в списке. Если предыдущий элемент
// отсутствует, то имеет значение null
#endregion

#region Свойства LinkedList
//  Класс LinkedList определяет следующие свойства:
//  Count: количество элементов в связанном списке
//  First: первый узел в списке в виде объекта LinkedListNode<T>
//  Last: последний узел в списке в виде объекта LinkedListNode<T>

Console.WriteLine(people.Count);         // 3
Console.WriteLine(people.First?.Value);  // Tom
Console.WriteLine(people.Last?.Value);   // Bob

//  Используя свойства LinkedList и LinkedListNode, можно пройтись по всем элементам списка в прямом или
//  обратном порядке:
var curNodeIsFirst = people.First;
while (curNodeIsFirst != null)
{
    Console.Write("{0}\t", curNodeIsFirst.Value);
    curNodeIsFirst = curNodeIsFirst.Next;
}
Console.WriteLine();
var curNodeIsLast = people.Last;
while (curNodeIsLast != null)
{
    Console.Write("{0}\t", curNodeIsLast.Value);
    curNodeIsLast = curNodeIsLast.Previous;
}
Console.WriteLine();
#endregion

#region Методы LinkedList
//  Используя методы класса LinkedList<T>, можно обращаться к различным элементам, как в конце, так и в
//  начале списка:

//AddAfter(LinkedListNode < T > node, LinkedListNode < T > newNode): вставляет узел newNode в список после узла node.

//AddAfter(LinkedListNode<T> node, T value): вставляет в список новый узел со значением value после узла node.

//AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode): вставляет в список узел newNode перед узлом node.

//AddBefore(LinkedListNode<T> node, T value): вставляет в список новый узел со значением value перед узлом node.

//AddFirst(LinkedListNode<T> node): вставляет новый узел в начало списка

//AddFirst(T value): вставляет новый узел со значением value в начало списка

//AddLast(LinkedListNode<T> node): вставляет новый узел в конец списка

//AddLast(T value): вставляет новый узел со значением value в конец списка

//RemoveFirst(): удаляет первый узел из списка. После этого новым первым узлом становится узел, следующий за удаленным

//RemoveLast(): удаляет последний узел из списка

var _people = new LinkedList<string>();
_people.AddLast("Tom"); // вставляем узел со значением Tom на последнее место
                         //так как в списке нет узлов, то последнее будет также и первым
_people.AddFirst("Bob"); // вставляем узел со значением Bob на первое место

if (_people.First != null )
{
    _people.AddAfter(_people.First, "Mike");
}
/// вставляем после первого узла новый узел со значением Mike

var company = new LinkedList<Person>();
company.AddFirst(new Person {Name = "Paul" });
company.AddLast(new Person {Name = "Rail" });
company.AddAfter(company?.First, new Person { Name = "Kristina" });
foreach (Person p in company)
{
    Console.Write("{0}\t",p.Name);
}

class Person
{
    public string Name { get; set; }
}
#endregion
