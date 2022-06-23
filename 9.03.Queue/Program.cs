// Класс Queue<T> представляет обычную очередь, которая работает по алгоритму FIFO ("первый вошел - первый вышел").

#region Создание очереди
//  Для создания очереди можно использовать один из трех ее конструкторов.
//  Прежде всего можно создать пустую очередь:
Queue<string> people = new Queue<string>();
//  При создании пустой очереди можно указать емкость очереди:
Queue<string> people2 = new Queue<string>(16);

//  Также можно инициализировать очередь элементами из другой коллекции или массивом:
var employees = new List<string> { "Tom", "Sam", "Bob" };
Queue<string> people3 = new Queue<string>(employees);
//Для перебора очереди можно использовать стандартный цикл foreach.
//Для получения количества элементов в очереди в классе определено свойство Count.
#endregion

#region Методы Queue
//  У класса Queue<T> можно отметить следующие методы:
//  void Clear(): очищает очередь
//  bool Contains(T item): возвращает true, если элемент item имеется в очереди
//  T Dequeue(): извлекает и возвращает первый элемент очереди
//  void Enqueue(T item): добавляет элемент в конец очереди
//  T Peek(): просто возвращает первый элемент из начала очереди без его удаления

var _people = new Queue<string>();

// добавляем элементы
_people.Enqueue("Tom"); // people = { Tom }
_people.Enqueue("Sam"); // people = { Tom, Bob }
_people.Enqueue("Bob"); // people = { Tom, Bob, Sam }

// получаем элемент из начала очереди 
var fPerson = _people.Peek();

// удаляем элементы
var p1 = _people.Dequeue(); // people = { Bob, Sam }
Console.WriteLine(p1);      // Tom
//  Стоит отметить, что если с помощью методов Peek или Enqueue мы попытаемся получить первый элемент очереди,
//  которая пуста, то программа выдаст исключение. Соответственно перед получением элемента мы можем проверять
//  количество элементов в очереди:

//Либо можно использовать пару методов:
//bool TryDequeue(out T result): передает в переменную result первый элемент очереди с его удалением из очереди,
//возвращает true, если очередь не пуста и элемент успешно получен.
//bool TryPeek(out T result): передает в переменную result первый элемент очереди без его извлечения из очереди,
//возвращает true, если очередь не пуста и элемент успешно получен.

var _people1 = new Queue<string>();
_people1.Enqueue("Tom");

var success1 = _people1.TryDequeue(out var pers1);   // success1 = true
if (success1) Console.WriteLine(pers1);              // Tom

var success2 = people.TryPeek(out var pers2);  // success2 = false
if (success2) Console.WriteLine(pers2);
#endregion

//  Очереди - довольно часто встречаемая стуктура в реальной жизни. Например, очередь пациентов на прием к врачу.
//  Реализуем данную ситуацию:

var patients = new Queue<Person>();
patients.Enqueue(new Person("Kara"));
patients.Enqueue(new Person("German"));
patients.Enqueue(new Person("Elis"));

var doctor = new Doctor();
doctor.TakePatients(patients);

class Person
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;    
    }
}
class Doctor
{
    public void TakePatients(Queue<Person> que)
    {
        while (que.Count > 0 )
        {
            var patient = que.Dequeue();
            Console.WriteLine($"Осмотр пациента {patient.Name}");
        }
        Console.WriteLine("Осмотр завершён");
    }
}
//  Здесь класс врача - класс Doctor в методе TakePatients принимает очередь пациентов в виде объектов Person.
//  И пока в очереди есть объекты извлекает по одному объекту. Консольный вывод:


