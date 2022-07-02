// AssemblyLoadContext и динамическая загрузка и выгрузка сборок

//  В статье Динамическая загрузка сборок и позднее связывание рассматривалось, динамически загружать в приложение сборки и
//  использовать их функционал. Но фреймворк .NET также позволяет выгружать сборки, что позволяет уменьшить объем потребляемой
//  памяти. Для этого применяется класс AssemblyLoadContext из пространства имен System.Runtime.Loader, который представляет
//  контекст загрузки и выгрузки сборок. Рассмотрим, как его использовать.

//  Допустим, у нас есть консольный проект MyApp со следующим файлом Program.cs:
//class Program
//{
//    static void Main(string[] args)
//    {
//        var number = 5;
//        var result = Square(number);
//        Console.WriteLine($"Квадрат {number} равен {result}");
//    }
//    static int Square(int n) => n * n;
//}
//  Эта программа содержит метод Square для вычисления квадрата, и по умолчанию она компилируется в сборку MyApp.dll.
//  Загрузим эту сборку, чтобы использовать ее метод Square.

//  Для создания объекта AssemblyLoadContext применяется следующий конструктор:
//          public AssemblyLoadContext (string? name, bool isCollectible = false);
//  В конструкторе первый параметр устанавливает имя контекста - это может произвольная строка. Второй параметр -
//  isCollectible устанавливает, можно ли загруженные сборки выгружать. Значение true указывает, что загруженные
//  сборки можно выгружать.

//  Для загрузки сборок класс AssemblyLoadContext предоставляет ряд методов. Некоторые из них:
//  Assembly LoadFromAssemblyName(AssemblyName assemblyName): загружает определенную сборку по имени, которое представлено
//  типом System.Reflection.AssemblyName
//  Assembly LoadFromAssemblyPath (string assemblyPath): загружает сборку по определенному пути (путь должен быть абсолютным)
//  Assembly LoadFromStream(System.IO.Stream stream): загружает определенную сборку из потока Stream

//Использовав один из этих методов, мы можем получить доступ к сборке через тип Assembly и обращаться к ее функционалу.

//После завершения работы со сборкой мы можем вызвать у AssemblyLoadContext метод Unload() и выгрузить контекст со всеми
//загруженными сборками и тем самым снизить потребление памяти и увеличить общую производительность.
using System.Reflection;
using System.Runtime.Loader;

Square(8);
// очистка памяти
GC.Collect();
GC.WaitForPendingFinalizers();

Console.WriteLine();
// смотрим, какие сборки после выгрузки
foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
    Console.WriteLine(asm.GetName().Name);


void Square(int number)
{
    var context = new AssemblyLoadContext(name: "Square", isCollectible: true);
    // установка обработчика выгрузки
    context.Unloading += Context_Unloading;

    // получаем путь к сборке MyApp
    var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "MyApp.dll");
    // загружаем сборку
    Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);

    // получаем тип Program из сборки MyApp.dll
    var type = assembly.GetType("MyApp.Program");
    if (type != null)
    {
        // получаем его метод Square
        var squareMethod = type.GetMethod("Square");
        // вызываем метод
        var instance = Activator.CreateInstance(type);
        var result = squareMethod?.Invoke(instance, new object[] { number });
        if (result is int)
        {
            // выводим результат метода на консоль
            Console.WriteLine($"Квадрат числа {number} равен {result}");
        }
    }

    // смотим, какие сборки у нас загружены
    foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
        Console.WriteLine(asm.GetName().Name);

    // выгружаем контекст
    context.Unload();
}


// обработчик выгрузки контекста
void Context_Unloading(AssemblyLoadContext obj)
{
    Console.WriteLine("Библиотека MyApp выгружена");
}

//  Все эти действия оформляются в виде отдельного метода Square(). В качестве параметра он принимает число, квадрат которого
//  надо вычислить.

//  Вначале в методе создается объект AssemblyLoadContext:
//  var context = new AssemblyLoadContext(name: "Square", isCollectible: true);
//  Обратите внимание, что параметру isCollectible передается значение true, что позволит выгружать ранее загруженные сборки.

//  Класс AssemblyLoadContext определяет событие Unloadig, благодаря чему мы можем повесить обработчик и определить момент
//  выгрузки контекста.
//  context.Unloading += Context_Unloading;

//  Далее используется метод LoadFromAssemblyPath для загузки сборки MyApp.dll по абсолютному пути. В данном случае
//  предполагается, что файл сборки находится в одной папке с текущим приложением.
//  Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
//  Получив сборку, с помощью рефлексии обращаемся к методу Square и получаем квадрат числа.

//  Затем смотрим, какие сборки загружены в текущий домен. Среди них мы сможем найти и MyApp.dll. И в конце выгружаем контекст:
//  context.Unload();

//  Данный метод Square вызывается в методе Main:
//       Square(8);
//       // очистка
//       GC.Collect();
//       GC.WaitForPendingFinalizers();

//  Но обратите внимание, что выгрузка контекста сама по себе не означает немедленной очистки памяти. Вызов метода Unload
//  только инициирует процесс выгрузки, реальная выгрузка произойдет лишь тогда, когда в дело вступит автоматический сборщик
//  мусора и удалит соответствующие объекты. Поэтому для более быстрой очистки в конце вызываются методы GC.Collect() и
//  GC.WaitForPendingFinalizers().