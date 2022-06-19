// Интерфейсы, как и классы, могут наследоваться:
interface IAction
{
    void Move();
}
interface IRunAction : IAction
{
    void Run();
}
class BaseAction : IRunAction
{
    public void Move()
    {
        Console.WriteLine("Move");
    }
    public void Run()
    {
        Console.WriteLine("Run");
    }
}
//  При применении этого интерфейса класс BaseAction должен будет реализовать как методы и свойства интерфейса IRunAction,
//  так и методы и свойства базового интерфейса IAction, если эти методы и свойства не имеют реализации по умолчанию.

//  Однако в отличие от классов мы не можем применять к интерфейсам модификатор sealed, чтобы запретить наследование
//  интерфейсов.

//  Также мы не можем применять к интерфейсам модификатор abstract, поскольку интерфейс фактически итак, как правило,
//  предоставляет абстрактный функционал, который должен быть реализован в классе или структуре (за исключением методов
//  и свойств с реализацией по умолчанию).

//  Однако методы интерфейсов могут использовать ключевое слово new для скрытия методов из базового интерфейса:
class Program
{
    static void Main(string[] args)
    {
        IAction2 action1 = new RunAction();
        action1.Move(); // I am moving

        IRunAction2 action2 = new RunAction();
        action2.Move(); // I am running

        IAction2 action3 = new RunAction2();
        action3.Move(); // I am tired

        IRunAction2 action4 = new RunAction2();
        action4.Move(); // I am tired
    }
}
interface IAction2
{
    void Move() => Console.WriteLine("I'm walking");
}
interface IRunAction2 : IAction2
{
    // скрываем реализацию из IAction
    new void Move() => Console.WriteLine("I'm running");
}
class RunAction : IRunAction2 { }
//  Здесь метод Move из IRunAction скрывает метод Move из базового интерфейса IAction. Это имеет смысл, если в базовом
//  интерфейсе определена реализация по умолчанию, как в случае выше, которую нужно переопределить. И в случае выше,
//  если переменная представляет тип IRunAction, то для метода Move вызывается реализация этого интерфейса.

//  Но класс RunAction2 может переопределить метод Move сразу для обоих интерфейсов.

class RunAction2 : IRunAction2
{
    public void Move() => Console.WriteLine("I'm tired");
}

//  При наследовании интерфейсов следует учитывать, что, как и при наследовании классов, производный интерфейс должен
//  иметь тот же уровень доступа или более строгий, чем базовый интерфейс.