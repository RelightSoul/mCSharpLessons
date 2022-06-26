//  Работа с классом Task

#region Вложенные задачи
//  Одна задача может запускать другую - вложенную задачу. При этом эти задачи выполняются независимо друг
//  от друга. Например:
using System.Threading.Tasks;

var outer = Task.Factory.StartNew(() =>
{
    Console.WriteLine("Other task starting...");
    var inner = Task.Factory.StartNew(() =>
    {
        Console.WriteLine("Inner task starting");
        Thread.Sleep(1500);
        Console.WriteLine("Inner task finished");
    });
});
outer.Wait();
Console.WriteLine("End of Main");
//  При этом внутренняя задача может даже не начать свое выполнение к завершению работы основного потока
//  программы. То есть в данном случае внешняя и вложенная задачи выполняются независимо друг от друга.

//  Если необходимо, чтобы вложенная задача выполнялась как часть внешней, необходимо использовать 
//  значение TaskCreationOptions.AttachedToParent:

var outer2 = Task.Factory.StartNew(() =>
{
    Console.WriteLine("Outer2 start");
    var inner2 = Task.Factory.StartNew(() =>
    {
        Console.WriteLine("Inner start");
        Thread.Sleep(2000);
        Console.WriteLine("Inner end");
    },TaskCreationOptions.AttachedToParent);
});
outer2.Wait();
Console.WriteLine("End Main");
//  В данном случае вложенная задача прикреплена к внешней и выполняется как часть внешней задачи. И внешняя
//  задача завершится только когда завершатся все прикрепленные к ней вложенные задачи.
#endregion

#region Массив задач
//  Также как и с потоками, мы можем создать и запустить массив задач. Можно определить все задачи в массиве
//  непосредственно через объект Task:
Task[] tasks1 = new Task[3]
{
    new Task (() => Console.WriteLine("First Task")),
    new Task (() => Console.WriteLine("Second Task")),
    new Task (() => Console.WriteLine("Third Task"))
};
// запуск задач в массиве
foreach (Task task in tasks1)
{
    task.Start();
}

//  Либо также можно использовать методы Task.Factory.StartNew или Task.Run и сразу запускать все задачи:
Task[] task2 = new Task[3];
int j = 1;
for (int i = 0; i < task2.Length; i++)
{
    task2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));
}

//  Но в любом случае мы опять же можем столкнуться с тем, что все задачи из массива могут завершиться после
//  того, как отработает метод Main, в котором запускаются эти задачи:
Task[] tasks3 = new Task[3];
for (int i = 0; i < tasks3.Length; i++)
{
    tasks3[i] = new Task(() =>
    {
        Thread.Sleep(1000); // эмуляция долгой работы
        Console.WriteLine($"Task{i}: finished");
    });
    tasks3[i].Start();
}
//

//  Если необходимо завершить выполнение программы или вообще выполнять некоторый код лишь после того, как
//  все задачи из массива завершатся, то применяется метод Task.WaitAll(tasks):
Task.WaitAll(tasks3);
//  В этом случае сначала завершатся все задачи, и лишь только потом будет выполняться последующий код
//  из метода Main:

//  Также мы можем применять метод Task.WaitAny(tasks). Он ждет, пока завершится хотя бы одна из массива задач.
#endregion

#region Возвращение результатов из задач
//  Задачи могут не только выполняться как процедуры, но и возвращать определенные результаты:
int n1 = 4, n2 = 5;
Task<int> taskSum= new Task<int>(() => Sum(n1,n2));
//  Task<int> taskSum = new Task<int>(() => n1 + n2 );
taskSum.Start();

Console.WriteLine($"Result = {taskSum.Result}");

int Sum(int x, int y) => x + y;

//  Во - первых, чтобы получать из задачи не который результат, необходимо типизировать объект Task тем типом,
//  объект которого мы хотим получить из задачи. Например, в примере выше мы ожидаем из задачи sumTask получить
//  число типа int, соответственно типизируем объект Task данным типом - Task<int>.

//  И, во-вторых, в качестве задачи должен выполняться метод, который возвращает данный тип объекта. Так, в
//  данном случае у нас в качестве задачи выполняется метод Sum, которая принимаетдва числа и на выходе
//  возвращает их сумму - значение типа int.

//  Возвращаемое число будет храниться в свойстве Result: sumTask.Result.Нам не надо его приводить к типу int,
//  оно уже само по себе будет представлять число.
//  int result = sumTask.Result;
//  При этом при обращении к свойству Result текущий поток останавливает выполнение и ждет, когда будет
//  получен результат из выполняемой задачи.

//  Другой пример:
Task<Person> defaultPersonTask = new Task<Person>(() => new Person("Tom",37));
defaultPersonTask.Start();

Person person = defaultPersonTask.Result;
Console.WriteLine($"{person.Name} - {person.Age}");

record Person(string Name, int Age);
//  В данном случае задача defaultPersonTask возвращает объект типа Person, который мы можем получить из
//  свойства Result.
#endregion
