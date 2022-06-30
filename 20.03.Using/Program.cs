//  Конструкция using
//  В прошлой теме, где рассматривалась реализация метода Dispose, говорилось, что для его вызова можно
//  использовать следующую конструкцию try..catch:
Test();

void Test()
{
    Person? tom = null;
    try
    {
        tom = new Person("Tom");
    }
    catch (Exception)
    {
        tom?.Dispose();
    }
}

//  Однако синтаксис C# также предлагает синонимичную конструкцию для автоматического вызова метод Dispose
//  - конструкцию using:
using (Person tom = new Person("Tom"))
{
}
//  Конструкция using оформляет блок кода и создает объект некоторого типа, который реализует интерфейс
//  IDisposable, в частности, его метод Dispose. При завершении блока кода у объекта вызывается метод Dispose.

//  Важно, что данная конструкция применяется только для типов, которые реализуют интерфейс IDisposable.

//  Ее использование:
Test2();

void Test2()
{
    using (Person tom = new Person("Tom"))
    {
        // переменная tom доступна только в блоке using
        // некоторые действия с объектом Person
        Console.WriteLine($"Name = {tom.Name}");
    }
    Console.WriteLine("Конец метода Test2");
}
//  Здесь мы видим, что по завершении блока using у объекта Person вызывается метод Dispose. Вне блока кода
//  using объект tom не существует.

//  Начиная с версии C# 8.0 мы можем задать в качестве области действия всю окружающую область видимости,
//  например, метод:
Test3();
void Test3()
{
    using Person tom = new Person("Tom");

    // переменная tom доступна только в блоке using
    // некоторые действия с объектом Person
    Console.WriteLine($"Name: {tom.Name}");
    Console.WriteLine("Конец метода Test3");
}
//  В данном случае using сообщает компилятору, что объявляемая переменная должна быть удалена в конце области
//  видимости - то есть в конце метода Test

#region Освобождение множества ресурсов
//  Для освобождения множества ресурсов мы можем применять вложенные конструкции using. Например:
Test4();
void Test4()
{
    using (Person tom = new Person("Tom"))
    {
        using (Person bob = new Person("Bob"))
        {
            Console.WriteLine($"Person1: {tom.Name}    Person2: {bob.Name}");
        }// вызов метода Dispose для объекта bob
    } // вызов метода Dispose для объекта tom
    Console.WriteLine("Конец метода Test4");
}
//  В данном случае обе конструкции using создают объекты одного и того же типа, но это могут быть и разные
//  типы данных, главное, чтобы они реализовали интерфейс IDisposable.

//  Мы можем сократить это определение:
void Test5()
{
    using (Person tom = new Person("Tom"))
    using (Person bob = new Person("Bob"))
    {
        Console.WriteLine($"Person1: {tom.Name}    Person2: {bob.Name}");
    }// вызов метода Dispose для объектов bob и tom
    Console.WriteLine("Конец метода Test5");
}

//  И, как уже было выше сказано, в C# мы можем задать в качестве области действия для объектов, создаваемых
//  в конструкции using, весь метод:
void Test6()
{
    using Person tom = new Person("Tom");
    using Person bob = new Person("Bob");
    Console.WriteLine($"Person1: {tom.Name}    Person2: {bob.Name}");
    Console.WriteLine("Конец метода Test");
}   // вызов метода Dispose для объектов bob и tom
#endregion










class Person : IDisposable
{
    public string Name { get; set; }
    public Person(string name)
    {
        Name = name;
    }

    public void Dispose()
    {
        Console.WriteLine($"{Name} has beed disposed");
    }
}