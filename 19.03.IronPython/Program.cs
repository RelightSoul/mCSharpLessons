// Использование IronPython в .NET

//Одним из ключевых достоинств среды DLR является поддержка таких динамических языков как IronPython и IronRuby.
//Казалось бы, зачем нам нужны еще языки, тем более которые применяются в рамках другого языка C#?

//На самом деле динамические языки, возможно, не часто используются, однако есть сферы, где их применение является
//целесообразным. Например, написание клиентских сценариев. Возможно, пользователь нашей программы захочет внести
//какое-то дополнительное поведение в программу и для этого может использоваться IronPython. Можно даже сказать,
//что создание клиентских сценариев широко распространено в наши дни, многие программы и даже игры поддерживают
//добавление клиентских сценариев, написанных на различных языках.

//Кроме того, возможно, есть библиотеки на Python, функциональность которых может отсутствовать в .NET. И в этом
//случае опять же нам может помочь IronPython.

//Рассмотрим на примере применение IronPython. Но для начала необходимо добавить в проект несколько пакетов через
//пакетный менеджер NuGet. Для того нажмем в окне проекта на узел Dependencies правой кнопкой мыши и выберем в
//появившемся списке пункт Manage NuGet Packages... (Управление NuGet-пакетами):

//И перед нами откроется окно пакетного менеджера. Чтобы найти нужный пакет, введем в поле поиска "DLR", и менеджер
//отобразит ряд результатов, из которых первый - пакет DynamicLanguageRuntime необходимо установить.

//После этого в проект в узел Dependencies добавляется библиотека Microsoft.Scripting.

//Теперь также нам надо добавить пакет IronPython. Для этого введем в поле поиска "IronPython" и после этого
//установим одноименный пакет:

//После установки пакета в узле Dependencies добавляется библиотека IronPython.

//Теперь напишем примитивную программу:

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

ScriptEngine engine = Python.CreateEngine();
engine.Execute("print('hello, world')");    //hello, world
                                            //  Здесь используется выражение print('hello, world') языка Python, которое выводит на консоль строку.
                                            //  Для создания движка, выполняющего скрипт, применяется класс ScriptEngine. А его метод Execute()
                                            //  выполняет скрипт.
                                            //  Мы также могли бы определить файл, то есть обычный текстовый файл с кодом на языке Python
                                            //  и запустить его в программе
                                            //  ScriptEngine engine2 = Python.CreateEngine();
                                            //  engine2.ExecuteFile("Имя файла с кодом на языке Python");
                                            //  Также можно использовать абсолютные пути, например, если скрипт располагается по пути "D://hello.py":
                                            //  ScriptEngine engine3 = Python.CreateEngine();
                                            //  engine3.ExecuteFile("D://hello.py");

#region ScriptScope
//Объект ScriptScope позволяет взаимодействовать со скриптом, получая или устанавливая его переменные,
//получая ссылки на функции. Например, напишем простейший скрипт hello2.py, который использует переменные:

//x = 10
//z = x + y
//print(z)
//Теперь напишем программу, которая будет взаимодействовать со скриптом:

//using IronPython.Hosting;
//using Microsoft.Scripting.Hosting;

//int y = 22;

//ScriptEngine engine = Python.CreateEngine();
//ScriptScope scope = engine.CreateScope();
//scope.SetVariable("y", y);
//engine.ExecuteFile("hello.py", scope);
//dynamic x = scope.GetVariable("x");
//dynamic z = scope.GetVariable("z");
//Console.WriteLine($"{x} + {y} = {z}");
//Объект ScriptScope с помощью метода SetVariable позволяет установить переменные в скрипте, а с помощью
//метода GetVariable() - получить их.
#endregion

#region Вызов функций из IronPython
//  Определим в файле hello.py функцию для вычисления квадрата числа:
//  def square(n):
//       return n * n
//  Теперь обратимся к этой функции в коде C#:

//using IronPython.Hosting;
//using Microsoft.Scripting.Hosting;
 
//int number = 5;

//ScriptEngine engine = Python.CreateEngine();
//ScriptScope scope = engine.CreateScope();

//engine.ExecuteFile("hello.py", scope);
//dynamic square = scope.GetVariable("square");
//// вызываем функцию и получаем результат
//dynamic result = square(number);
//Console.WriteLine(result);      // 25
//Получить объект функции можно также, как и переменную: scope.GetVariable("square");. Затем с этим объектом
//работаем также, как и с любым другим методом. В итоге при передаче в метод/функцию square числа 5 его
//результатом будет 25.
#endregion