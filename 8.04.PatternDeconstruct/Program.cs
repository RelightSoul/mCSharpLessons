//  Позиционный паттерн

//  Позиционный паттерн применяется к типу, у которого определен метод деконструктора. Например,
//  определим следующий класс:

class MessageDetails
{
    public string Language { get; set; } = "";
    public string DateTime { get; set; } = "";
    public string Status { get; set; } = "";
    public void Deconstruct(out string lang, out string datetime, out string status)
    {
        lang = Language;
        datetime = DateTime;
        status = Status;
    }
}
//  Теперь используем позиционный паттерн и в зависимости от значений объекта MessageDetails
//  возвратим определенное сообщение:
class Program
{
    static void Main(string[] args)
    {
        string GetWelcome(MessageDetails details) => details switch
        {
            ("english", "morning", _) => "Good morning",
            ("english", "evening", _) => "Good evening",
            ("german", "morning", _) => "Guten Morgen",
            ("german", "evening", _) => "Guten Abend",
            (_, _, "admin") => "Hello, Admin",
            _ => "Здрасьть"
        };
        //  Фактически этот паттерн похож на пример с кортежами выше, только теперь вместо кортежа в
        //  конструкцию switch передается объект MessageDetails. Через метод деконструктора мы можем
        //  получить набор выходных параметров в виде кортежа и опять же по позиции сравнивать их с
        //  некоторыми значениями в конструкции switch.
        MessageDetails details1 = new MessageDetails { Language = "english", DateTime = "evening", Status = "user" };
        string message = GetWelcome(details1);
        Console.WriteLine(message);  // Good evening

        MessageDetails details2 = new MessageDetails { Language = "french", DateTime = "morning", Status = "admin" };
        message = GetWelcome(details2);
        Console.WriteLine(message);  // Hello, Admin

        //  Также мы можем взять значения объекта MessageDetails и использовать их при создании результата метода:
        string GetWelcome2(MessageDetails details) => details switch
        {
            ("english", "morning", _) => "Good morning",
            ("english", "evening", _) => "Good evening",
            ("german", "morning", _) => "Guten Morgen",
            ("german", "evening", _) => "Guten Abend",
            (_, _, "admin") => "Hello, Admin",
            (var lang, var dTime, var user) => $"Hello. {lang} not found, {dTime} unknown, {user} undefined",
            _ => "Здрасьть"
        };
        //  В предпоследней инструкции в конструкции switch получаем по позиции значения из MessageDetails в
        //  переменные lang, datetime и status и используем их для создания сообщения:
        MessageDetails lastDetails = new MessageDetails
        {
            Language = "chinese",
            DateTime = "night",
            Status = "moderator"
        };
        Console.WriteLine(GetWelcome2(lastDetails));
    }
}
