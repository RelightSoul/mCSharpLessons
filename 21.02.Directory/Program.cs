//  Работа с каталогами
//  Для работы с каталогами в пространстве имен System.IO предназначены сразу два класса: Directory и DirectoryInfo.

#region Класс Directory
//  Статический класс Directory предоставляет ряд методов для управления каталогами. Некоторые из этих методов:
//  CreateDirectory(path): создает каталог по указанному пути path
//  Delete(path): удаляет каталог по указанному пути path
//  Exists(path): определяет, существует ли каталог по указанному пути path. Если существует,
//  возвращается true, если не существует, то false
//  GetCurrentDirectory(): получает путь к текущей папке
//  GetDirectories(path): получает список подкаталогов в каталоге path
//  GetFiles(path): получает список файлов в каталоге path
//  GetFileSystemEntries(path): получает список подкаталогов и файлов в каталоге path
//  Move(sourceDirName, destDirName): перемещает каталог
//  GetParent(path): получение родительского каталога
//  GetLastWriteTime(path): возвращает время последнего изменения каталога
//  GetLastAccessTime(path): возвращает время последнего обращения к каталогу
//  GetCreationTime(path): возвращает время создания каталога
#endregion

#region Класс DirectoryInfo
//  Данный класс предоставляет функциональность для создания, удаления, перемещения и других операций с каталогами.
//  Во многом он похож на Directory, но не является статическим.

//  Для создания объекта класса DirectoryInfo применяется конструктор, который в качестве параметра принимает
//  путь к каталогу:
//          public DirectoryInfo (string path);

//  Основные методы класса DirectoryInfo:
//  Create(): создает каталог
//  CreateSubdirectory(path): создает подкаталог по указанному пути path
//  Delete(): удаляет каталог
//  GetDirectories(): получает список подкаталогов папки в виде массива DirectoryInfo
//  GetFiles(): получает список файлов в папке в виде массива FileInfo
//  MoveTo(destDirName): перемещает каталог

//  Основные свойства класса DirectoryInfo:
//  CreationTime: представляет время создания каталога
//  LastAccessTime: представляет время последнего доступа к каталогу
//  LastWriteTime: представляет время последнего изменения каталога
//  Exists: определяет, существует ли каталог
//  Parent: получение родительского каталога
//  Root: получение корневого каталога
//  Name: имя каталога
//  FullName: полный путь к каталогу
#endregion

#region Directory или DirectoryInfo
//  Как видно из функционала, оба класса предоставляют похожие возможности. Когда же и что использовать?
//  Если надо совершить одну-две операции с одним каталогом, то проще использовать класс Directory. Если
//  необходимо выполнить последовательность операций с одним и тем же каталогом, то лучше воспользоваться
//  классом DirectoryInfo. Почему? Дело в том, что методы класса Directory выполняют дополнительные
//  проверки безопасности. А для класса DirectoryInfo такие проверки не всегда обязательны.

//  Посмотрим на примерах применение этих классов
#endregion

#region Получение списка файлов и подкаталогов
using System.IO;

string dirName = "C:\\";
// если папка существует
if (Directory.Exists(dirName))
{
    Console.WriteLine("Подкаталоги: ");
    string[] dirs = Directory.GetDirectories(dirName);
    foreach (var directory in dirs)
    {
        Console.WriteLine(directory);
    }
    Console.WriteLine();
    Console.WriteLine("Файлы: ");
    string[] files = Directory.GetFiles(dirName);
    foreach (var file in files)
    {
        Console.WriteLine(file);
    }
}
//  Обратите внимание на использование слешей в именах файлов. Либо мы используем двойной слеш:
//  "C:\\", либо одинарный, но тогда перед всем путем ставим знак @: @"C:\Program Files"

//  Аналогичный пример с DirectoryInfo:
Console.WriteLine();
string _dirName = "C:\\";
var _directory = new DirectoryInfo(_dirName);
if (_directory.Exists)
{
    Console.WriteLine("Подкаталоги: ");
    DirectoryInfo[] _dirs = _directory.GetDirectories();
    foreach (var dir in _dirs)
    {
        Console.WriteLine(dir);
    }
    Console.WriteLine();
    Console.WriteLine("Файлы: ");
    FileInfo[] _files = _directory.GetFiles();
    foreach (var file in _files)
    {
        Console.WriteLine(file);
    }
}
#endregion

#region Фильтрация папок и файлов
//  Методы получения папок и файлов позволяют выполнять фильтрацию. В качестве фильтра в эти методы передается
//  шаблон, который может содержать два плейсхолдера: * или символ-звездочка (соответствует любому количеству
//  символов) и ? или вопросительный знак (соответствует одному символу)

//  Например, найдем все папки, которые начинаются на "books":
//  класс Directory
string[] dirs22 = Directory.GetDirectories(dirName,"books*.");
// класс DirectoryInfo
var directory22 = new DirectoryInfo(dirName);
DirectoryInfo[] _dirs22 = directory22.GetDirectories("books*.");

//  Или получим все файлы с расширением ".exe":
// класс Directory
string[] files33 = Directory.GetFiles(dirName, "*.exe");

// класс DirectoryInfo
var directory33 = new DirectoryInfo(dirName);
FileInfo[] _files33 = directory33.GetFiles("*.exe");
#endregion

#region Создание каталога
//  Класс DirectoryInfo
string path = @"C:\SomeDir";
string subpath = @"program\avalon";
DirectoryInfo directoryInfo = new DirectoryInfo(path);
if (!directoryInfo.Exists)
{
    directoryInfo.Create();
}
directoryInfo.CreateSubdirectory(subpath);
//  Вначале проверяем, а нету ли такой директории, так как если она существует, то ее создать будет нельзя,
//  и приложение выбросит ошибку. В итоге у нас получится следующий путь: "C:\SomeDir\program\avalon"

//  Аналогичный пример с классом Directory:
string _path = @"C:\SomeDir";
string _subpath = @"program\avalon";
if (!Directory.Exists(_path))
{
    Directory.CreateDirectory(_path);
}
Directory.CreateDirectory($"{_path}/{_subpath}");
#endregion

#region Получение информации о каталоге
string dirNamePro = "C:\\Program Files";
DirectoryInfo dirInfoPro = new DirectoryInfo(dirNamePro);
Console.WriteLine(dirInfoPro.Name);
Console.WriteLine(dirInfoPro.FullName);
Console.WriteLine(dirInfoPro.CreationTime);
Console.WriteLine(dirInfoPro.Root);
#endregion

#region Удаление каталога
//  Если мы просто применим метод Delete к непустой папке, в которой есть какие-нибудь файлы или подкаталоги,
//  то приложение нам выбросит ошибку. Поэтому нам надо передать в метод Delete дополнительный параметр
//  булевого типа, который укажет, что папку надо удалять со всем содержимым. Кроме того, перед удалением
//  следует проверить наличие удаляемой папки, иначе приложение выбросит исключение:
string dirNameDel = @"C:\SomeDir";
DirectoryInfo dirInfoDel = new DirectoryInfo(dirNameDel);
if (dirInfoDel.Exists)
{
    dirInfoPro.Delete(true);
    Console.WriteLine("Удалён");
}
else
{
    Console.WriteLine("Не существует");
}
//  Или так:
string delldirName = @"C:\SomeDir";
if (Directory.Exists(delldirName))
{
    Directory.Delete(delldirName, true);
    Console.WriteLine("Каталог удален");
}
else
{
    Console.WriteLine("Каталог не существует");
}
#endregion

#region Перемещение каталога
//  При перемещении надо учитывать, что новый каталог, в который мы хотим перемесить все содержимое старого
//  каталога, не должен существовать.
string oldPath = @"C:\SomeFolder";
string newPath = @"C:\SomeDir";
DirectoryInfo dirInfoRelocate = new DirectoryInfo(oldPath);
if (dirInfoRelocate.Exists && !Directory.Exists(newPath))
{
    dirInfoRelocate.MoveTo(newPath);
    // или так
    // Directory.Move(oldPath, newPath);
}
#endregion