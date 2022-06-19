// События сигнализируют системе о том, что произошло определенное действие.
// И если нам надо отследить эти действия, то как раз мы можем применять события.

#region Определение и вызов событий
//  События объявляются в классе с помощью ключевого слова event, после которого указывается
//  тип делегата, который представляет событие:

//  delegate void AccountHandler(string message);
//  event AccountHandler Notify;

//  В данном случае вначале определяется делегат AccountHandler, который принимает один параметр типа string.
//  Затем с помощью ключевого слова event определяется событие с именем Notify, которое представляет делегат
//  AccountHandler. Название для события может быть произвольным, но в любом случае оно должно представлять
//  некоторый делегат.

//  Определив событие, мы можем его вызвать в программе как метод, используя имя события:

//  Notify("Произошло действие");

//  Поскольку событие Notify представляет делегат AccountHandler, который принимает один параметр типа string
//  - строку, то при вызове события нам надо передать в него строку.

//  Однако при вызове событий мы можем столкнуться с тем, что событие равно null в случае, если для его
//  не определен обработчик. Поэтому при вызове события лучше его всегда проверять на null. Например, так:

//  if(Notify !=null) Notify("Произошло действие");
//  или
//  Notify?.Invoke("Произошло действие");

//  В этом случае поскольку событие представляет делегат, то мы можем его вызвать с помощью метода Invoke(),
//  передав в него необходимые значения для параметров.

//  Объединим все вместе и создадим и вызовем событие:

class Account
{
    public delegate void AccountHandler(string message);
    public event AccountHandler? Notify;                     // Определение события
    public int Sum { get; private set; }
    public Account(int sum) => Sum = sum;
    public void Put(int sum)
    {
        Sum += sum;
        Notify?.Invoke($"На чёт поступило: {sum}");
    }
    public void Take(int sum)
    {
        if (sum <= Sum)
        {
            Sum -= sum;
            Notify?.Invoke($"Со счёта снято: {sum}");
        }
        else
        {
            Notify?.Invoke($"На счету недостаточно средств, баланс: {Sum}");
        }
    }
}
//  Теперь с помощью события Notify мы уведомляем систему о том, что были добавлены средства
//  и о том, что средства сняты со счета или на счете недостаточно средств.
#endregion

#region Добавление обработчика события
//  С событием может быть связан один или несколько обработчиков. Обработчики событий - это именно то,
//  что выполняется при вызове событий. Нередко в качестве обработчиков событий применяются методы.
//  Каждый обработчик событий по списку параметров и возвращаемому типу должен соответствовать делегату,
//  который представляет событие. Для добавления обработчика события применяется операция +=:

//  Notify += обработчик события;

class Program
{
    static void Main(string[] args)
    {
        Account account = new Account(100);
        account.Notify += DisplayMessage;       // добавляем обработчик DisplayMessage для события Notify
        account.Notify += DisplayRedMessage;    // добавляем обработчик DisplayRedMessage для события Notify
        account.Put(20);
        account.Take(70);
        account.Notify -= DisplayRedMessage;    // удаляем обработчик DisplayRedMessage для события Notify
        account.Take(180);

        //установка делегата, который указывает на метод DisplayMessage
        account.Notify += new Account.AccountHandler(DisplayMessage);

        //Установка в качестве обработчика анонимного метода:
        account.Notify += delegate (string mes)
        {
            Console.WriteLine(mes);
        };

        //Установка в качестве обработчика лямбда-выражения:
        account.Notify += mes => Console.WriteLine(mes); 

        void DisplayMessage(string message) => Console.WriteLine(message);
        void DisplayRedMessage(string message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }
        Console.WriteLine();

        Account2 newAcc = new Account2(100);
        newAcc.Notify += DisplayMessage;   // добавляем обработчик DisplayMessage
        newAcc.Put(20);  // добавляем на счет 20
        newAcc.Notify -= DisplayMessage;   // удаляем обработчик DisplayRedMessage
        newAcc.Put(20);  // добавляем на счет 20
        Console.WriteLine();

        Account3 acc3 = new Account3(100);
        acc3.Notify += DisplayMessage2;
        acc3.Put(20);
        acc3.Take(70);
        acc3.Take(150);    

        void DisplayMessage2(Account3 sender, AccountEventArgs arg)
        {
            Console.WriteLine($"Сумма транзакции {arg.Sum}");
            Console.WriteLine(arg.Message);
            Console.WriteLine($"Текущая сумма на счёте: {sender.Sum}");
        }
    }    
}
//  В данном случае в качестве обработчика используется метод DisplayMessage, который соответствует
//  по списку параметров и возвращаемому типу делегату AccountHandler. В итоге при вызове события
//  Notify?.Invoke() будет вызываться метод DisplayMessage, которому для параметра message будет
//  передаваться строка, которая передается в Notify?.Invoke(). В DisplayMessage просто выводим
//  полученное от события сообщение, но можно было бы определить любую логику.

//  Если бы в данном случае обработчик не был бы установлен, то при вызове события Notify?.Invoke()
//  ничего не происходило, так как событие Notify было бы равно null.
#endregion

#region Добавление и удаление обработчиков
//  Для одного события можно установить несколько обработчиков и потом в любой момент времени их удалить.
//  Для удаления обработчиков применяется операция -=.

//  В качестве обработчиков могут использоваться не только обычные методы, но также делегаты,
//  анонимные методы и лямбда-выражения.

//  Рассмотрим примеры в классе программ выше:
#endregion

#region Управление обработчиками
//  С помощью специальных акссесоров add/remove мы можем управлять добавлением и удалением обработчиков.
//  Как правило, подобная функциональность редко требуется, но тем не менее мы ее можем использовать. Например:

class Account2
{
    public delegate void AccountHandler(string message);
    AccountHandler? notify;
    public event AccountHandler? Notify
    {
        add 
        {
            notify += value;
            Console.WriteLine($"{value?.Method.Name} добавлен");
        }
        remove
        {
            notify -= value;
            Console.WriteLine($"{value?.Method.Name} удален");
        }
    }
    public int Sum { get; set; }
    public Account2( int sum) => Sum = sum;
    public void Put(int sum)
    {
        Sum += sum;
        notify?.Invoke($"На счет поступило: {sum}");   // 2.Вызов события 
    }
    public void Take(int sum)
    {
        if (Sum >= sum)
        {
            Sum -= sum;
            notify?.Invoke($"Со счета снято: {sum}");   // 2.Вызов события
        }
        else
        {
            notify?.Invoke($"Недостаточно денег на счете. Текущий баланс: {Sum}"); ;
        }
    }
}
//  Теперь опредление события разбивается на две части. Вначале просто определяется переменная делегата,
//  через которую мы можем вызывать связанные обработчики:

//      AccountHandler notify;

//  Во второй части определяем акссесоры add и remove. Аксессор add вызывается при добавлении обработчика,
//  то есть при операции +=. Добавляемый обработчик доступен через ключевое слово value. Здесь мы можем
//  получить информацию об обработчике (например, имя метода через value.Method.Name) и определить некоторую
//  логику. В данном случае для простоты просто выводится сообщение на консоль. Блок remove вызывается при
//  удалении обработчика. Аналогично здесь можно задать некоторую дополнительную логику.

//  Внутри класса событие вызывается также через переменную notify. Но для добавления и удаления обработчиков
//  в программе используется Notify. Рассмотрим пример в классе программ выше.
#endregion

#region Передача данных события
//  Нередко при возникновении события обработчику события требуется передать некоторую информацию о событии.
//  Например, добавим и в нашу программу новый класс AccountEventArgs со следующим кодом:
class AccountEventArgs
{
    public string Message { get; }
    public int Sum { get; }
    public AccountEventArgs(string message, int sum)
    {
        Message = message;
        Sum = sum;  
    }
}
//  Данный класс имеет два свойства: Message - для хранения выводимого сообщения и Sum - для хранения суммы,
//  на которую изменился счет.

//  Теперь применим класс AccoutEventArgs, изменив класс Account3 следующим образом:
class Account3
{
    public delegate void AccountHandler(Account3 sender, AccountEventArgs e);
    public event AccountHandler? Notify;                     // Определение события
    public int Sum { get; private set; }
    public Account3(int sum) => Sum = sum;
    public void Put(int sum)
    {
        Sum += sum;
        Notify?.Invoke(this, new AccountEventArgs($"На счёт поступило: {sum}", sum));
    }
    public void Take(int sum)
    {
        if (sum <= Sum)
        {
            Sum -= sum;
            Notify?.Invoke(this, new AccountEventArgs($"Со счёта снято: {sum}", sum));
        }
        else
        {
            Notify?.Invoke(this, new AccountEventArgs($"На счету недостаточно средств, баланс: {Sum}", sum));
        }
    }
}
//  По сравнению с предыдущей версией класса Account здесь изменилось только количество параметров у делегата и
//  соответственно количество параметров при вызове события. Теперь делегат AccountHandler в качестве первого
//  параметра принимает объект, который вызвал событие, то есть текущий объект Account. А в качестве второго параметра
//  принимает объект AccountEventArgs, который хранит информацию о событии, получаемую через конструктор.

//  Дальнейший разбор в программ, выше.
#endregion

//  Отличия делегатов от событий:
//  1.События потокобезопасны, если не использовать add\remove.
//  2. Делегаты можно вызвать откуда угодно, события только внутри класса, в котором они определены.
//  3. Событию нельзя присвоить значения, на него можно только подписаться (+=) или отписаться(-=).