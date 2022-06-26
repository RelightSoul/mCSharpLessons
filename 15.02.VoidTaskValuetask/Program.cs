//  Возвращение результата из асинхронного метода

//  В качестве возвращаемого типа в асинхронном методе должны использоваться типы void, Task, Task<T>
//  или ValueTask<T>

#region MyRegion
//  При использовании ключевого слова void асинхронный метод ничего не возвращает:
PrintAsync("Hiho");
PrintAsync("Good evening");

await Task.Delay(3000);   // ждем завершения задач

// определение асинхронного метода
async void PrintAsync(string message)
{
    Task.Delay(1000);   // имитация продолжительной работы
    Console.WriteLine(message);
}
//  Однако асинхронных void-методов следует избегать и следует использовать только там, где эти подобные
//  методы представляют единственный возможный способ определения асинхронного метода. Прежде всего, мы
//  не можем применить к подобным методам оператор await. Также потому что исключения в таких методах
//  сложно обрабатывать, так как они не могут быть перехвачены вне метода. Кроме того, подобные void-методы
//  сложно тестировать.

//  Тем не менее есть ситуации, где без подобных методов не обойтись - например, при обработке событий:
Account account = new Account();
account.Added += PrintAsync2;

account.Put(500);
await Task.Delay(2000); // ждем завершения

// определение асинхронного метода
async void PrintAsync2(object? obj, string message)
{
    await Task.Delay(1000);   // имитация продолжительной работы
    Console.WriteLine(message);
}
#endregion

#region Task
//  Возвращение объекта типа Task:
await PrintAsync3("(-_-)");

async Task PrintAsync3(string message)
{
    await Task.Delay(1000);
    Console.WriteLine(message);
}
//  Здесь формально метод PrintAsync не использует оператор return для возвращения результата. Однако если
//  в асинхронном методе выполняется в выражении await асинхронная операция, то мы можем возвращать из
//  метода объект Task.

//  Для ожидания завершения асинхронной задачи можно применить оператор await. Причем его необязательно
//  использовать непосредственно при вызове задачи. Его можно применить лишь там, где нам нужно гарантировано
//  получить результат задачи или удостовериться, что задача завершена.

var task = PrintAsync4("lalala");     // задача начинает выполняться
Console.WriteLine("Main works");

await task;    // ожидаем завершения задачи, получит результат

async Task PrintAsync4(string mes)
{
    await Task.Delay(0);
    Console.WriteLine(mes);
}
#endregion

#region Task<T>
//  Метод может возвращать некоторое значение. Тогда возвращаемое значение оборачивается в объект Task,
//  а возвращаемым типом является Task<T>:
int n1 = await SquareAsync(5);
int n2 = await SquareAsync(6);
Console.WriteLine($"n1={n1}  n2={n2}"); // n1=25  n2=36

async Task<int> SquareAsync(int n)
{
    await Task.Delay(0);
    return n * n;
}
//  В данном случае метод Square возвращает значение типа int - квадрат числа. Поэтому возвращаемым
//  типом в данном случае является типа Task<int>.

//  Подобным образом можно получать данные других типов:
Person person = await GetPersonAsync("Tom");
Console.WriteLine(person.Name); // Tom
// определение асинхронного метода
async Task<Person> GetPersonAsync(string name)
{
    await Task.Delay(0);
    return new Person(name);
}
//  Опять же получение непосредственных результатов асинхронной задачи можно отложить до того момента,
//  когда они непосредственно нужны:
var square5 = SquareAsync2(5);
var square6 = SquareAsync2(6);

int n11 = await square5;
int n22 = await square6;
Console.WriteLine(n11 +" "+ n22);

async Task<int> SquareAsync2(int n)
{
    await Task.Delay(0);
    int res = n * n;
    Console.WriteLine($"Result = {res}");
    return res;
}
#endregion

#region ValueTask<T>
//  Использование типа ValueTask<T> во многом аналогично применению Task<T> за исключением некоторых
//  различий в работе с памятью, поскольку ValueTask - структура, которая содержит большее количество
//  полей. Поэтому применение ValueTask вместо Task приводит к копированию большего количества данных
//  и соответственно создает некоторые дополнительные издержки.

//  Преимуществом ValueTask перед Task является то, что данный тип позволяет избежать дополнительных
//  выделений памяти в хипе. Например, иногда требуется синхронно возвратить некоторое значение. Так,
//  возьмем следующий пример:
var result = await AddAsync(4, 5);
Console.WriteLine(result);

Task<int> AddAsync(int a, int b)
{
    return Task.FromResult(a + b);
}
//  Здесь метод AddAsync синхронно возвращает некоторое значение - в данном случае сумму двух чисел.
//  С помощью статического метода Task.FromResult можно синхронно возвратить некоторое значение. Однако
//  использование типа Task приведет к выделению дополнительной задачи с сопутствующими выделениями памяти
//  в хипе. ValueTask решает эту задачу:
var result2 = await AddAsync2(4, 5);
Console.WriteLine(result);

ValueTask<int> AddAsync2(int a, int b)
{
    return new ValueTask<int>(a + b);
}
//  В данном случае дополнительный объект Task не будет создаваться и соответственно дополнительная память
//  не будет выделяться. Поэтому ValueTask обычно применяется, когда результат асинхронной операции уже имеется.

//  При необходимости также можно преобразовать ValueTask в объект Task с помощью метода AsTask():
var getMessage = GetMessageAsync();
string message = await getMessage.AsTask();
Console.WriteLine(message); // Hello

async ValueTask<string> GetMessageAsync()
{
    await Task.Delay(0);
    return "Hello";
}
#endregion













#region Конец кода
record class Person(string Name);
class Account
{
    int sum = 0;
    public event EventHandler<string>? Added;
    public void Put(int sum)
    {
        this.sum += sum;
        Added?.Invoke(this, $"На счёт поступило {sum} $");
    }
}
//  В данном случае событие Added в классе Account представляет делегат EventHandler, который имеет
//  тип void. Соответственно под это событие можно определить только метод-обработчик с типом void.
#endregion
