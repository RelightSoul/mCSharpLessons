﻿//  Класс AutoResetEvent

//  Класс AutoResetEvent также служит целям синхронизации потоков. Этот класс представляет событие синхронизации
//  потоков, который позволяет при получении сигнала переключить данный объект-событие из сигнального в
//  несигнальное состояние.

//  Для управления синхронизацией класс AutoResetEvent предоставляет ряд методов:

//  Reset(): задает несигнальное состояние объекта, блокируя потоки.

//  Set();: задает сигнальное состояние объекта, позволяя одному или несколким ожидающим потокам продолжить
//  работу.

//  WaitOne(): задает несигнальное состояние и блокирует текущий поток, пока текущий объект AutoResetEvent
//  не получит сигнал.

//  Событие синхронизации может находиться в сигнальном и несигнальном состоянии. Если состояние события
//  несигнальное, поток, который вызывает метод WaitOne, будет заблокирован, пока состояние события не
//  станет сигнальным. Метод Set, наоборот, задает сигнальное состояние события.

//  Так, в одной из предыдущих тем для синхронизации потоков применялся оператор lock:

//      int x = 0;
//      object locker = new();  // объект-заглушка
//      // запускаем пять потоков
//      for (int i = 1; i < 6; i++)
//      {
//           Thread myThread = new(Print);
//           myThread.Name = $"Поток {i}";
//          myThread.Start();
//      }


//      void Print()
//      {
//           lock (locker)
//           {
//               x = 1;
//               for (int i = 1; i < 6; i++)
//               {
//               Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
//               x++;
//               Thread.Sleep(100);
//               }
//           }
//       }

//  Перепишем этот пример с использованием AutoResetEvent:

using System.Threading;

int x = 0; //Общий ресурс

AutoResetEvent waitHandler = new AutoResetEvent(true);

for (int i = 1; i < 6; i++)
{
    Thread thread = new Thread(Print);
    thread.Name = $"Поток: {i}";
    thread.Start();
}

void Print()
{
    waitHandler.WaitOne();
    x = 1;
    for (int i = 1; i < 6; i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} - {x}");
        x++;
        Thread.Sleep(100);
    }
    waitHandler.Set();
}
//Во-первых, создаем переменную типа AutoResetEvent. Передавая в конструктор значение true, мы тем самым
//указываем, что создаваемый объект изначально будет в сигнальном состоянии.

//Когда начинает работать поток, то первым делом срабатывает определенный в методе Print вызов waitHandler.
//WaitOne(). Метод WaitOne указывает, что текущий поток переводится в состояние ожидания, пока объект
//waitHandler не будет переведен в сигнальное состояние. И так все потоки у нас переводятся в состояние
//ожидания.

//После завершения работы вызывается метод waitHandler.Set, который уведомляет все ожидающие потоки, что
//объект waitHandler снова находится в сигнальном состоянии, и один из потоков "захватывает" данный объект,
//переводит в несигнальное состояние и выполняет свой код. А остальные потоки снова ожидают.

//Так как в конструкторе AutoResetEvent мы указываем, что объект изначально находится в сигнальном состоянии,
//то первый из очереди потоков захватывает данный объект и начинает выполнять свой код.

//Но если бы мы написали AutoResetEvent waitHandler = new AutoResetEvent(false), тогда объект изначально
//был бы в несигнальном состоянии, а поскольку все потоки блокируются методом waitHandler.WaitOne() до
//ожидания сигнала, то у нас попросту случилась бы блокировка программы, и программа не выполняла бы никаких
//действий.

//Если у нас в программе используются несколько объектов AutoResetEvent, то мы можем использовать для
//отслеживания состояния этих объектов статические методы WaitAll и WaitAny, которые в качестве параметра
//принимают массив объектов класса WaitHandle - базового класса для AutoResetEvent.

//Так, мы тоже можем использовать WaitAll в вышеприведенном примере. Для этого надо строку

// waitHandler.WaitOne();
// заменить на следующую:
//AutoResetEvent.WaitAll(new WaitHandle[] {waitHandler});