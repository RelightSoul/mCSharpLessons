// See Проверка на null, операторы ?. и ??

#region Проверка на null. Null guard
//  Если мы собираемся использовать переменную или параметр, которые допускают значение null,
//  то есть представляют nullable-тип (не важно значимый или ссылочный), то, чтобы избежать возникновения
//  NullReferenceException, мы можем проверить на null:
void PrintUpeer(string? text)
{
    if (text != null)
    {
        Console.WriteLine(text.ToUpper());
    }
}
//  проверка с помощью is
void PrintUpeer2(string? text)
{
    if (text is null)
        return;    
    Console.WriteLine(text.ToUpper());
}
//  проверка с помощью is not
void PrintUpeer3(string? text)
{
    if (text is not null)
    {
        Console.WriteLine(text.ToUpper());
    }
}
//  проверить на соответствие типу
void PrintUpeer4(string? text)
{
    if (text is string)
    {
        Console.WriteLine(text.ToUpper());
    }
}
#endregion

#region Оператор ??
//  Оператор ?? называется оператором null-объединения. Он применяется для установки значений
//  по умолчанию для типов, которые допускают значение null:
//  левый_операнд ?? правый_операнд

//  Оператор ?? возвращает левый операнд, если этот операнд не равен null. Иначе возвращается
//  правый операнд. При этом левый операнд должен принимать null. Посмотрим на примере:
string? text = null;
string name = text ?? "Bob";
Console.WriteLine(name);

int? id = 200;
int num = id ?? 0;
Console.WriteLine(num);

//  Также можно использовать производный оператора ??=
string? text2 = null;
text2 ??= "Bob";
Console.WriteLine(text2);

int? id2 = 200;
id2 ??= 0;
Console.WriteLine(id2);
#endregion

#region Оператор условного null - Сахар
//  в C# есть оператор условного null (Null-Conditional Operator) - оператор ?.

//          объект?.компонент

//  Если объект не равен null, то происходит обращение к компоненту объекта - полю,
//  свойству, методу. Если объект представляет значение null, обращение к компаненту
//  метода не происходит.

void PrintWebSite(Person? person)
{
    Console.WriteLine(person?.Company?.WebSite?.ToUpper());
}
class Person
{
    public Company? Company { get; set; }   // место работы
}
class Company
{
    public string? WebSite { get; set; }    // веб-сайт компании
}
#endregion
