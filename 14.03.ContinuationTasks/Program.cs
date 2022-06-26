//  Задачи продолжения

//  Задачи продолжения или continuation task позволяют определить задачи, которые выполняются после завершения
//  других задач. Благодаря этому мы можем вызвать после выполнения одной задачи несколько других, определить
//  условия их вызова, передать из предыдущей задачи в следующую некоторые данные.

//  Задачи продолжения похожи на методы обратного вызова, но фактически являются обычными задачами Task.
//  Посмотрим на примере:
using System.Threading.Tasks;

Task task1 = new Task(() =>
{
    Console.WriteLine($"Id задачи: {Task.CurrentId}");
});

Task task2 = task1.ContinueWith(PrintTask);   // задача продолжения - task2 выполняется после task1

task1.Start();
task2.Wait();   // ждем окончания второй задачи
Console.WriteLine("Конец метода Main");

void PrintTask(Task t)
{
    Console.WriteLine($"Id задачи: {Task.CurrentId}");
    Console.WriteLine($"Id предыдущей задачи: {t.Id}");
    Thread.Sleep(3000);
}
//  Первая задача задается с помощью лямбда-выражения, которое просто выводит id этой задачи. Вторая
//  задача - задача продолжения задается с помощью метода ContinueWith, который в качестве параметра
//  принимает делегат Action<Task>. То есть метод PrintTask, который передается в вызов ContinueWith,
//  должен принимать параметр типа Task.

//  Благодаря передачи в метод параметра Task, мы можем получить различные свойства предыдущей задачи,
//  как например, в данном случае получает ее Id.

//  И после завершения задачи task1 сразу будет вызываться задача task2.
Task<int> sumTask = new Task<int>(() => Sum(4,5));
// задача продолжения
Task printTask = sumTask.ContinueWith(task => PrintResult(task.Result));
//  Параметр task в лямбда-выражении фактически представляет задачу sumTask, из которой извлекается результат.
//  То есть является её резулдьтатом

sumTask.Start();
// ждем окончания второй задачи
printTask.Wait();
Console.WriteLine("Конец метода Main");

int Sum(int x, int y) => x + y;
void PrintResult(int sum) => Console.WriteLine(sum);
//  В данном случае задача sumTask выполняет метод Sum и возвращает его результат. Задача printTask
//  является задачей продолжения, выполняется сразу после sumTask и получает ее результат. Так, в вызове
//          Task printTask = sumTask.ContinueWith(task => PrintResult(task.Result));
//  Параметр task в лямбда-выражении фактически представляет задачу sumTask, из которой извлекается результат.

//  Подобным образом можно построить целую цепочку последовательно выполняющихся задач:
Task task11 = new Task(() => Console.WriteLine($"Current Task: {Task.CurrentId}"));

// задача продолжения
Task task22 = task11.ContinueWith(t =>
    Console.WriteLine($"Current Task: {Task.CurrentId}  Previous Task: {t.Id}"));

Task task33 = task22.ContinueWith(t =>
    Console.WriteLine($"Current Task: {Task.CurrentId}  Previous Task: {t.Id}"));


Task task44 = task33.ContinueWith(t =>
    Console.WriteLine($"Current Task: {Task.CurrentId}  Previous Task: {t.Id}"));

task11.Start();

task44.Wait();   //  ждем завершения последней задачи
Console.WriteLine("Конец метода Main");