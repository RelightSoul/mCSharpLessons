//  Обработка ошибок в асинхронных методах

//  Обработка ошибок в асинхронных методах, использующих ключевые слова async и await, имеет свои особенности.

//  Для обработки ошибок выражение await помещается в блок try:
try
{
    await PrintAsync("Privet");
    await PrintAsync("Hi");
}
catch (Exception mes)
{
    Console.WriteLine(mes.Message);   
}

async Task PrintAsync(string mes)
{
    if (mes.Length < 3)
    {
        throw new ArgumentException("Слишком короткое сообщение");
    }
    await Task.Delay(100);
    Console.WriteLine(mes);
}
//  В данном случае асинхронный метод PrintAsync генерирует исключение ArgumentException, если методу
//  передается строка с длиной меньше 3 символов.

//  Для обработки исключения в методе Main выражение await помещено в блок try. В итоге при выполнении
//  вызова await PrintAsync("Hi") будет сгенерировано исключение, что привет к генерации исключения.
//  Однако программа не остановит аварийно свою работу, а обработает исключение и продолжит дальнейшие вычисления.

//  Следует учитывать, что если асинхронный метод имеет тип void, то в этом случае исключение во вне не передается,
//  соответственно мы не сможем обработать исключение при вызове метода:

//try
//{
//    PrintAsync("Hello METANIT.COM");
//    PrintAsync("Hi");       // здесь программа сгенерирует исключение и аварийно остановится
//    await Task.Delay(1000); // ждем завершения задач
//}
//catch (Exception ex)    // исключение НЕ будет обработано
//{
//    Console.WriteLine(ex.Message);
//}

//async void PrintAsync(string message)
//{
//    // если длина строки меньше 3 символов, генерируем исключение
//    if (message.Length < 3)
//        throw new ArgumentException($"Invalid string length: {message.Length}");
//    await Task.Delay(100);     // имитация продолжительной операции
//    Console.WriteLine(message);
//}

//  В данном случае, не смотря на то, что асинхронные методы вызываются в блоке try, исключение не
//  будет перехвачено и обработано. В этом один из минусов применения асинхронных void-методов. Правда,
//  в этом случае мы можем определить обработку исключения в самом асинхронном методе:
PrintAsync2("Ok, let's rock");
PrintAsync2("Ok");

await Task.Delay(100);  // ждем завершения задач

async void PrintAsync2(string str)
{
    try
    {
        if (str.Length < 3)
        {
            throw new ArgumentException("Слишком короткая строка");
        }
        await Task.Delay(100);
        Console.WriteLine(str);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

#region Исследование исключения
//  При возникновении ошибки у объекта Task, представляющего асинхронную задачу, в которой произошла
//  ошибка, свойство IsFaulted имеет значение true. Кроме того, свойство Exception объекта Task содержит
//  всю информацию об ошибке. Проинспектируем данное свойство:
var task = PrintAsync3("Hi");
try
{
    await task;
}
catch (Exception)
{
    Console.WriteLine(task.Exception?.InnerException?.Message);  // Invalid string length: 2
    Console.WriteLine($"IsFaulted: {task.IsFaulted}");    // IsFaulted: True 
    Console.WriteLine($"Status: {task.Status}");         // Status: Faulted
}

async Task PrintAsync3(string str)
{
    if (str.Length < 3)
    {
        throw new ArgumentException($"Invalid string length: {str.Length}");
    }
    await Task.Delay(100);
    Console.WriteLine(str);    
}
//  И если мы передадим в метод строку с длиной меньше 3 символов, то task.IsFaulted будет равно true.
#endregion

#region Обработка нескольких исключений. WhenAll
//  Если мы ожидаем выполнения сразу нескольких задач, например, с помощью Task.WhenAll, то мы можем
//  получить сразу несколько исключений одномоментно для каждой выполняемой задачи. В этом случае мы
//  можем получить все исключения из свойства Exception.InnerExceptions:
var task2 = PrintAsync4("Hi");
var task3 = PrintAsync4("Qq");
var taskWhen = Task.WhenAll(task2, task3);

try
{
    await taskWhen;
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
    Console.WriteLine($"IsFaulted: {taskWhen.IsFaulted}");
    if (taskWhen.Exception is not null)
    {
        foreach (var exception in taskWhen.Exception.InnerExceptions)
        {
            Console.WriteLine(exception.Message);
        }
    }
}

async Task PrintAsync4(string str)
{
    if (str.Length < 3)
    {
        throw new ArgumentException($"Invalid string length: {str.Length}");
    }
    await Task.Delay(100);
    Console.WriteLine(str);
}
//  Здесь в два вызова метода PrintAsync передаются заведомо некорректные значения. Таким образом,
//  при обоих вызовах будет сгенерирована ошибка.

//  Хотя блок catch через переменную Exception ex будет получать одно перехваченное исключение, но
//  с помощью коллекции Exception.InnerExceptions мы сможем получить информацию обо всех возникших исключениях.
#endregion
