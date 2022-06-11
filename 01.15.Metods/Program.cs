// По сути метод - это именованный блок кода, который выполняет некоторые действия.

//  [модификаторы] тип_возвращаемого_значения название_метода([параметры])
//  {
//      // тело метода
//  }

#region Определение метода
void SayHello()      //определен метод SayHello, который выводит некоторое сообшение
{
    Console.WriteLine("Hello");
}
#endregion

#region Вызов метода
//  название_метода(значения_для_параметров_метода);

SayHello();          //Чтобы использовать метод SayHello, нам надо его вызвать
//Преимуществом методов является то, что их можно повторно и многократно вызывать в различных частях программы. 

void SayHelloRu()
{
    Console.WriteLine("Привет");
}
void SayHelloEn()
{
    Console.WriteLine("Hello");
}
void SayHelloFr()
{
    Console.WriteLine("Salut");
}

string language = "ru";   //en  //fr  //В конструкции switch проверяется значение переменной language

switch (language)
{
    case "en":
        SayHelloEn();
        break;
    case "ru":
        SayHelloRu();
        break;
    case "fr":
        SayHelloFr();
        break;
}
#endregion

#region Сокращение
// Если метод в качестве тела определяет только одну инструкцию, то мы можем сократить определение метода.
void SaySomething()
{
    Console.WriteLine("brbrbrbr");
}

void SaySomething2() => Console.WriteLine("hehehe");
#endregion