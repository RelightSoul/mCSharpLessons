// Класс Stack<T> представляет коллекцию, которая использует алгоритм LIFO ("последний вошел - первый вышел").
// При такой организации каждый следующий добавленный элемент помещается поверх предыдущего.
// Извлечение из коллекции происходит в обратном порядке - извлекается тот элемент, который находится
// выше всех в стеке.

//  Стек - довольно часто встречаемая структура данных в реальной жизни. Банальные примеры стеков - стопка книг
//  или тарелок, где каждую новую книгу или тарелку помещают поверх предыдущей. А извлекают из этой стопки
//  книги/тарелки в обратном порядке - сначала самую верхнюю и так далее. Другой пример - одежда: допустим,
//  человек выходит на улицу в зимнюю погоду и для этого сначала одевает майку, потом рубашку, затем свитер,
//  и в конце куртку. Когда человек снимает с себя одежду - он делает это в обратном порядке: сначала снимает
//  куртку, потом свитер и так далее.

#region Создание стека
//  Для создания стека можно использовать один из трех конструкторов. Прежде всего можно создать пустой стек:
//  Stack<string> people = new Stack<string>();
//  При создании пустого стека можно указать емкость стека:
//  Stack<string> people = new Stack<string>(16);
//  Также можно инициализировать стек элементами из другой коллекции или массивом:
var employees = new List<string> { "Tom", "Sam", "Bob" };
Stack<string> people = new Stack<string>(employees);
foreach (var person in people) Console.WriteLine(person);

Console.WriteLine(people.Count); // 3
#endregion

#region Методы Stack
//В классе Stack можно выделить следующие методы:

//Clear: очищает стек

//Contains: проверяет наличие в стеке элемента и возвращает true при его наличии

//Push: добавляет элемент в стек в верхушку стека

//Pop: извлекает и возвращает первый элемент из стека

//Peek: просто возвращает первый элемент из стека без его удаления

var _people = new Stack<string>();
_people.Push("Tom");  // people = { Tom }
_people.Push("Sam");  // people = { Sam, Tom }
_people.Push("Bob");  // people = { Bob, Sam, Tom }

// получаем первый элемент стека без его удаления
string headP = _people.Peek();
Console.WriteLine(headP);   // Bob
                            // people = { Bob, Sam, Tom }
string p1 = _people.Pop();
Console.WriteLine(p1);      // Bob
                            // people = { Sam, Tom }
string p2 = _people.Pop();
Console.WriteLine(p2);      // Sam 
                            // people = { Tom }

//  Стоит отметить, что если с помощью методов Peek или Pop мы попытаемся получить первый элемент стека,
//  который пуст, то программа выдаст исключение. Соответственно перед получением элемента мы можем проверять
//  количество элементов в стеке:
if (_people.Count > 0)
{
    var person = _people.Peek();
    _people.Pop();
}

//  Либо можно использовать пару методов:

//  bool TryPop(out T result): удаляет из стека первый элемент и передает его в переменную result,
//  возвращает true, если очередь не пуста и элемент успешно получен.

//  bool TryPeek(out T result): передает в переменную result первый элемент стека без его извлечения,
//  возвращает true, если элемент успешно получен.

var nPeople = new Stack<string>();
nPeople.Push("Sam");

var succRemove = nPeople.TryPop(out var s);  // succRemove = true
if (succRemove) Console.WriteLine(s);

var succPeek = nPeople.TryPeek(out string r); // succPeek = false
if (succPeek) Console.WriteLine(r);
#endregion
