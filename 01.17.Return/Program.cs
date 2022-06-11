// Возвращение значения и оператор return

// Методы могут возвращать некоторое значение. Для этого применяется оператор return, после которого идет возвращаемое значение:

//      return возвращаемое значение;

string GetMessage()      //Метод GetMessage имеет тип string, следовательно, он должен возвратить строку.
{
    return "Hello";
}

//Методы, которые в качестве возвращаемого типа имеют любой тип, кроме void, обязательно должны
//использовать оператор return для возвращения значения.

// Результат методов, который возвращают значение, мы можем присвоить переменным или использовать иным образом в программе:
string message = GetMessage();
Console.WriteLine(message);
void PrintMess(string mess)
{
    Console.WriteLine(mess);
}
PrintMess(GetMessage());  // GetMessage возвращает string, передал методу который принимает string

#region Сокращение (синтетический сахар:))
string GoToGym()
{
    return "On my way";
}
//аналогичен
string GoToGym2() => "On my way";

int Sum(int x, int y)
{
    return x + y;
}
//аналогичен
int Sum2(int x, int y) => x + y;
#endregion

#region Выход из метода
//Оператор return не только возвращает значение, но и производит выход из метода.
//Поэтому он должен определяться после остальных инструкций. 

// мы можем использовать оператор return и в методах с типом void. В этом случае после
// оператора return не ставится никакого возвращаемого значения
void PrintOldPerson(string name, int age)
{
    if (age > 120 || age < 1)
    {
        Console.WriteLine("Недопустимый возраст");
        return;
    }
    Console.WriteLine($"Name = {name}, Age = {age}");
}
PrintOldPerson("Bill", 90);
PrintOldPerson("Colin", 124);
#endregion