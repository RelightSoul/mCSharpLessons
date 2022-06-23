// Как мы увидели, основной для большинства коллекций является реализация интерфейсов IEnumerable и IEnumerator.
// Благодаря такой реализации мы можем перебирать объекты в цикле foreach:

//      foreach (var item in перечислимый_объект)
//      {

//      }

//  Перебираемая коллекция должна реализовать интерфейс IEnumerable.

//  Интерфейс IEnumerable имеет метод, возвращающий ссылку на другой интерфейс - перечислитель:

//      public interface IEnumerable
//      {
//          IEnumerator GetEnumerator();
//      }

//  А интерфейс IEnumerator определяет функционал для перебора внутренних объектов в контейнере:

//      public interface IEnumerator
//      { 
//          bool MoveNext(); // перемещение на одну позицию вперед в контейнере элементов
//          object Current { get; }  // текущий элемент в контейнере
//          void Reset(); // перемещение в начало контейнера
//      }

//Метод MoveNext() перемещает указатель на текущий элемент на следующую позицию в последовательности.
//Если последовательность еще не закончилась, то возвращает true. Если же последовательность закончилась,
//то возвращается false.

//Свойство Current возвращает объект в последовательности, на который указывает указатель.

//Метод Reset() сбрасывает указатель позиции в начальное положение.

//Каким именно образом будет осуществляться перемещение указателя и получение элементов зависит от
//реализации интерфейса. В различных реализациях логика может быть построена различным образом.

//Например, без использования цикла foreach перебирем массив с помощью интерфейса IEnumerator:
using System.Collections;

string[] people = { "Tom", "Sam", "Bob" };

IEnumerator peopleEnumerator = people.GetEnumerator();  // получаем IEnumerator
while (peopleEnumerator.MoveNext())   // пока не будет возвращено false
{
    string item = (string)peopleEnumerator.Current;  // получаем элемент на текущей позиции
    Console.WriteLine(item);
}
peopleEnumerator.Reset(); // сбрасываем указатель в начало массива

#region Реализация IEnumerable и IEnumerator
Week week = new Week();
foreach (string day in week)
{
    Console.WriteLine(day);
}

class Week : IEnumerable
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
                         "Friday", "Saturday", "Sunday" }; 
    public IEnumerator GetEnumerator()
    {
        return days.GetEnumerator();
    }
}
//  В данном случае класс Week, который представляет неделю и хранит все дни недели, реализует интерфейс
//  IEnumerable. Однако в данном случае мы поступили очень просто - вместо реализации IEnumerator мы просто
//  возвращаем в методе GetEnumerator объект IEnumerator для массива.

//  Благодаря этому мы можем перебрать все дни недели в цикле foreach.

//  В то же время стоит отметить, что для перебора коллекции через foreach в принципе необязательно
//  реализовать интерфейс IEnumerable. Достаточно в классе определить публичный метод GetEnumerator,
//  который бы возвращал объект IEnumerator.

class Week2
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
                        "Friday", "Saturday", "Sunday" };
    public IEnumerator GetEnumerator() => days.GetEnumerator();
}

//  Однако это было довольно просто - мы просто используем уже готовый перчислитель массива. Однако,
//  возможно, потребуется задать свою собственную логику перебора объектов. Для этого реализуем интерфейс
//  IEnumerator:
class WeekEnumerator : IEnumerator
{
    string[] days;
    int position = -1;
    public WeekEnumerator(string[] days)
    {
        this.days = days;
    }

    public object Current
    {
        get
        {
            if (position == -1 || position >= days.Length)
            {
                throw new ArgumentException();
            }
            return days[position];
        }
    }

    public bool MoveNext()
    {
        if (position < days.Length -1)
        {
            position++;
            return true;
        }
        else
            return false;
    }

    public void Reset()
    {
        position = -1;
    }
}
//  Здесь теперь класс Week использует не встроенный перечислитель, а WeekEnumerator, который реализует
//  IEnumerator.

//  Ключевой момент при реализации перечислителя - перемещения указателя на элемент. В классе WeekEnumerator
//  для хранения текущей позиции определена переменная position. Следует учитывать, что в самом начале
//  (в исходном состоянии) указатель должен указывать на позицию условно перед первым элементом. Когда
//  будет производиться цикл foreach, то данный цикл вначале вызывает метод MoveNext и фактически перемещает
//  указатель на одну позицию в перед и только затем обращается к свойству Current для получения элемента
//  в текущей позиции.
#endregion

#region Обобщенная версия IEnumerator
//  В примерах выше использовались необобщенные версии интерфейсов, однако мы также можем использовать их
//  обобщенные двойники: 
class WeekEnumerator2 : IEnumerator<string>
{
    string[] days;
    int position = -1;
    public WeekEnumerator2(string[] days) => this.days = days;
    public string Current
    {
        get
        {
            if (position == -1 || position >= days.Length)
                throw new ArgumentException();
            return days[position];
        }
    }
    object IEnumerator.Current => throw new NotImplementedException();
    public bool MoveNext()
    {
        if (position < days.Length - 1)
        {
            position++;
            return true;
        }
        else
            return false;
    }
    public void Reset() => position = -1;
    public void Dispose() { }
}
class Week3
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
"Friday", "Saturday", "Sunday" };
    public IEnumerator<string> GetEnumerator() => new WeekEnumerator2(days);
}
//  В данном случае реализуем интерфейс IEnumerator<string>, соответственно в свойстве Current нам надо
//  возвратить объект string. В этом случае при переборе в цикле foreach перебираемые объекты будут
//  автоматически представлять тип string
#endregion