// Типы исключений. Класс Exception

//  Базовым для всех типов исключений является тип Exception. Этот тип определяет ряд свойств,
//  с помощью которых можно получить информацию об исключении.

//  InnerException: хранит информацию об исключении, которое послужило причиной текущего исключения

//  Message: хранит сообщение об исключении, текст ошибки

//  Source: хранит имя объекта или сборки, которое вызвало исключение

//  StackTrace: возвращает строковое представление стека вызывов, которые привели к возникновению исключения

//  TargetSite: возвращает метод, в котором и было вызвано исключение

try
{
    int x = 5;
    int y = x / 0;
    Console.WriteLine(y);
}
catch (Exception ex)
{ 
    Console.WriteLine(ex.Message);      // Сообщение об исключение
    Console.WriteLine(ex.TargetSite);   // Метод
    Console.WriteLine(ex.StackTrace);   // Трассировка стека
}

//  Также есть более специализированные типы исключений, которые предназначены
//  для обработки каких-то определенных видов исключений. Их довольно много,
//  тут лишь некоторые:

//DivideByZeroException: представляет исключение, которое генерируется при делении на ноль

//ArgumentOutOfRangeException: генерируется, если значение аргумента находится вне диапазона
//допустимых значений

//ArgumentException: генерируется, если в метод для параметра передается некорректное значение

//IndexOutOfRangeException: генерируется, если индекс элемента массива или коллекции находится
//вне диапазона допустимых значений

//InvalidCastException: генерируется при попытке произвести недопустимые преобразования типов

//NullReferenceException: генерируется при попытке обращения к объекту, который равен null
//(то есть по сути неопределен)

//  при необходимости мы можем разграничить обработку различных типов исключений, включив
//  дополнительные блоки catch:
try
{
    int[] numbers = new int[4];
    numbers[7] = 9;     // IndexOutOfRangeException

    int x1 = 5;
    int y1 = x1 / 0;  // DivideByZeroException
    Console.WriteLine($"Результат: {y1}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Возникло исключение DivideByZeroException");
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine(ex.Message);
}

//  Другой пример
try
{
    object obj = "you";
    int num = (int)obj;     // System.InvalidCastException
    Console.WriteLine($"Результат: {num}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Возникло исключение DivideByZeroException");
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("Возникло исключение IndexOutOfRangeException");
}
catch (Exception ex)
{
    Console.WriteLine($"Исключение: {ex.Message}");
}

//  И в данном случае блок catch (Exception ex){} будет обрабатывать все исключения кроме
//  DivideByZeroException и IndexOutOfRangeException. При этом блоки catch для более общих,
//  более базовых исключений следует помещать в конце - после блоков catch для более конкретный,
//  специализированных типов. Так как CLR выбирает для обработки исключения первый блок catch,
//  который соответствует типу сгенерированного исключения. Поэтому в данном случае сначала
//  обрабатывается исключение DivideByZeroException и IndexOutOfRangeException, и только потом Exception
//  (так как DivideByZeroException и IndexOutOfRangeException наследуется от класса Exception).