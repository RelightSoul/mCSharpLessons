// Применение рефлексии и исследование типов

#region Получение всех компонентов типа
//  Метод GetMembers() возвращает все доступные компоненты типа в виде объекта MemberInfo. Этот объект позволяет
//  извлечь некоторую информацию о компоненте типа. В частности, некоторые его свойства:
//  DeclaringType: возвращает полное название типа.
//  MemberType: возвращает значение из перечисления MemberTypes:
//  MemberTypes.Constructor
//  MemberTypes.Method
//  MemberTypes.Field
//  MemberTypes.Event
//  MemberTypes.Property
//  MemberTypes.NestedType
//  Name: возвращает название компонента
using System.Reflection;

Type myType = typeof(Person);

foreach (MemberInfo member in myType.GetMembers())
{
    Console.WriteLine($"{member.DeclaringType} {member.MemberType} {member.Name}");
}
//  Обратите внимание, что в данном случае мы получаем только все публичные компоненты класса, и нам не
//  выводится информация о приватной переменной name.

//  Кроме того, для свойства выводятся методы доступа - геттер (здесь get_Age) и сеттер(здесь set_Age).

//  Третий момент, который надо отметить, что по умолчанию мы получаем весь функционал, в том числе
//  унаследованный от базовых классов (в данном случае функционал базового класса Object).
#endregion

#region BindingFlags
//  В примере выше использовалась простая форма метода GetMembers(), которая извлекает все общедоступные
//  публичные методы. Но мы можем использовать и другую форму метода: MembersInfo[] GetMembers(BindingFlags).
//  Перечисление BindingFlags может принимать различные значения:
//  DeclaredOnly: получает только методы непосредственно данного класса, унаследованные методы не извлекаются
//  Instance: получает только методы экземпляра
//  NonPublic: извлекает не публичные методы
//  Public: получает только публичные методы
//  Static: получает только статические методы

//  Объединяя данные значения с помощью побитовой операции ИЛИ можно комбинировать вывод. Например, получим
//  только компоненты непосредственно самого класса без унаследованных, как публичные, так и все остальные:
Type _myType = typeof(Person);
foreach (MemberInfo memberInfo in _myType.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance |
    BindingFlags.NonPublic | BindingFlags.Public))
{
    Console.WriteLine($"{memberInfo.DeclaringType} {memberInfo.MemberType} {memberInfo.Name}");
}
#endregion

#region Получение одного компонента по имени
//  Для получения одного компонента можно использовать метод GetMember(), в который передается имя компонента.
//  И опционально можно передать флаги BindingFlags.
Type myType2 = typeof(Person);
MemberInfo[] print = myType2.GetMember("Print", BindingFlags.Instance | BindingFlags.Public);
//  Стоит отметить, что при получении одного члена типа опять же возвращается массив MemberInfo[], поскольку в
//  классе может быть несколько элементов с одним именем, например, несколько перегруженных версий метода Print.
Console.WriteLine();
#endregion
public class Person
{
    string name;
    public int Age { get; set; }
    public Person(string name, int age)
    {
        this.name = name;
        this.Age = age;
    }
    public void Print() => Console.WriteLine($"Name: {name} Age: {Age}");
}