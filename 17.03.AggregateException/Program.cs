//  Обработка ошибок и отмена операции

//  При выполнении параллельных операций также могут возникать ошибки, обработка которых имеет свои особенности.
//  При параллельной обработке коллекция разделяется а части, и каждая часть обрабатывается в отдельном потоке.
//  Однако если возникнет ошибка в одном из потоков, то система прерывает выполнение всех потоков.

//  При генерации исключений все они агрегируются в одном исключении типа AggregateException

//  Например, пусть в метод факториала передается массив объектов, который содержит не только числа, но и строки:
object[] numbers = new object[] { 1, 2, 3, 4, 5, "6" };
var squares = from i in numbers.AsParallel()
              let x = (int)i
              select Square(x);
try
{
    squares.ForAll(n => Console.WriteLine(n));
}
catch (AggregateException ex)
{
    foreach (var e in ex.InnerExceptions)
    {
        Console.WriteLine(e.Message);
    }
}
int Square(int i) => i * i;
//  Запустим проект без отладки. И так как массив содержит строку, то попытка приведения закончится неудачей,
//  и на консоль будет выведено сообщение об ошибке. При запуске приложения в Visual Studio в режиме отладки
//  выполнение остановится на строке преобразования. А после продолжения также сработает перехват исключения
//  в блоке catch, и на консоль будет выведено сообщение об ошибке.

#region Прерывание параллельной операции
//  Вполне вероятно, что нам может потребоваться прекратить операцию до ее завершения. В этом случае мы можем
//  использовать метод WithCancellation(), которому в качестве параметра передается токен CancellationToken:
CancellationTokenSource cts = new CancellationTokenSource();
// запускаем дополнительную задачу, в которой через 400 миллисек. прерываем операцию
new Task(() =>
{
    Thread.Sleep(400);
    cts.Cancel();
}).Start();

try
{
    int[] numbers2 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, };

    var squares2 = from n in numbers2.AsParallel().WithCancellation(cts.Token)
                  select Square2(n);

    foreach (var n in squares2)
        Console.WriteLine(n);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Операция была прервана");
}
catch (AggregateException ex)
{
    if (ex.InnerExceptions != null)
    {
        foreach (Exception e in ex.InnerExceptions)
            Console.WriteLine(e.Message);
    }
}
finally
{
    cts.Dispose();
}
int Square2(int n)
{
    var result = n * n;
    Console.WriteLine($"Квадрат числа {n} равен {result}");
    Thread.Sleep(1000); // имитация долгого вычисления
    return result;
}
//  В параллельной запущенной задаче вызывается метод cts.Cancel(), что приводит к завершению операции и
//  генерации исключения OperationCanceledException. При этом также имеет смысл обрабатывать исключение
//  AggregateException, так как если параллельно возникает еще одно исключение, то это исключение, а также
//  OperationCanceledException помещаются внутрь одного объекта AggregateException.
#endregion