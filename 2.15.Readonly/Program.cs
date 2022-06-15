// See Поля для чтения представляют такие поля класса или структуры, значение которых нельзя изменить.
// Таким полям можно присвоить значение либо при непосредственно при их объявлении, либо в конструкторе.
// В других местах программы присваивать значение таким полям нельзя, можно только считывать их значение.
Person tom = new Person("Tom");
Console.WriteLine(tom.name);

class Person
{
    public readonly string name = "Undefined";   // можно инициализировать по умолчанию
    public Person(string name)          // присвоить в конструкторе
    {
        this.name = name;               // в отличие от констант поле для чтения можно определить в конструкторе
    }
    //  public void ChangeName(string otherName)
    //  {
    //  this.name = otherName;                          // так нельзя 
    //  }
}

#region Структуры для чтения
//  Кроме полей для чтения в C# можно определять структуры для чтения. Для этого они предваряются модификатором readonly
readonly struct PersonStr
{
    public readonly string name;
    public int Age { get; }    // свойство только для чтения
    public PersonStr(string name, int age)
    {
        this.name = name;
        Age = age;
    }
}
//  Особенностью таких структур является то, что все их поля должны быть также полями для чтения:
#endregion
