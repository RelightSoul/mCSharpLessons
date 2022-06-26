//  Асинхронные стримы

//  Начиная с версии C# 8.0 в C# были добавлены асинхронные стримы, которые упрощают работу со потоками
//  данных в асинхронном режиме. Хотя асинхронность в C# существует уже довольно давно, тем не менее
//  асинхронные методы до сих пор позволяли получать один объект, когда асинхронная операция была готова
//  предоставить результат. Для возвращения нескольких значений в C# могут применяться итераторы, но они
//  имеют синхронную природу, блокируют вызывающий поток и не могут использоваться в асинхронном контексте.
//  Асинхронные стримы обходят эту проблему, позволяя получать множество значений и возвращать их по мере
//  готовности в асинхронном режиме.

//  По сути асинхронный стрим представляет метод, который обладает тремя характеристиками:
//  1. метод имеет модификатор async
//  2. метод возращает объект IAsyncEnumerable<T>. Интерфейс IAsyncEnumerable определяет
//  метод GetAsyncEnumerator, который возвращает IAsyncEnumerator:
//public interface IAsyncEnumerable<out T>
//{
//    IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
//}

//public interface IAsyncEnumerator<out T> : IAsyncDisposable
//{
//    T Current { get; }
//    ValueTask<bool> MoveNextAsync();
//}
//public interface IAsyncDisposable
//{
//    ValueTask DisposeAsync();
//}
//  3. метод содержит выражения yield return для последовательного получения элементов из асинхронного стрима

//  Фактически асинхронный стрим объединяет асинхронность и итераторы. Рассмотрим простейший пример:
await foreach (var number in GetNumbersAsync())
{
    Console.WriteLine(number);
}

async IAsyncEnumerable<int> GetNumbersAsync()
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(100);
        yield return i;
    }
}
//  Итак, метод GetNumbersAsync() фактически и представляет асинхронный стрим. Этот метод является асинхронным.
//  Его возвращаемый тип - IAsyncEnumerable<int>. А его суть сводится к тому, что он возвращает с помощью yield
//  return каждые 100 миллисекунд некоторое число. То есть фактически метод должен вернуть 10 чисел от 0 до 9
//  с промежутком в 100 миллисекунд.

//  Для получения данных из стрима в методе Main используется цикл foreach:
//      await foreach (var number in GetNumbersAsync())
//  Важно отметить, что он предваряется оператором await. В итоге, каждый раз когда асинхронный стрим
//  будет возвращать очередное число, цикл будет его получать и выводить на консоль.

//  Где можно применять асинхронные стримы? Асинхронные стримы могут применяться для получения данных из
//  какого-нибудь внешнего хранилища. Например, пусть имеется следующий класс некоторого хранилища:
Repository repo = new Repository();
IAsyncEnumerable<string> data = repo.GetDataAsync();
await foreach (var name in data)
{
    Console.WriteLine(name);
}

class Repository
{
    string[] data = { "Tom", "Sam", "Kate", "Alice", "Bob" };
    public async IAsyncEnumerable<string> GetDataAsync()
    {
        for (int i = 0; i < data.Length; i++)
        {
            Console.WriteLine($"Получаем {i} элемент");
            await Task.Delay(500);
            yield return data[i];
        }
    }
}
//  Для упрощения примера данные здесь представлены в виде простого внутреннего массива строк.
//  Для имитации задержки в получении применяется метод Task.Delay.