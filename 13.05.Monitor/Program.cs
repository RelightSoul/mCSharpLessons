//  Мониторы

//  Наряду с оператором lock для синхронизации потоков мы можем использовать мониторы, представленные классом
//  System.Threading.Monitor. Для управления синхронизацией этот класс предоставляет следующите методы:

//  void Enter(object obj): получает в экслюзивное владение объект, передаваемый в качестве параметра.

//  void Enter(object obj, bool acquiredLock): дополнительно принимает второй параметра - логическое значение,
//  которое указывает, получено ли владение над объектом из первого параметра

//  void Exit(object obj): освобождает ранее захваченный объект

//  bool IsEntered(object obj): возвращает true, если монитор захватил объект obj

//  void Pulse (object obj): уведомляет поток из очереди ожидания, что текущий поток освободил объект obj

//  void PulseAll(object obj): уведомляет все потоки из очереди ожидания, что текущий поток освободил объект
//  obj. После чего один из потоков из очереди ожидания захватывает объект obj.

//  bool TryEnter (object obj): пытается захватить объект obj. Если владение над объектом успешно получено,
//  то возвращается значение true

//  bool Wait (object obj): освобождает блокировку объекта и переводит поток в очередь ожидания объекта.
//  Следующий поток в очереди готовности объекта блокирует данный объект. А все потоки, которые вызвали
//  метод Wait, остаются в очереди ожидания, пока не получат сигнала от метода Monitor.Pulse или
//  Monitor.PulseAll, посланного владельцем блокировки.

//  Стоит отметить, что фактически конструкция оператора lock инкапсулирует в себе синтаксис использования
//  мониторов. Например, в прошлой теме для синхронизации потоков применялся оператор lock:

//      int x = 0;
//      object locker = new();

//      for (int i = 1; i < 6; i++)
//      {
//           Thread tread = new Thread(Print);
//          tread.Name = $"P={i}";
//          tread.Start();
//      }

//      void Print()
//      {
//          lock (locker)
//          {
//              x = 1;
//              for (int i = 1; i < 6; i++)
//              {
//                  Console.WriteLine($"{Thread.CurrentThread.Name} - {x}");
//                  x++;
//                  Thread.Sleep(100);
//              }
//          }
//       }

//  Фактически данный пример будет эквивалентен следующему коду:
using System.Threading;

int x = 0;
object locker = new object();

for (int i = 1; i < 6; i++)
{
    Thread thread = new Thread(Print);
    thread.Name = $"Поток:{i}";
    thread.Start();
}

void Print()
{
    bool acLock = false;
    try
    {
        Monitor.Enter(locker,ref acLock);
        x = 1;
        for (int i = 1; i < 6; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
            x++;
            Thread.Sleep(100);
        }
    }
    finally
    {
        if (acLock)
        {
            Monitor.Exit(locker);
        }
    }
}