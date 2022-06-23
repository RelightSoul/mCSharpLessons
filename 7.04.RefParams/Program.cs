﻿// Переменные-ссылки и возвращение ссылки

//  Кроме параметров метода, которые с помощью модификатора ref позволяют передавать значение по ссылке,
//  C# также позволяет с помощью ключевого слова ref возвращать ссылку из метода и определять переменную,
//  которая будет хранить ссылку.

#region Переменная-ссылка
//  Для определения локальной переменной-ссылки (ref local) перед ее типом ставится ключевое слово ref:

//Здесь переменная xRef указывает не просто на значение переменной x, а на область в памяти, где располагается
//эта переменная. Для этого перед x также указывается ref.
int x = 5;
ref int xRef = ref x;
//  Получив ссылку, мы можем манипулировать значением по этой ссылке. Например:
Console.WriteLine(x);  //5
xRef = 125;
Console.WriteLine(x);  //125
x = 625;
Console.WriteLine(xRef);  //625
#endregion

#region Ссылка как результат функции
//  Для возвращения из функции ссылки в сигнатуре функции перед возвращаемым типом, а также после оператора
//  return следует указать ключевое слово ref:

int[] numbers = {1,2,3,4,5,6,7 };

//  В методе Main для определения переменной, которая будет содержать ссылку, используется ключевое слово ref.
//  При вызове метода также указывается слово ref:
ref int numberRef = ref Find(4,numbers);
numberRef = 9;
Console.WriteLine(numbers[3]);  //9

ref int Find(int number, int[] numbers)
{
    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] == number)
        {
            return ref numbers[i]; // возвращаем ссылку на адрес, а не само значение
        }
    }
    throw new IndexOutOfRangeException("Не найдено число");
}
//  В итоге переменная numberRef будет содержать ссылку на объект int, и через данную переменную в последствиии
//  мы можем изменить объект по этой ссылке.

//  Другой пример - возвращение ссылки на максимальное число из двух:
int a = 5;
int b = 8;
ref int pointer = ref Max(ref a, ref b);
Console.WriteLine(pointer);  //8
pointer = 34;
Console.WriteLine($"{b}");   //34

ref int Max(ref int n1, ref int n2)
{
    if (n1 > n2)
    {
        return ref n1;
    }
    else
    {
        return ref n2;
    }
}

//При определении метода, который возвращает ссылку, следует учитывать, что такой метод естественно не может
//иметь тип void. Кроме того, такой метод не может возвращать:

//Значение null

//Константу

//Значение перечисления enum

//Свойство класса или структуры

//Поле для чтения (которое имеет модификатор read-only)
#endregion
