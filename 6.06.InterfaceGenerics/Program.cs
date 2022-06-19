// Интерфейсы в обобщениях

#region Интерфейсы как ограничения обобщений
//  Интерфейсы могут выступать в качестве ограничений обобщений. При этом если в качестве ограничения можно указаь
//  только один класс, то интерфейсов можно указать несколько.

//  Допустим, у нас есть следующие интерфейсы и класс, который их реализует:

interface IMessage
{
    string Text { get; } // текст сообщения
}
interface IPrintable
{
    void Print();
}
class Message : IMessage, IPrintable
{
    public string Text { get; }
    public Message(string text) => Text = text;

    public void Print() => Console.WriteLine(Text);
}
//  Интерфейс IMessage представляет интерфейс сообщения и определяет свойство Text для хранения текста сообщения.
//  Интерфейс IPrintable определяет метод Print для условной печати сообщения. И непосредственно класс сообщения
//  - класс Message реализует эти интерфейсы.

//  Используем выше перечисленные интерфейсы в качестве ограничений обобщенного класса:
class Messenger<T> where T: IMessage, IPrintable
{
    public void Send(T message)
    {
        Console.WriteLine("Отправка сообщения: ");
        message.Print();
    }
}
//  В данном случае класс условного мессенджера использует параметр T - тип, который который реализует сразу два
//  интерфейса IMessage и IPrintable. Например, выше определен класс Message, который реализует оба интерфейса,
//  поэтому мы можем данным типом типизировать объекты Messenger:
class Program
{
    static void Main(string[] args)
    {
        // -----  Интерфейсы как ограничения обобщений -----

        // создаем мессенджер
        var telegtam = new Messenger<Message>();
        // создаем сообщение
        var message = new Message("Hello World");
        // отправляем сообщение
        telegtam.Send(message);

        // создаем мессенджер
        var telegtam2 = new Messenger<PrintableMessage>();
        // создаем сообщение
        var message2 = new PrintableMessage("Hello World");
        // отправляем сообщение
        telegtam2.Send(message2);

        // -----  Обобщенные интерфейсы -----
        IUser<int> user1 = new User<int>(55456);
        Console.WriteLine(user1.Id);

        IUser<string> user2 = new User<string>("1423");
        Console.WriteLine(user2.Id);

        IUser<int> user3 = new IntUser(543);
        Console.WriteLine(user3.Id);

        IntUser user4 = new IntUser(345);
        Console.WriteLine(user4.Id);
        
    }
}

//  Также параметр T может представлять интерфейс, который наследуется от обоих интерфейсов:
interface IPrintableMessage : IPrintable, IMessage { }
class PrintableMessage : IPrintableMessage
{
    public string Text { get; }
    public PrintableMessage(string text) => Text = text;
    public void Print() => Console.WriteLine(Text);
}
#endregion

#region Обобщенные интерфейсы
//  Как и классы, интерфейсы могут быть обобщенными:
interface IUser<T>
{
    T Id { get; }
}
class User<T> : IUser<T>
{
    public T Id { get; }
    public User(T id) => Id = id;
}
//  Интерфейс IUser типизирован параметром T, который при реализации интерфейса используется в классе User.
//  В частности, переменная _id определена как T, что позволяет нам использовать для id различные типы.

//  Также при реализации интерфейса мы можем явным образом указать, какой тип будет использоваться для параметра T:
class IntUser : IUser<int>
{
    public int Id { get; }
    public IntUser(int id) => Id = id;
}
#endregion
