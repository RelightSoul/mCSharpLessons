//  Ковариантность и контравариантность обобщенных интерфейсов

//  Понятия ковариантности и контравариантности связаны с возможностью использовать в приложении вместо некоторого
//  типа другой тип, который находится ниже или выше в иерархии наследования.

//Имеется три возможных варианта поведения:

//Ковариантность: позволяет использовать более конкретный тип, чем заданный изначально

//Контравариантность: позволяет использовать более универсальный тип, чем заданный изначально

//Инвариантность: позволяет использовать только заданный тип

//  C# позволяет создавать ковариантные и контравариантные обобщенные интерфейсы. Эта функциональность повышает
//  гибкость при использовании обобщенных интерфейсов в программе. По умолчанию все обобщенные интерфейсы являются
//  инвариантными.

//  Для рассмотрения ковариантных и контравариантных интерфейсов возьмем следующие классы:

class Message
{
    public string Text { get; set; }
    public Message(string text) => Text = text;
}
class EmailMessage : Message
{
    public EmailMessage(string text) : base(text) { }
}

//  Здесь определен класс сообщения Message, который получает через конструктор текст и сохраняет его в свойство Text.
//  А класс EmailMessage представляет условное email-сообщение и просто вызывает конструктор базового класса, передавая
//  ему текст сообщения.

#region Ковариантные интерфейсы
//  Обобщенные интерфейсы могут быть ковариантными, если к универсальному параметру применяется ключевое слово out. Такой
//  параметр должен представлять тип объекта, который возвращается из метода. Например:
interface IMessenger<out T>
{
    T WriteMessage(string text);
}
class EmailMessenger : IMessenger<EmailMessage>
{
    public EmailMessage WriteMessage(string text)
    {
        return new EmailMessage($"Email: {text}");
    }
}
//  Здесь обобщенный интерфейс IMessenger представляет интерфейс мессенджера и определяет метод WriteMessage() для создания
//  сообщения. При этом на момент определения интерфейса мы не знаем, объект какого типа будет возвращаться в этом методе.
//  Ключевое слово out в определении интерфейса указывает, что данный интерфейс будет ковариантным.

//  Класс EmailMessenger, который представляет условную программу для отправки email-сообщений, реализует этот интерфейс
//  и возвращает из метода WriteMessage() объект EmailMessage.

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("------ Ковариантные интерфейсы ------");
        IMessenger<Message> outlook = new EmailMessenger();
        Message message = outlook.WriteMessage("Hello World");
        Console.WriteLine(message.Text);     // Email: Hello World

        IMessenger<EmailMessage> emailClient = new EmailMessenger();
        IMessenger<Message> messenger = emailClient;
        Message emailMessage = messenger.WriteMessage("Hi!");
        Console.WriteLine(emailMessage.Text);  // Email: Hi!
        //  То есть мы можем присвоить более общему типу IMessenger<Message> объект более конкретного типа EmailMessenger
        //  или IMessenger<EmailMessage>.

        //  При создании ковариантного интерфейса надо учитывать, что универсальный параметр может использоваться только в
        //  качестве типа значения, возвращаемого методами интерфейса. Но не может использоваться в качестве типа аргументов
        //  метода или ограничения методов интерфейса.

        Console.WriteLine("------ Контравариантные интерфейсы ------");
        IMessenger2<EmailMessage> outlook2 = new SimpleMessenger();
        outlook2.SendMessage(new EmailMessage("Hi"));

        IMessenger2<Message> telegram2 = new SimpleMessenger();
        IMessenger2<EmailMessage> emailClient2 = telegram2;
        emailClient2.SendMessage(new EmailMessage("Hello"));
        //  При создании контрвариантного интерфейса надо учитывать, что универсальный параметр контрвариантного типа может
        //  применяться только к аргументам метода, но не может применяться к возвращаемому результату метода.

        Console.WriteLine("------ Совмещение ковариантности и контравариантности ------");
        IMessenger3<EmailMessage, Message> messenger3 = new SimpleMessenger3();
        Message message3 = messenger3.WriteMessage("Hello World");
        Console.WriteLine(message3.Text);
        messenger3.SendMessage(new EmailMessage("Test"));

        IMessenger3<EmailMessage, EmailMessage> outlook3 = new SimpleMessenger3();
        EmailMessage emailMessage3 = outlook3.WriteMessage("Message from Outlook");
        outlook3.SendMessage(emailMessage3);

        IMessenger3<Message, Message> telegram3 = new SimpleMessenger3();
        Message simpleMessage3 = telegram3.WriteMessage("Message from Telegram");
        telegram3.SendMessage(simpleMessage3);
    }
}
#endregion

#region Контравариантные интерфейсы
//  Для создания контравариантного интерфейса надо использовать ключевое слово in. Например, возьмем те же классы Message
//  и EmailMessage и определим следующие типы:
interface IMessenger2<in T>
{
    void SendMessage(T message);
}
class SimpleMessenger : IMessenger2<Message>
{
    public void SendMessage(Message message)
    {
        Console.WriteLine($"Отправляется сообщение: {message.Text}");
    }
}
//  Здесь опять же интерфейс IMessenger представляет интерфейс мессенджера и определяет метод SendMessage() для отправки
//  условного сообщения. Ключевое слово in в определении интерфейса указывает, что этот интерфейс - контравариантный.

//  Класс SimpleMessenger представляет условную программу отправки сообщений и реализует этот интерфейс. Причем в качестве
//  типа используемого этот класс использует тип Message. То есть SimpleMessenger фактически представляет тип
//  IMessenger<Message>. Применим эти типы в программе.
#endregion

#region Совмещение ковариантности и контравариантности
//  Также мы можем совместить ковариантность и контравариантность в одном интерфейсе. Например:
interface IMessenger3<in T, out K>
{
    void SendMessage(T message);
    K WriteMessage(string text);
}
class SimpleMessenger3 : IMessenger3<Message, EmailMessage>
{
    public void SendMessage(Message message)
    {
        Console.WriteLine($"Отправляется сообщение: {message.Text}");
    }
    public EmailMessage WriteMessage(string text)
    {
        return new EmailMessage($"Email: {text}");
    }
}
//  Фактически здесь объединены два предыдущих примера. Благодаря ковариантности/контравариантности объект класса
//  SimpleMessenger может представлять типы IMessenger<EmailMessage, Message>, IMessenger<Message, EmailMessage>,
//  IMessenger<Message, Message> и IMessenger<EmailMessage, EmailMessage>.
#endregion