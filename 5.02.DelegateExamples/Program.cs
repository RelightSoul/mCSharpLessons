// Применение делегатов

using System;

public delegate void AccountHandler(string message);  //объявляем делегат
public class Account
{
    int sum;
    AccountHandler? taken;                            //Создаём переменную делегата
    public Account(int sum) => this.sum = sum;
    public void RegisterHandler(AccountHandler del)   //Регистрируем делегат
    {
        taken = del;
    }
    public void Add(int sum) => this.sum += sum;
    public void Take(int sum)
    {
        if (this.sum >= sum)
        {
            this.sum -= sum;
            taken?.Invoke($"Со счёта списано {sum} y.e"); // // вызываем делегат, передавая ему сообщение
        }
        else
        {
            taken?.Invoke($"Недостаточно средств, текущий баланс {this.sum} y.e");
        }
    }
}
//  Поскольку делегат AccountHandler в качестве параметра принимает строку, то при вызове
//  переменной taken() мы можем передать в этот вызов конкретное сообщение. В зависимости
//  от того, произошло снятие денег или нет, в вызов делегата передаются разные сообщения.

//  То есть фактически вместо делегата будут выполняться действия, которые переданы делегату
//  в методе RegisterHandler. Причем опять же подчеркну, при вызове делегата мы не значем,
//  что это будут действия. Здесь мы только передаем в эти действия сообщение об успешно или
//  неудачном снятии.

class Program
{
    static void Main(string[] args)
    {
        Account account = new Account(200);
        account.RegisterHandler(PrintSimpleMessage);
        account.Take(100);
        account.Take(150);
        Console.WriteLine();

        Account2 account2 = new Account2(200);
        account2.RegisterHandler(PrintSimpleMessage);
        account2.RegisterHandler(PrintColorMessage);
        account2.Take(100);
        account2.Take(150);
        account2.UnregisterHandler(PrintColorMessage);
        account.Take(50);

    }
    static void PrintSimpleMessage(string message) => Console.WriteLine(message);
    static void PrintColorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
//  Здесь через метод RegisterHandler переменной taken в классе Account передается ссылка на
//  метод PrintSimpleMessage. Этот метод соответствует делегату AccountHandler. Соответственно
//  там, где вызывается делегат taken в методе Account, в реальности будет выполняться метод
//  PrintSimpleMessage.

//  Через параметр message метод PrintSimpleMessage получит переданное из делегата сообщение
//  и выведет его на консоль:

//  В результате, если мы создаем консольное приложение, мы можем через делегат выводить сообщение
//  на консоль. Если мы создаем графическое приложение Windows Forms или WPF, то можно выводить
//  сообщение в виде графического окна. А можно не просто выводить сообщение. А, например,
//  записать при списании информацию об этом действии в файл или отправить уведомление на
//  электронную почту. В общем любыми способами обработать вызов делегата. И способ обработки
//  не будет зависеть от класса Account.

#region Добавление и удаление методов в делегате
//  Хотя в примере наш делегат принимал адрес на один метод, в действительности он может указывать
//  сразу на несколько методов. Кроме того, при необходимости мы можем удалить ссылки на адреса
//  определенных методов, чтобы они не вызывались при вызове делегата. Итак, изменим в классе
//  Account2 метод RegisterHandler и добавим новый метод UnregisterHandler, который будет удалять
//  методы из списка методов делегата:

public class Account2
{
    int sum;
    AccountHandler? taken;                            
    public Account2(int sum) => this.sum = sum;
    public void RegisterHandler(AccountHandler del)  
    {
        taken += del;
    }
    public void UnregisterHandler(AccountHandler del)
    {
        taken -= del;  // удаляем делегат
    }
    public void Add(int sum) => this.sum += sum;
    public void Take(int sum)
    {
        if (this.sum >= sum)
        {
            this.sum -= sum;
            taken?.Invoke($"Со счёта списано {sum} y.e");
        }
        else
        {
            taken?.Invoke($"Недостаточно средств, текущий баланс {this.sum} y.e");
        }
    }
}
//  В первом методе объединяет делегаты taken и del в один, который потом
//  присваивается переменной taken. Во втором методе из переменной taken удаляется делегат del.

//  Рассмотрим пример в классе Programm выше
#endregion