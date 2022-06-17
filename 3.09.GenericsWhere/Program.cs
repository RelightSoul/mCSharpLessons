// Ограничения обобщений
// Ограничения путём ключевого слова Where нужно для повышения производительности и избежания ошибок
// преобразования и типизации.

#region Ограничения методов
//  Ограничения методов указываются после списка параметров после оператора where:

//          имя_метода<T>(параметры) where T: тип_ограничения

//  После оператора where указывается универсальный параметр, для которого применяется ограничение.
//  И через двоеточие указывается тип ограничения - обычно в качестве ограничения выступает конкретный тип.

using System.Collections;

SendMessage(new Message("Good morning"));
SendMessage(new EmailMessage("Good Evening"));

Messenger<Message> telegram = new Messenger<Message>();
telegram.sendMessage(new Message("Телега жива"));

Messenger<EmailMessage> outlook = new Messenger<EmailMessage>();
outlook.sendMessage(new EmailMessage("Кто им вообще пользуется?"));

Messenger6<Message2, Person> newTelegram = new Messenger6<Message2,Person>();
Person tom = new Person("Tom");
Person sam = new Person("Sam");
Message2 lalala = new Message2("Hey, bro!");
newTelegram.SendMessage(tom,sam,lalala);

void SendMessage<T>(T message) where T: Message
{
    Console.WriteLine($"Отправить {message.Text}");
}

class Message
{
    public string Text { get; }
    public Message(string text)
    {
        Text = text;
    }
}
class EmailMessage : Message
{
    public EmailMessage(string text) : base(text) { }
}
//  Выражение where T: Message в определении метода SendMessage говорит, что через универсальный параметр
//  T будут передаваться объекты класса Message и производных классов. Благодаря этому компилятор будет знать,
//  что T будет иметь функционал класса Message, и соответственно мы сможем обратиться к методам и свойствам
//  класса Message внутри метода без проблем.
#endregion

#region Ограничения обобщений в типах
//  Подобным образом можно определять и ограничения обобщенных типов.

//              class имя_класса<T> where T: тип_ограничения

class Messenger<T> where T : Message
{
    public void sendMessage(T message)
    {
        Console.WriteLine($"Отпрпавляет сообщение {message.Text}");
    }
}
//  Здесь для класса Messenger опять же установлено ограничение where T : Message. То есть внутри класса
//  Messenger все объекты типа T можно использовать как объекты Message. И в данном случае в классе
//  Messenger в методе SendMessage опять эмулируется отправка сообшений.
#endregion

#region Типы ограничений и стандартные ограничения
//  В качестве ограничений мы можем использовать следующие типы:

//      Классы

//      Интерфейсы

//      class -универсальный параметр должен представлять класс

//      struct -универсальный параметр должен представлять структуру

//      new() - универсальный параметр должен представлять тип, который имеет общедоступный
//      (public) конструктор без параметров

class Messenger2<T> where T : struct { }   // только структуры или другие Значимые типы

class Messenger3<T> where T : class { }    // только ссылочные типы 

class Messenger4<T> where T : new() { }    // класс или структуру, которые имеют общедоступный конструктор без параметров

//  Если для универсального параметра задано несколько ограничений, то они должны идти в определенном порядке:

//      Название класса, class, struct. Причем мы можем одновременно определить только одно из этих ограничений

//      Название интерфейса

//      new()

class Messenger5<T> where T : Message, IEnumerable, new() { }
#endregion

#region Использование нескольких универсальных параметров
//  Если класс использует несколько универсальных параметров, то последовательно можно задать
//  ограничения к каждому из них:

class Messenger6 <T, P> 
    where T : Message2
    where P : Person 
{
    public void SendMessage(P sender, P reciever, T message)
    {
        Console.WriteLine($"Отправитель = {sender.Name}");
        Console.WriteLine($"Получатель = {reciever.Name}");
        Console.WriteLine($"Сообщение = {message.Text}");
    }
}
class Person 
{
    public string Name { get; }
    public Person(string name) => Name = name;
}
class Message2
{
    public string Text { get; }
    public Message2(string text) => Text = text;
}
#endregion