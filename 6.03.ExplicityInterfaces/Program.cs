// Кроме неявного применения интерфейсов, которое было рассмотрено в прошлой статье, сушествует также явная реализация
// интерфейса. При явной реализации указывается название метода или свойства вместе с названием интерфейса, при этом мы
// не можем использовать модификатор public, то есть методы являются закрытыми:

interface IAction
{
    void Move();
}
class BaseAction : IAction
{
    void IAction.Move() => Console.WriteLine("Move in Base Class");
}

class Program
{
    static void Main(string[] args)
    {
        // -----  Явная реализация интерфейсов -----
        BaseAction baseAction1 = new BaseAction();
        //  baseAction1.Move();   // ! Ошибка - в BaseAction нет метода Move
        // необходимо приведение к типу IAction
        // небезопасное приведение
        ((IAction)baseAction1).Move();
        // безопасное приведение 
        if (baseAction1 is IAction act) act.Move();
        // или так
        IAction baseAction2 = new BaseAction();
        baseAction2.Move();

        Person person1 = new Person();

        if (person1 is ISchool persS) persS.Study();        
        if (person1 is IUniversity persU) persU.Study();

        HeroAction3 action3 = new HeroAction3();
        action3.Move();    // Делаю движения как базовый чел
        if (action3 is IAction3 action4) action4.Move();   // Двигаюсь, как Герой

        IAction3 action5 = new HeroAction3();
        action5.Move();   // Двигаюсь, как Герой
        Console.WriteLine();

        // -----  Модификаторы доступа -----
        IMovable33 tom = new Person33("Tom");
        tom.MoveEvent += () => Console.WriteLine($"{tom.Name} is moving");
        tom.Move();
        tom.Move();
        //В данном случае опять же надо учитывать, что напрямую мы можем обратиться к подобным методам, свойствам и
        //событиям через переменную интерфейса, но не переменную класса.
    }
}

//  В какой ситуации может действительно понадобиться явная реализация интерфейса? Например, когда класс применяет
//  несколько интерфейсов, но они имеют один и тот же метод с одним и тем же возвращаемым результатом и одним и тем
//  же набором параметров:

interface ISchool
{
    void Study();
}
interface IUniversity
{
    void Study();
}
class Person : ISchool, IUniversity
{
    void IUniversity.Study()
    {
        Console.WriteLine("Учится в универе");
    }

    void ISchool.Study()
    {
        Console.WriteLine("Учится в школе");
    }
}
// Класс Person определяет один метод Study(), создавая одну общую реализацию для обоих примененных интерфейсов.
// И вне зависимости от того, будем ли мы рассматривать объект Person как объект типа ISchool или IUniversity,
// результат метода будет один и тот же.

//  Чтобы разграничить реализуемые интерфейсы, надо явным образом применить интерфейс.

//  Другая ситуация, когда в базовом классе уже реализован интерфейс, но необходимо в производном классе по-своему
//  реализовать интерфейс:

interface IAction3
{
    void Move();
}
class BaseAction3 : IAction3
{
    public void Move()
    {
        Console.WriteLine("Делаю движения как базовый чел");
    }
}
class HeroAction3 : BaseAction3, IAction3
{
    void IAction3.Move()
    {
        Console.WriteLine("Двигаюсь, как Герой");
    }
}

#region Модификаторы доступа
//  Члены интерфейса могут иметь разные модификаторы доступа. Если модификатор доступа не public, а какой-то другой,
//  то для реализации метода, свойства или события интерфейса в классах и структурах также необходимо использовать явную
//  реализацию интерфейса.
interface IMovable33
{
    protected internal void Move();
    protected internal string Name { get; }
    delegate void MoveHandler();
    protected internal event MoveHandler MoveEvent;
}
class Person33 : IMovable33
{
    string name;
    // явная реализация свойства - в виде автосвойства
    string IMovable33.Name { get => name; }
    public Person33(string name)
    {
        this.name = name;   
    }

    // явная реализация события - дополнительно создается переменная
    IMovable33.MoveHandler? moveEvent;    // переменная
    event IMovable33.MoveHandler IMovable33.MoveEvent
    {
        add
        {
            moveEvent += value;
        }
        remove
        {
            moveEvent -= value;
        }
    }

    void IMovable33.Move()
    {
        Console.WriteLine($"{name} walking");
        moveEvent?.Invoke();
    }
}
#endregion
