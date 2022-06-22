// Паттерны кортежей позволяют сравнивать значения кортежей. Например, передадим конструкци switch
// кортеж с названием языка и времени дня и в зависимости от переданных данных возвратим определенное сообщение:

string GetWelcome(string lang, string daytime) => (lang, daytime) switch
{
    ("english","morning") => "Good morning",
    ("english","evening") => "Good evening",
    ("german","morning") => "Guten Morgen",
    ("gernam","evening") => "Guten Aben",
    _ => "Здрасьть"
};
//  Здесь в метод передаются два значения, из которых создается кортеж (можно и сразу передать в метод кортеж).
//  Далее в конструкции switch с помощью круглых скобок определяются значения, которым должны соответствовать
//  элементы кортежа. Например, выражение ("english", "morning") => "Good morning" будет выполняться, если
//  одновременно lang="english" и datetime="morning".

string message = GetWelcome("english", "evening");
Console.WriteLine(message);  // Good evening

message = GetWelcome("french", "morning");
Console.WriteLine(message);  // Здрасьть

//  Нам не обязательно сравнивать все значения кортежа, мы можем использовать только некоторые элементы кортежа.
//  В случае, если мы не хотим использовать элемент кортежа, то вместо него ставим прочерк:
string GetWelcome2(string lang, string daytime, string status) => (lang, daytime,status) switch
{
    ("english", "morning",_) => "Good morning",
    ("english", "evening",_) => "Good evening",
    ("german", "morning",_) => "Guten Morgen",
    ("gernam", "evening",_) => "Guten Aben",
    (_,_,"admin") => "Hello, admin",
    _ => "Здрасьть"
};
//  Теперь кортеж состоит из трех элементов. Но первые четыре выражения не используют последний элемент
//  кортежа, допустим, он не важен, поэтому вместо него ставится прочерк ("english", "morning", _).

//  А в предпоследнем примере, наоборот, не важны первые два элемента, а важен третий элемент:
//  (_, _, "admin") =>.

string message2 = GetWelcome2("english", "evening", "user");
Console.WriteLine(message2);  // Good evening

message2 = GetWelcome2("french", "morning", "admin");
Console.WriteLine(message2);  // Hello, Admin