// Если класс применяет интерфейс, то этот класс должен реализовать все методы и свойства интерфейса, которые
// не имеют реализации по умолчанию. Однако также можно и не реализовать методы, сделав их абстрактными,
// переложив право их реализации на производные классы:
interface IMovable
{
    void Move();
}
abstract class Person : IMovable
{
    public abstract void Move();
}
class Driver : Person
{
    public override void Move()
    {
        Console.WriteLine("Водитель ведёт машину");
    }
}

//  При реализации интерфейса учитываются также методы и свойства, унаследованные от базового класса. Например:
interface IAction
{
    void Action();
}
class BaseAction : IAction
{
    public void Action()
    {
        Console.WriteLine("Происходит действие");
    }
}
class HeroAction : BaseAction, IAction { }
//  Здесь класс HeroAction реализует интерфейс IAction, однако для реализации метода Move из интерфейса применяется
//  метод Move, унаследованный от базового класса BaseAction. Таким образом, класс HeroAction может не реализовать
//  метод Move, так как этот метод уже определен в базовом классе BaseAction.

#region Изменение реализации интерфейсов в производных классах
//  Может сложиться ситуация, что базовый класс реализовал интерфейс, но в классе-наследнике необходимо изменить реализацию
//  этого интерфейса. Что в этом случае делать? В этом случае мы можем использовать либо переопределение, либо скрытие
//  метода или свойства интерфейса.

//  Первый вариант - переопределение виртуальных/абстрактных методов:
interface IAction2
{
    void Move();
}
class BaseAction2 : IAction2
{
    public virtual void Move() => Console.WriteLine("Move in BaseAction");
}
class HeroAction2 : BaseAction2
{
    public override void Move() => Console.WriteLine("Move in HeroAction");
}

//  Второй вариант - скрытие метода в производном классе:
interface IAction3
{
    void Move();
}
class BaseAction3 : IAction3
{
    public void Move() => Console.WriteLine("Move in BaseAction");

}
class HeroAction3 : BaseAction3
{
    public new void Move() => Console.WriteLine("Move in HeroAction");
}

//  Третий вариант - повторная реализация интерфейса в классе-наследнике:
interface IAction4
{
    void Move();
}
class BaseAction4 : IAction4
{
    public void Move() => Console.WriteLine("Move in BaseAction");
}
class HeroAction4 : BaseAction4, IAction4
{
    public new void Move() => Console.WriteLine("Move in HeroAction");
}

//  Четвертый вариант: явная реализация интерфейса:
interface IAction5
{
    void Move();
}
class BaseAction5 : IAction5
{
    public void Move() => Console.WriteLine("Move in BaseAction");
}
class HeroAction5 : BaseAction5, IAction5
{
    public new void Move() => Console.WriteLine("Move in HeroAction");
    // явная реализация интерфейса
    void IAction5.Move() => Console.WriteLine("Move in IAction");
}

class Program
{
    static void Main(string[] args)
    {
        // -----  Первый вариант -----
        BaseAction2 action1 = new HeroAction2();
        action1.Move();            // Move in HeroAction

        IAction2 action2 = new HeroAction2();
        action2.Move();             // Move in HeroAction

        // -----  Второй вариант -----
        BaseAction3 action13 = new HeroAction3();
        action13.Move();            // Move in BaseAction

        IAction3 action23 = new HeroAction3();
        action23.Move();           // Move in BaseAction

        // -----  Третий вариант -----
        BaseAction4 action14 = new HeroAction4();
        action14.Move();            // Move in BaseAction

        IAction4 action24 = new HeroAction4();
        action24.Move();             // Move in HeroAction

        HeroAction4 action34 = new HeroAction4();
        action34.Move();             // Move in HeroAction

        // -----  Четвертый вариант -----
        BaseAction5 action15 = new HeroAction5();
        action15.Move();            // Move in BaseAction

        IAction5 action25 = new HeroAction5();
        action25.Move();             // Move in IAction

        HeroAction5 action35 = new HeroAction5();
        action35.Move();             // Move in HeroAction
    }
}
#endregion