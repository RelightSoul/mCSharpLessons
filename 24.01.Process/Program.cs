//  Процессы и домены приложения
//  Процессы

//При запуске приложения операционная система создает для него отдельный процесс, которому выделяется определённое адресное
//пространство в памяти и который изолирован от других процессов. Процесс может иметь несколько потоков. Как минимум, процесс
//содержит один - главный поток. В приложении на C# точкой входа в программу является метод Main. Вызов этого метода
//автоматически создает главный поток. А из главного потока могут запускаться вторичные потоки.

//В .NET процесс представлен классом Process из пространства имен System.Diagnostics. Этот класс позволяет управлять уже
//запущенными процессами, а также запускать новые. В данном классе определено ряд свойств и методов, позволяющих получать
//информацию о процессах и управлять ими:
//Свойство Handle: возвращает дескриптор процесса
//Свойство Id: получает уникальный идентификатор процесса в рамках текущего сеанса ОС
//Свойство MachineName: возвращает имя компьютера, на котором запущен процесс
//Свойство MainModule: представляет основной модуль - исполняемый файл программы, представлен объектом типа ProcessModule
//Свойство Modules: получает доступ к коллекции ProcessModuleCollection, которая в виде объектов ProcessModule хранит набор
//модулей (например, файлов dll и exe), загруженных в рамках данного процесса
//Свойство ProcessName: возвращает имя процесса, которое нередко совпадает с именем приложения
//Свойство StartTime: возвращает время, когда процесс был запущен
//Свойство PageMemorySize64: возвращает объем памяти, который выделен для данного процесса
//Свойство VirtualMemorySize64: возвращает объем виртуальной памяти, который выделен для данного процесса
//Метод CloseMainWindow(): закрывает окно процесса, который имеет графический интерфейс
//Метод GetProcesses(): возвращающий массив всех запущенных процессов
//Метод GetProcesses(): возвращающий массив всех запущенных процессов
//Метод GetProcessesByName(): возвращает процессы по его имени. Так как можно запустить несколько копий одного приложения,
//то возвращает массив
//Метод GetProcessById(): возвращает процесс по Id. Так как можно запустить несколько копий одного приложения, то возвращает
//массив
//Метод Kill(): останавливает процесс
//Метод Start(): запускает новый процесс

//  Например, исследуем текущий процесс:
using System.Diagnostics;

var process = Process.GetCurrentProcess();
Console.WriteLine($"Id: {process.Id}");
Console.WriteLine($"Name: {process.ProcessName}");
Console.WriteLine($"VirtualMemory: {process.VirtualMemorySize64}");

//  Получим все запущенные процессы:
foreach (Process process1 in Process.GetProcesses())
{
    // выводим id и имя процесса
    Console.WriteLine($"ID: {process1.Id}  Name: {process1.ProcessName}");
}

//  Получим id процессов, который представляют запущенные экземпляры Visual Studio:
Process[] vsProcs = Process.GetProcessesByName("devenv");   // для Windows
// Process[] vsProcs = Process.GetProcessesByName("VisualStudio"); //  для MacOS
foreach (var proc in vsProcs)
    Console.WriteLine($"ID: {proc.Id}");

#region Потоки процесса
//Свойство Threads представляет коллекцию потоков процесса - объект ProcessThreadCollection, каждый поток в которой является
//объектом ProcessThread. В данном классе можно выделить следующие свойства:
//  CurrentPriority: возвращает текущий приоритет потока
//  Id: идентификатор потока
//  IdealProcessor: позволяет установить процессор для обработки потока
//  PriorityLevel: уровень приоритета потока
//  StartAddress: адрес в памяти функции, запустившей поток
//  StartTime: время запуска потока (поддерживается только на Windows и Linux)

Process proc2 = Process.GetProcessesByName("devenv")[0];  // Windows
// Process proc = Process.GetProcessesByName("VisualStudio")[0];  // MacOS
ProcessThreadCollection processThreads = proc2.Threads;

foreach (ProcessThread thread in processThreads)
{
    Console.WriteLine($"ThreadId: {thread.Id}");
}
#endregion

#region Модули процесса
//Одно приложение может использовать набор различных сторонних библиотек и модулей. Для их получения класс Prosess имеет
//свойство Modules, которое представляет объект ProcessModuleCollection. Каждый отдельный модуль представлен классом
//ProcessModule, у которого можно выделить следующие свойства:
//BaseAddress: адрес модуля в памяти
//FileName: полный путь к файлу модуля
//EntryPointAddress: адрес функции в памяти, которая запустила модуль
//ModuleName: название модуля(краткое имя файла)
//ModuleMemorySize: возвращает объем памяти, необходимый для загрузки модуля

//Получим все модули, используемые Visual Studio:
Process proc3 = Process.GetProcessesByName("devenv")[0]; // для Windows
// Process proc = Process.GetProcessesByName("VisualStudio")[0]; // для MacOS
ProcessModuleCollection modules = proc3.Modules;

foreach (ProcessModule module in modules)
{
    Console.WriteLine($"Name: {module.ModuleName}  FileName: {module.FileName}");
}
#endregion

#region Запуск нового процесса
//  С помощью статического метода Process.Start() можно запустить новый процесс. Например:
// обращение к исполняемой программе
Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome");

// Process.Start("/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"); // на MacOS

//  В данном случае запускается браузер Google Chrome

//  При обращении к исполняемому файлу .NET запускает приложение.

//  Однако при запуске некоторых программ может потребоваться передать им различные параметры. В этом случае можно использовать
//  перегруженную версию метода, передавая в качестве второго параметра параметры:
//  Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome", "https://metanit.com");
//  Чтобы отделить настройку параметров запуска от самого запуска можно использовать класс ProcessStartInfo:
ProcessStartInfo procInfo = new ProcessStartInfo();
// исполняемый файл программы - браузер хром
procInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome";
// аргументы запуска - адрес интернет-ресурса
procInfo.Arguments = "https://metanit.com";
Process.Start(procInfo);
#endregion