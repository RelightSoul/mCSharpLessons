//  Итераторы и оператор yield

//  Итератор по сути представляет блок кода, который использует оператор yield для перебора набора значений.
//  Данный блок кода может представлять тело метода, оператора или блок get в свойствах.
//  Итератор использует две специальных инструкции:

//  yield return: определяет возвращаемый элемент

//  yield break: указывает, что последовательность больше не имеет элементов

Numbers numbers = new Numbers();
foreach (int n in numbers)
{
Console.WriteLine(n);
}
Console.WriteLine();
//class Numbers
//{
//    public IEnumerator<int> GetEnumerator()
//    {
//        for (int i = 0; i < 6; i++)
//        {
//            yield return i * i;
//        }
//    }
//}

//  В классе Numbers метод GetEnumerator() фактически представляет итератор. С помощью оператора
//  yield return возвращается некоторое значение (в данном случае квадрат числа).

//  В программе с помощью цикла foreach мы можем перебрать объект Numbers как обычную коллекцию.
//  При получении каждого элемента в цикле foreach будет срабатывать оператор yield return,
//  который будет возвращать один элемент и запоминать текущую позицию.

//  Благодаря итераторам мы можем пойти дальше и легко реализовать перебор числа в цикле foreach:

//  foreach (var n in 5) Console.WriteLine(n);
//  foreach (var n in -5) Console.WriteLine(n);

//  static class Int32Extension
//  {
//       public static IEnumerator<int> GetEnumerator(this int number)
//       {
//           int k = (number > 0) ? number : 0;
//           for (int i = number - k; i <= k; i++) yield return i;
//       }
//  }

//  Другой пример: пусть у нас есть коллекция Company, которая представляет компанию и которая хранит в массиве
//  personnel штат сотрудников - объектов Person. Используем оператор yield для перебора этой коллекции:
var people = new Person[]
{
    new Person("Tom"),
    new Person("Bob"),
    new Person("Sam")
};
// -------- IEnumerator -----------
var microsoft = new Company(people);
foreach (Person p in microsoft)
{
    Console.WriteLine(p.Name);
}
// -------- IEnumerable -----------
Console.WriteLine();
var google = new Company2(people);
foreach (Person person in google.GetPersonnel(5))
{
    Console.WriteLine(person.Name);
}
//  Вызов microsoft.GetPersonnel(3) будет возвращать набор из не более чем 5 объектов Person.
//  Но так как у нас всего три таких объекта, то в методе GetPersonnel после трех операций
//  сработает оператор yield break.

class Person
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;    
    }
}
class Company
{
    Person[] personnel;
    public Company(Person[] personnel)
    {
        this.personnel = personnel;
    }
    public int Lenght => personnel.Length;
    public IEnumerator<Person> GetEnumerator()
    {
        for (int i = 0; i < personnel.Length; i++)
        {
            yield return personnel[i];
        }
    }
}
//  Метод GetEnumerator() представляет итератор. И когда мы будем осуществлять перебор в объекте Company
//  в цикле foreach, то будет идти обращение к вызову yield return personnel[i];. При обращении к оператору
//  yield return будет сохраняться текущее местоположение. И когда метод foreach перейдет к следующей
//  итерации для получения нового объекта, итератор начнет выполнения с этого местоположения.

#region Именованный итератор
//  Выше для создания итератора мы использовали метод GetEnumerator. Но оператор yield можно использовать
//  внутри любого метода, только такой метод должен возвращать объект интерфейса IEnumerable. Подобные
//  методы еще называют именованными итераторами.
class Company2
{
    Person[] personnel;
    public Company2(Person[] personnel) => this.personnel = personnel;
    public int Length => personnel.Length;
    public IEnumerable<Person> GetPersonnel(int max)
    {
        for (int i = 0; i < max; i++)
        {
            if (i == personnel.Length)
            {
                yield break;
            }
            else
            {
                yield return personnel[i];
            }
        }
    }
}
//  Определенный здесь итератор - метод IEnumerable GetPersonnel(int max) в качестве параметра принимает
//  количество выводимых объектов. В процессе работы программы может сложиться, что его значение будет
//  больше, чем длина массива personnel. И чтобы не произошло ошибки, используется оператор yield break.
//  Этот оператор прерывает выполнение итератора.
#endregion

#region Конец кода
class Numbers
{
    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return i * i;
        }
    }
}
#endregion