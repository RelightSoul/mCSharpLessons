// Делегаты представляют такие объекты, которые указывают на методы.
// То есть делегаты - это указатели на методы и с помощью делегатов мы можем вызвать данные методы.

#region Определение делегатов
//  Для объявления делегата используется ключевое слово delegate, после которого идет
//  возвращаемый тип, название и параметры. Например:

Message mes;              // 2 Создаём переменную делегата
mes = HelloAll;              // 3 Приваиваем адрес метода, переменной
mes();                    // 4 Вызываем метод

void HelloAll() => Console.WriteLine("Hello all");

//delegate void Message();  // 1 Объвляем делегат

//  При этом делегаты необязательно могут указывать только на методы, которые определены
//  в том же классе, где определена переменная делегата. Это могут быть также методы из
//  других классов и структур.
Message message1 = Welcome.Print;
Message message2 = new Hello().Display;

message1();
message2();
Console.WriteLine();
#endregion

#region Место определения делегата
//  Если мы определяем делегат в прогаммах верхнего уровня (top-level program),
//  которую по умолчанию представляет файл Program.cs начиная с версии C# 10, как в
//  примере выше, то, как и другие типы, делегат определяется в конце кода. Но в принцие
//  делегат можно определять внутри класса:

//      class Program
//      {
//          delegate void Message(); // 1. Объявляем делегат
//          static void Main()
//          {
//              Message mes;            // 2. Создаем переменную делегата
//              mes = Hello;            // 3. Присваиваем этой переменной адрес метода
//              mes();                  // 4. Вызываем метод

//              void Hello() => Console.WriteLine("Hello METANIT.COM");
//           }
//      }

//  Либо вне класса:
#endregion

#region Параметры и результат делегата
//  Рассмотрим определение и применение делегата, который принимает параметры и возвращает результат:
Operation oper = Add;
int result = oper(4, 5);
Console.WriteLine(result);   //9

oper = Multiply;
result = oper(4, 5);
Console.WriteLine(result);  //20

int Add(int x, int y) => x + y;
int Multiply(int x, int y) => x * y;
//delegate int Operation(int x, int y);

//В данном случае делегат Operation возвращает значение типа int и имеет два параметра типа int.
//Поэтому этому делегату соответствует любой метод, который возвращает значение типа int и
//принимает два параметра типа int. В данном случае это методы Add и Multiply. То есть мы можем
//присвоить переменной делегата любой из этих методов и вызывать.
#endregion

#region Присвоение ссылки на метод
//  Выше переменной делегата напрямую присваивался метод. Есть еще один способ - создание объекта
//  делегата с помощью конструктора, в который передается нужный метод:
Operation operX = Add;
Operation operY = new Operation(Add);
Console.WriteLine();

//  Оба способа равноценны
#endregion

#region Соответствие методов делегату
//  Как было написано выше, методы соответствуют делегату, если они имеют один и тот же
//  возвращаемый тип и один и тот же набор параметров. Но надо учитывать, что во внимание
//  также принимаются модификаторы ref, in и out, порядок параметров!!
#endregion

#region Добавление методов в делегат
//  В примерах выше переменная делегата указывала на один метод. В реальности же делегат может
//  указывать на множество методов, которые имеют ту же сигнатуру и возвращаемые тип. Все
//  методы в делегате попадают в специальный список - список вызова или invocation list. И
//  при вызове делегата все методы из этого списка последовательно вызываются. И мы можем
//  добавлять в этот список не один, а несколько методов. Для добавления методов в делегат
//  применяется операция +=:
Message? newMess = HelloRu;
newMess += HelloEn;
newMess += HowAreYou;
newMess();


void HelloRu() => Console.WriteLine("Привет");
void HelloEn() => Console.WriteLine("Hello");
void HowAreYou() => Console.WriteLine("How are you?");
Console.WriteLine();
//стоит отметить, что в реальности будет происходить создание нового объекта делегата,
//который получит методы старой копии делегата и новый метод, и новый созданный объект
//делегата будет присвоен переменной message.

//  Подобным образом мы можем удалять методы из делегата с помощью операций -=:
newMess -= HelloEn;
if (newMess != null) newMess();
//  При удалении методов из делегата фактически будет создаватья новый делегат, который
//  в списке вызова методов будет содержать на один метод меньше.

//  При удалении следует учитывать, что если делегат содержит несколько ссылок на один и
//  тот же метод, то операция -= начинает поиск с конца списка вызова делегата и удаляет
//  только первое найденное вхождение. Если подобного метода в списке вызова делегата нет,
//  то операция -= не имеет никакого эффекта.
#endregion

#region Объединение делегатов
//  Делегаты можно объединять в другие делегаты. Например:
Message newMess2 = HelloRu;
Message newMess3 = newMess + newMess2;  // объединяем делегаты
newMess3();             //вызываются все методы из mes1 и mes2
Console.WriteLine();
#endregion

#region Вызов делегата
//  В примерах выше делегат вызывался как обычный метод. Если делегат принимал параметры,
//  то при его вызове для параметров передавались необходимые значения:
Operation opHere = Add;
Console.WriteLine(opHere(4, 5));

//  Другой способ вызова делегата представляет метод Invoke():
Console.WriteLine(opHere.Invoke(4, 5));
//  Если делегат принимает параметры, то в метод Invoke передаются значения для этих параметров.

//Следует учитывать, что если делегат пуст, то есть в его списке вызова нет ссылок ни на один из
//методов (то есть делегат равен Null), то при вызове такого делегата мы получим исключение, как,
//например, в следующем случае:
Message? mesXxX = null;
//mesXxX();  // ! Ошибка: делегат равен null

mesXxX?.Invoke();    // ошибки нет, делегат просто не вызывается

Operation? op33 = null;
int? n = op33?.Invoke(4,5);   // ошибки нет, делегат просто не вызывается, а n = null
Console.WriteLine();

//Если делегат возвращает некоторое значение, то возвращается значение последнего метода из списка
//вызова (если в списке вызова несколько методов). Например:
Operation? op34 = Multiply2;
op34 += Subtract2;
op34 += Add2;
Console.WriteLine(op34.Invoke(4, 5));  //возвращает только от последнего

int Add2(int x, int y) => x + y;
int Subtract2(int x, int y) => x - y;
int Multiply2(int x, int y) => x * y;
Console.WriteLine();
#endregion

#region Обобщенные делегаты
//  Делегаты, как и другие типы, могут быть обобщенными, например:
Opera<decimal, int> bigSquare = Square;
decimal x = bigSquare(777);
Console.WriteLine(x);

Opera<int, int> doDouble = Double;
Console.WriteLine(doDouble(33));

decimal Square(int x) => x * x;
int Double(int x) => x + x;
Console.WriteLine();

// delegate T Opera<T, K>(K val);

//  Здесь делегат Operation типизируется двумя параметрами типов. Параметр T представляет
//  тип возвращаемого значения. А параметр K представляет тип передаваемого в делегат
//  параметра. Таким образом, этому делегату соответствует метод, который принимает параметр
//  любого типа и возвращает значение любого типа.
#endregion

#region Делегаты как параметры методов
//  Также делегаты могут быть параметрами методов. Благодаря этому один метод в качестве
//  параметров может получать действия - другие методы. Например:
DoOperation(4, 5, Add3);
DoOperation(4, 5, Subtract3);
DoOperation(4, 5, Multiply3);

void DoOperation(int x, int y, Operation op)
{
    Console.WriteLine(op(x,y));
}

int Add3(int x, int y) => x + y;
int Subtract3(int x, int y) => x - y;
int Multiply3(int x, int y) => x * y;
Console.WriteLine();
//  При вызове метода DoOperation мы можем передать в него в качестве третьего параметра метод,
//  который соответствует делегату Operation.
#endregion

#region Возвращение делегатов из метода
//  Также делегаты можно возвращать из методов. То есть мы можем возвращать из метода
//  какое-то действие в виде другого метода.
Operation SelectOperation(OperationType opType) => opType switch
{
        OperationType.Add => _Add,
        OperationType.Subtract => _Subtract,
        _ => _Multiply
};
// Другой вид записи, того же switch
//switch (opType)
//{
//    case OperationType.Add: return Add;
//    case OperationType.Subtract: return Subtract;
//    default: return Multiply;
//}

int _Add(int x, int y) => x + y;
int _Subtract(int x, int y) => x - y;
int _Multiply(int x, int y) => x * y;
#endregion

#region Конец кода
enum OperationType
{
    Add, Subtract, Multiply
}
delegate T Opera<T, K>(K val);
delegate void Message();
delegate int Operation(int x, int y);
class Welcome
{
    public static void Print() => Console.WriteLine("Welcome");
}
class Hello
{
    public void Display() => Console.WriteLine("Hello");
}
//  При удалении методов из делегата фактически будет создаватья новый делегат,
//  который в списке вызова методов будет содержать на один метод меньше.

#endregion

