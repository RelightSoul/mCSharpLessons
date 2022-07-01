//  Чтение и запись текстовых файлов. StreamReader и StreamWriter
//  Для работы непосредственно с текстовыми файлами в пространстве System.IO определены специальные классы:
//  StreamReader и StreamWriter.

#region Запись в файл и StreamWriter
//  Для записи в текстовый файл используется класс StreamWriter. Некоторые из его конструкторов, которые могут
//  применяться для создания объекта StreamWriter:
//  StreamWriter(string path): через параметр path передается путь к файлу, который будет связан с потоком
//  StreamWriter(string path, bool append): параметр append указывает, надо ли добавлять в конец файла данные
//  или же перезаписывать файл. Если равно true, то новые данные добавляются в конец файла. Если равно false,
//  то файл перезаписываетсяя заново
//  StreamWriter(string path, bool append, System.Text.Encoding encoding): параметр encoding указывает на
//  кодировку, которая будет применяться при записи

//  Свою функциональность StreamWriter реализует через следующие методы:
//  int Close(): закрывает записываемый файл и освобождает все ресурсы
//  void Flush(): записывает в файл оставшиеся в буфере данные и очищает буфер.
//  Task FlushAsync(): асинхронная версия метода Flush
//  void Write(string value): записывает в файл данные простейших типов, как int, double, char, string и т.д.
//  Соответственно имеет ряд перегруженных версий для записи данных элементарных типов, например,
//  Write(char value), Write(int value), Write(double value) и т.д.
//  Task WriteAsync(string value): асинхронная версия метода Write. Обратите внимание, что асинхронные версии
//  есть не для всех перегрузок метода Write.
//  void WriteLine(string value): также записывает данные, только после записи добавляет в файл символ
//  окончания строки
//  Task WriteLineAsync(string value): асинхронная версия метода WriteLine

//  Рассмотрим запись в файл на примере:

string path = "note1.txt";
string text = "Hello World\nHello METANIT.COM";

// полная перезапись файла 
using (StreamWriter writer = new StreamWriter(path, false))
{
    await writer.WriteLineAsync(text);
}
// добавление в файл
using (StreamWriter writer = new StreamWriter(path, true))
{
    await writer.WriteLineAsync("Addition");
    await writer.WriteAsync("4,5");
}
//  В данном случае два раза создаем объект StreamWriter. В первом случае если файл существует, то он будет
//  перезаписан. Если не существует, он будет создан. И в нее будет записан текст из переменной text. Во втором
//  случае файл открывается для дозаписи, и будут записаны атомарные данные - строка и число.
#endregion

#region Чтение из файла и StreamReader
//  Класс StreamReader позволяет нам легко считывать весь текст или отдельные строки из текстового файла.

//  Некоторые из конструкторов класса StreamReader:
//  StreamReader(string path): через параметр path передается путь к считываемому файлу
//  StreamReader(string path, System.Text.Encoding encoding): параметр encoding задает кодировку для чтения файла

//  Среди методов StreamReader можно выделить следующие:
//  void Close(): закрывает считываемый файл и освобождает все ресурсы
//  int Peek(): возвращает следующий доступный символ, если символов больше нет, то возвращает -1
//  int Read(): считывает и возвращает следующий символ в численном представлении. Имеет перегруженную версию:
//  Read(char[] array, int index, int count), где array - массив, куда считываются символы, index - индекс в
//  массиве array, начиная с которого записываются считываемые символы, и count - максимальное количество
//  считываемых символов
//  Task<int> ReadAsync(): асинхронная версия метода Read
//  string ReadLine(): считывает одну строку в файле
//  string ReadLineAsync(): асинхронная версия метода ReadLine
//  string ReadToEnd(): считывает весь текст из файла
//  string ReadToEndAsync(): асинхронная версия метода ReadToEnd

//  Сначала считаем текст полностью из ранее записанного файла:

//  асинхронное чтение
using (StreamReader reader = new StreamReader(path))
{
    string text2 = await reader.ReadToEndAsync();
    Console.WriteLine(text2);
}

//  Считаем текст из файла построчно:
// асинхронное чтение
using (StreamReader reader = new StreamReader(path))
{
    string? line;
    while ((line = await reader.ReadLineAsync()) != null)
    {
        Console.WriteLine(line);
    }
}
//  В данном случае считываем построчно через цикл while: while ((line = await reader.ReadLineAsync()) != null)
//  - сначала присваиваем переменной line результат функции reader.ReadLineAsync(), а затем проверяем, не равна
//  ли она null. Когда объект sr дойдет до конца файла и больше строк не останется, то метод reader.ReadLineAsync()
//  будет возвращать null.
#endregion

