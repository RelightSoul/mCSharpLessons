//  Работа с файлами. Классы File и FileInfo
//  Подобно паре Directory/DirectoryInfo для работы с файлами предназначена пара классов File и FileInfo.
//  С их помощью мы можем создавать, удалять, перемещать файлы, получать их свойства и многое другое.

#region FileInfo
//Некоторые полезные методы и свойства класса FileInfo:
//CopyTo(path): копирует файл в новое место по указанному пути path
//Create(): создает файл
//Delete(): удаляет файл
//MoveTo(destFileName): перемещает файл в новое место
//Свойство Directory: получает родительский каталог в виде объекта DirectoryInfo
//Свойство DirectoryName: получает полный путь к родительскому каталогу
//Свойство Exists: указывает, существует ли файл
//Свойство Length: получает размер файла
//Свойство Extension: получает расширение файла
//Свойство Name: получает имя файла
//Свойство FullName: получает полное имя файла

//Для создания объекта FileInfo применяется конструктор, который получает в качестве параметра путь к файлу:
using System.Text;

FileInfo fileInf = new FileInfo(@"C:\app\content.txt");
#endregion

#region File
//Класс File реализует похожую функциональность с помощью статических методов:
//Copy(): копирует файл в новое место
//Create(): создает файл
//Delete(): удаляет файл
//Move: перемещает файл в новое место
//Exists(file): определяет, существует ли файл
#endregion

#region Пути к файлам
//  Для работы с файлами можно применять как абсолютные, так и относительные пути:
//  абсолютные пути
string path1 = @"C:\Users\eugene\Documents\content.txt";  // для Windows
string path2 = "C:\\Users\\eugene\\Documents\\content.txt";  // для Windows
string path3 = "/Users/eugene/Documents/content.txt";  // для MacOS/Linux

// относительные пути
string path4 = "MyDir\\content.txt";  // для Windows
string path5 = "MyDir/content.txt";  // для MacOS/Linux
#endregion

#region Получение информации о файле
string path = @"C:\Users\eugene\Documents\content.txt";
// string path = "/Users/eugene/Documents/content.txt";  // для MacOS/Linux
FileInfo fileInfo = new FileInfo(path);
if (fileInfo.Exists)
{
    Console.WriteLine($"Имя файла: {fileInfo.Name}");
    Console.WriteLine($"Время создания: {fileInfo.CreationTime}");
    Console.WriteLine($"Размер: {fileInfo.Length}");
}
#endregion

#region Удаление файла
string path6 = @"C:\app\content.txt";
FileInfo fileInf6 = new FileInfo(path6);
if (fileInf6.Exists)
{
    fileInf6.Delete();
    // альтернатива с помощью класса File
    //File.Delete(path);
}
#endregion

#region Перемещение файла
string path7 = @"C:\OldDir\content.txt";
string newPath = @"C:\NewDir\index.txt";
FileInfo fileInf7 = new FileInfo(path7);
if (fileInf7.Exists)
{
    fileInf7.MoveTo(newPath);
    // альтернатива с помощью класса File
    // File.Move(path, newPath);
}
//  Если файл по новому пути уже существует, то с помощью дополнительного параметра можно указать,
//  надо ли перезаписать файл (при значении true файл перезаписывается)
string path8 = @"C:\OldDir\content.txt";
string newPath8 = @"C:\NewDir\index.txt";
FileInfo fileInf8 = new FileInfo(path8);
if (fileInf8.Exists)
{
    fileInf8.MoveTo(newPath8, true);     // дополнительный bool параметр подтверждающий перезапись
    // альтернатива с помощью класса File
    // File.Move(path, newPath, true);
}
#endregion

#region Копирование файла
string path9 = @"C:\OldDir\content.txt";
string newPath9 = @"C:\NewDir\index2.txt";
FileInfo fileInf9 = new FileInfo(path9);
if (fileInf9.Exists)
{
    fileInf9.CopyTo(newPath9, true);
    // альтернатива с помощью класса File
    // File.Copy(path, newPath, true);
}
//  Метод CopyTo класса FileInfo принимает два параметра: путь, по которому файл будет копироваться,
//  и булевое значение, которое указывает, надо ли при копировании перезаписывать файл (если true,
//  как в случае выше, файл при копировании перезаписывается). Если же в качестве последнего параметра
//  передать значение false, то если такой файл уже существует, приложение выдаст ошибку.

//  Метод Copy класса File принимает три параметра: путь к исходному файлу, путь, по которому файл
//  будет копироваться, и булевое значение, указывающее, будет ли файл перезаписываться.
#endregion

#region Чтение и запись файлов
//  В дополнение к вышерассмотренным методам класс File также предоставляет ряд методов для чтения-записи
//  текстовых и бинарных файлов:

//  AppendAllLines(String, IEnumerable<String>) / AppendAllLinesAsync(String, IEnumerable<String>,
//  CancellationToken)
//  добавляют в файл набор строк. Если файл не существует, то он создается

//  AppendAllText(String, String) / AppendAllTextAsync(String, String, CancellationToken)
//  добавляют в файл строку. Если файл не существует, то он создается

//  byte[] ReadAllBytes (string path) / Task<byte[]> ReadAllBytesAsync(string path,
//  CancellationToken cancellationToken)
//  считывают содержимое бинарного файла в массив байтов

//  string[] ReadAllLines (string path) / Task<string[]> ReadAllLinesAsync(string path,
//  CancellationToken cancellationToken)
//  считывают содержимое текстового файла в массив строк

//  string ReadAllText (string path) / Task<string> ReadAllTextAsync(string path,
//  CancellationToken cancellationToken)
//  считывают содержимое текстового файла в строку

//  IEnumerable<string> ReadLines (string path)
//  считывают содержимое текстового файла в коллекцию строк

//  void WriteAllBytes (string path, byte[] bytes) / Task WriteAllBytesAsync(string path, byte[] bytes,
//  CancellationToken cancellationToken)
//  записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует,
//  то перезаписывается

//  void WriteAllLines (string path, string[] contents) / Task WriteAllLinesAsync(string path,
//  IEnumerable<string> contents, CancellationToken cancellationToken)
//  записывают массив строк в текстовый файл. Если файл не существует, он создается. Если существует,
//  то перезаписывается

//  WriteAllText (string path, string? contents) / Task WriteAllTextAsync(string path, string ? contents,
//  CancellationToken cancellationToken)
//  записывают строку в текстовый файл. Если файл не существует, он создается. Если существует, то перезаписывается

//  Как видно, эти методы покрывают практически все основные сценарии - чтение и запись текстовых и
//  бинарных файлов. Причем в зависимости от задачи можно применять как синхронные методы, так и их
//  асинхронные аналоги.

//  Например, запишем и считаем обратно в строку текстовый файл:
string _path = @"c:\app\content.txt";
string originalText = "Hello Metanit.com";
// запись строки
await File.WriteAllTextAsync(_path, originalText);
// дозапись в конец файла
await File.AppendAllTextAsync(_path, "\nHello work");

// чтение файла
string fileText = await File.ReadAllTextAsync(_path);
Console.WriteLine(fileText);
#endregion

#region Кодировка
//  В качестве дополнительного параметра методы чтения-записи текстовых файлов позволяют установить кодировку
//  в виде объекта System.Text.Encoding:
string path99 = "/Users/eugene/Documents/app/content.txt";

string originalText99 = "Привет Metanit.com";
// запись строки
await File.WriteAllTextAsync(path99, originalText99, Encoding.Unicode);
// дозапись в конец файла
await File.AppendAllTextAsync(path99, "\nПривет мир", Encoding.Unicode);

// чтение файла
string fileText99 = await File.ReadAllTextAsync(path99, Encoding.Unicode);
Console.WriteLine(fileText99);

//  Для установки кодировки при записи и чтении здесь применяется встроенное значение Encoding.Unicode.
//  Также можно указать название кодировки, единственное следует удостовериться, что текущая операционная
//  система поддерживает выбранную кодировку:
string path77 = @"c:\app\content.txt";

string originalText77 = "Hello Metanit.com";
// запись строки
await File.WriteAllTextAsync(path77, originalText77, Encoding.GetEncoding("iso-8859-1"));
// дозапись в конец файла
await File.AppendAllTextAsync(path77, "\nHello code", Encoding.GetEncoding("iso-8859-1"));

// чтение файла
string fileText77 = await File.ReadAllTextAsync(path77, Encoding.GetEncoding("iso-8859-1"));
Console.WriteLine(fileText77);
#endregion