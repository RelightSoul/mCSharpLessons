//  Отмена задач и параллельных операций. CancellationToken

//  Параллельное выполнение задач может занимать много времени. И иногда может возникнуть необходимость прервать
//  выполняемую задачу. Для этого платформа .NET предоставляет структуру CancellationToken из пространства имен
//  System.Threading.

//  Общий алгоритм отмены задачи обычно предусматривает следующий порядок действий:

//   ------------ 1 ------------
//  Создание объекта CancellationTokenSource, который управляет и посылает уведомление об отмене токену.

//   ------------ 2 ------------
//  С помощью свойства CancellationTokenSource.Token получаем собственно токен - объект структуры
//CancellationToken и передаем его в задачу, которая может быть отменена.
using System.Threading;
using System.Threading.Tasks;

CancellationTokenSource cancelTokenSourse = new CancellationTokenSource();
CancellationToken token = cancelTokenSourse.Token;
//  Для передачи токена в задачу можно применять один из конструкторов класса Task:
Task task = new Task(() => { },token);

//   ------------ 3 ------------
//  Определяем в задаче действия на случай ее отмены.

//   ------------ 4 ------------
//  Вызываем метод CancellationTokenSource.Cancel(), который устанавливает для свойства
//  CancellationToken.IsCancellationRequested значение true. Стоит понимать, что сам по себе метод
//  CancellationTokenSource.Cancel() не отменяет задачу, он лишь посылает уведомление об отмене через
//  установку свойства CancellationToken.IsCancellationRequested. Каким образом будет происходить выход
//  из задачи, это решает сам разработчик.

//   ------------ 5 ------------
//  Класс CancellationTokenSource реализует интерфейс IDisposable. И когда работа с объектом
//  CancellationTokenSource завершена, у него следует вызвать метод Dispose для освобождения всех
//  связанных с ним используемых ресурсов. (Вместо явного вызова метода Dispose можно использовать
//  конструкцию using).

//  Теперь касательно третьего пункта - определения действий отмены задачи. Как именно завершить задачу?
//  Конкретные действия лежат целиком на разработчике, тем не менее есть два общих варианта выхода:
//  1. При получении сигнала отмены выйти из метода задачи, например, с помощью оператора return или построив
//  логику метода соответствующим образом. Но следует учитывать, что в этом случае задача перейдет в состояние
//  TaskStatus.RanToCompletion, а не в состояние TaskStatus.Canceled.
//  2. При получении сигнала отмены сгенерировать исключение OperationCanceledException, вызвав у токена метод
//  ThrowIfCancellationRequested(). После этого задача перейдет в состояние TaskStatus.Canceled.

#region Мягкий выход из задачи без исключения OperationCanceledException
//  Сначала рассмотрим первый - "мягкий" вариант завершения:
CancellationTokenSource _calcelTokenSourse = new CancellationTokenSource();
CancellationToken _token = _calcelTokenSourse.Token;

Task _task = new Task(() =>
{
    for (int i = 1; i < 10; i++)
    {
        if (_token.IsCancellationRequested)         // проверяем наличие сигнала отмены задачи
        {
            Console.WriteLine("Операция прервана");
            return;
        }
        Console.WriteLine($"Квадрат числа {i} равен: {i * i}");
        Thread.Sleep(300);                          // выходим из метода и тем самым завершаем задачу
    }
}, _token);
_task.Start();
Thread.Sleep(1000);
_calcelTokenSourse.Cancel();    // отменяем выполнение задачи
Thread.Sleep(1000);
Console.WriteLine($"Task Status {_task.Status}");  //  проверяем статус задачи
_calcelTokenSourse.Dispose();   // освобождаем ресурсы
//  В данном случае задача task вычисляет и выводит на консоль квадраты чисел от 1 до 9. Для отмены
//  задачи нам надо создать и использовать токен. Вначале создается объект CancellationTokenSource:
//  CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
//  Затем из него получаем сам токен:
//  CancellationToken token = cancelTokenSource.Token;
//  Чтобы отменить операцию, необходимо вызвать метод Cancel() у объекта CancellationTokenSource:
//  ancelTokenSource.Cancel();
#endregion

#region Отмена задачи с помощью генерации исключения
//  Второй способ завершения задачи представляет генерация исключения OperationCanceledException. Для этого применяется метод
//  ThrowIfCancellationRequested() объекта CancellationToken:
CancellationTokenSource _cancelTokenSource2 = new CancellationTokenSource();
CancellationToken _token2 = _cancelTokenSource2.Token;

Task _task2 = new Task(() =>
{
    for (int i = 1; i < 10; i++)
    {
        if (_token2.IsCancellationRequested)   // проверяем наличие сигнала отмены задачи
        {
            _token2.ThrowIfCancellationRequested();   // генерируем исключение
        }
        Console.WriteLine($"Квадрат числа {i} равен: {i * i}");
        Thread.Sleep(300);
    }
}, _token2);
try
{
    _task2.Start();
    Thread.Sleep(1000);
    _cancelTokenSource2.Cancel();    // Меняет значение IsCancellationRequested  на true

    _task2.Wait();   // ожидаем завершения задачи
}
catch (AggregateException ae)
{
    foreach (Exception e in ae.InnerExceptions)
    {
        if (e is TaskCanceledException)
        {
            Console.WriteLine("Операция прервана");
        }
        else
            Console.WriteLine(e.Message);
    }
}
finally
{
    _cancelTokenSource2.Dispose();
}

Console.WriteLine($"Task Status: {_task2.Status}");  //  проверяем статус задачи
//  Здесь опять же проверяем значение свойства IsCancellationRequested, и если оно равно true,
//  генерируем исключение

//  Стоит отметить, что исключение возникает только тогда, когда мы останавливаем текущий поток и
//  ожидаем завершения задачи с помощью методов Wait или WaitAll. Если эти методы не используются
//  для ожидания задачи, то для нее просто устанавливается состояние Canceled. 
#endregion

#region Регистрация обработчика отмены задачи
//  Выше для проверки сигнала отмены применялось свойство IsCancellationRequested. Но есть и другой
//  способ узнать о том, что был послан сигнал отмены задачи. Метод Register() позволяет зарегистрировать
//  обработчик отмены задачи в виде делегата Action:
CancellationTokenSource _cancelTokenSourse3 = new CancellationTokenSource();
CancellationToken _token3 = _cancelTokenSourse3.Token;
Task _task3 = new Task(() =>
{
    int i = 0;
    _token3.Register(()=> 
    {
        Console.WriteLine("Операция прервана");
        i = 10;
    });
    for (; i < 10; i++)
    {
        Console.WriteLine($"Квадрат числа {i} равен {i * i}");
        Thread.Sleep(200);
    }    
},_token3);
_task3.Start();
Thread.Sleep(1000);
_cancelTokenSourse3.Cancel();
Thread.Sleep(1000);
Console.WriteLine($"Task Status: {_task3.Status}");
_cancelTokenSourse3.Dispose();   // освобождаем ресурсы
//  Поскольку действие задачи представляет цикл, который выполняется при значении i меньше 10, то установка
//  этой переменной в обработчике отмены приведет к выходу из цикла и соответственно завершению задачи.
#endregion

#region Передача токена во внешний метод
//  Если операция, которая выполняется в задаче, представляет внешний метод, то ему можно передавать
//  в качестве одного из параметров:
CancellationTokenSource cancelTokenSource2 = new CancellationTokenSource();
CancellationToken token2 = cancelTokenSource2.Token;

Task task2 = new Task(() => PrintSquares(token2), token2);
try
{
    task2.Start();
    Thread.Sleep(1000);
    // после задержки по времени отменяем выполнение задачи
    cancelTokenSource2.Cancel();

    // ожидаем завершения задачи
    task2.Wait();
}
catch (AggregateException ae)
{
    foreach (Exception e in ae.InnerExceptions)
    {
        if (e is TaskCanceledException)
            Console.WriteLine("Операция прервана");
        else
            Console.WriteLine(e.Message);
    }
}
finally
{
    cancelTokenSource2.Dispose();
}

//  проверяем статус задачи
Console.WriteLine($"Task Status: {task2.Status}");


void PrintSquares(CancellationToken token)
{
    for (int i = 1; i < 10; i++)
    {
        if (token.IsCancellationRequested)
            token.ThrowIfCancellationRequested(); // генерируем исключение

        Console.WriteLine($"Квадрат числа {i} равен {i * i}");
        Thread.Sleep(200);
    }
}
#endregion

#region Отмена параллельных операций Parallel
//  Для отмены выполнения параллельных операций, запущенных с помощью методов Parallel.For() и
//  Parallel.ForEach(), можно использовать перегруженные версии данных методов, которые принимают
//  в качестве параметра объект ParallelOptions. Данный объект позволяет установить токен:
CancellationTokenSource _cancelTokenSourse4 = new CancellationTokenSource();
CancellationToken _token4 = _cancelTokenSourse4.Token;

new Task(()=>
{
    Thread.Sleep(400);
    _cancelTokenSourse4.Cancel();
}).Start();

try
{
    Parallel.ForEach<int>(new List<int>() { 1, 2, 3, 4, 5 }, 
        new ParallelOptions {CancellationToken = _token4 },Square);
    // или так
    //Parallel.For(1, 5, new ParallelOptions { CancellationToken = token }, Square);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Операция прервана");
}
finally
{
    _cancelTokenSourse4.Dispose();
}
void Square(int n)
{
    Thread.Sleep(3000);
    Console.WriteLine($"Квадрат числа {n} равен {n * n}");
}
//  В параллельной запущеной задаче через 400 миллисекунд происходит вызов cancelTokenSource.Cancel(),
//  в результате программа выбрасывает исключение OperationCanceledException, и выполнение параллельных
//  операций прекращается.
#endregion
