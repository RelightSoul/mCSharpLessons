// Делегаты могут быть ковариантными и контравариантными. Ковариантность делегата предполагает,
// что возвращаемым типом может быть производный тип. Контрвариантность делегата предполагает,
// что типом параметра может быть более универсальный тип.

class Message
{
    public string Text { get; }
    public Message(string text) => Text = text;
    public virtual void Print() => Console.WriteLine($"Message: {Text}");
}
class EmailMessage : Message
{
    public EmailMessage(string text) : base(text) { }
    public override void Print() => Console.WriteLine($"Email: {Text}");
}
class SmsMessage : Message
{
    public SmsMessage(string text) : base(text) { }
    override public void Print() => Console.WriteLine($"Sms: {Text}");
}
//  В данном случае класс Message представляет некоторое сообщение и определяет свойство Text для хранения
//  текста сообщения, который устанавливается через конструктор. А в методе Print сообщение выводится на консоль.
//  Класс EmailMessage представляет email-сообщение, а SmsMessage - смс-сообщение, и оба класса является
//  производными от Message.

class Program
{
    static void Main(string[] args)
    {
        #region Ковариантность
        //Ковариантность позволяет передать делегату метод, возвращаемый тип которого является производный от возвращаемого
        //типа делегат. То есть если возвращаемый тип делегата Message, то у метод может иметь в качестве возвращаемого типа
        //класс EmailMessage:

        //  делегату с базовым типом передаем метод с производным типом
        MessageBuilder messageBuilder = WriteEmailMessage;  // ковариантность
        Message message = messageBuilder("Hello");
        message.Print();                                    // Email: Hello

        EmailMessage WriteEmailMessage(string text) => new EmailMessage(text);

        //  delegate Message MessageBuilder(string text);  определён в конце кода

        //  Здесь делегат MessageBuilder возвращает объект Message. Однако благодаря ковариантности
        //  данный делегат может указывать на метод, который возвращает объект производного типа, например,
        //  на метод WriteEmailMessage.
        #endregion

        #region Контрвариантность
        //  Контрвариантность позволяет присваить делегату метод, тип параметра которого является более универсальным
        //  по отношению к типу параметра делегата. Например, возьмем выше определенные классы Message и EmailMessage
        //  и используем их в следующем примере:

        //  делегату с производным типом передаем метод с базовым типом
        EmailReceiver emailBox = ReceiverMessage;  // контравариантность
        emailBox(new EmailMessage("Welcome"));     // Email: Welcome  

        void ReceiverMessage(Message message) => message.Print();

        //  delegate void EmailReceiver(EmailMessage message);   определён в конце кода

        //  Несмотря на то, что делегат в качестве параметра принимает объект EmailMessage, ему можно присвоить метод,
        //  у которого параметр представляет базовый тип Message.Может показаться на первый взгляд, что здесь есть
        //  некоторое противоречие, то есть использование более универсального тип вместо более производного. Однако в
        //  реальности в делегат при его вызове мы все равно можем передать только объекты типа EmailMessage, а любой
        //  объект типа EmailMessage является объектом типа Message, который используется в методе.
        #endregion

        #region Ковариантность и контравариантность в обобщенных делегатах
        //  Обобщенные делегаты также могут быть ковариантными и контравариантными, что дает нам больше гибкости в их использовании.

        //  Например, объявим и используем ковариантный обобщенный делегат
        MessageBuilder<Message> messBuilder = WriteEmailMessage2;       // ковариантность
        Message message2 = messBuilder("Hello");
        message2.Print();                                    // Email: Hello

        EmailMessage WriteEmailMessage2(string text) => new EmailMessage(text);

        //  delegate T MessageBuilder<out T>(string message);   определён в конце кода

        //Благодаря использованию out мы можем присвоить делегату типа MessageBuilder<Message>
        //делегат типа MessageBuilder<EmailMessage> или ссылку на метод, который возвращает объект EmailMessage.

        //  Рассмотрим контравариантный обобщенный делегат:
        MessageReceiver<EmailMessage> messageReceiver = ReceiveMessage2; // контравариантность
        messageReceiver(new EmailMessage("Hello World!"));

        void ReceiveMessage2(Message message) => message.Print();

        //  delegate void MessageReceiver<in T>(T message);     определён в конце кода
        //  Использование ключевого слова in позволяет присвоить делегату с производным
        //  типом(MessageReceiver<EmailMessage>) делегат с базовым типом(MessageReceiver<Message>)
        //  или метод, который принимает в качестве параметра объект базового типа.

        //  Причем делегат может одновеменно использовать оба оператора: in и out. Например:
        MessageConverter<SmsMessage, Message> mConvert = ConverterToMail;
        Message message66 = mConvert(new SmsMessage("Burn them ALL")); 
        message66.Print();

        EmailMessage ConverterToMail(Message text) => new EmailMessage(text.Text);

        //  delegate E MessageConverter<in M, out E>(M message);  определён в конце кода

        //  Здесь делегат MessageConverter представляет условное действие, которое конвертирует объект типа M в тип E.

        //  В программе определена переменная converter, которая представляет тип MessageConverter<SmsMessage,
        //  Message> -то есть конвертер из типа SmsMessage в любой тип Message, грубо говоря преобразует смс в
        //  любой другой тип сообщения.

        //  Этой переменной можно передать метод ConvertToEmail, который из сообщений любого типа создает объект Email
        //  - сообщения.Здесь применяется контравариантность: для параметра вместо производного типа SmsMessage
        //  применяется базовый тип Message. И также есть ковариантность: вместо возвращаемого типа Message используется
        //  производный тип EmailMessage.
        #endregion
    }
}
delegate E MessageConverter<in M, out E>(M message);
delegate void MessageReceiver<in T>(T message);
delegate T MessageBuilder<out T>(string message);
delegate void EmailReceiver(EmailMessage message);
delegate Message MessageBuilder(string text);
