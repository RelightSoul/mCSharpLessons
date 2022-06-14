//  Передача параметров по ссылке и значению. Выходные параметры

#region Передача параметров по значению
//  Наиболее простой способ передачи параметров представляет передача по значению, по сути это обычный способ передачи параметров:
void Increment(int n)
{
    n++; 
    Console.WriteLine($"Число в методе Increment {n}");
}

int number = 5;
Console.WriteLine($"Число до метода Increment {number}");
Increment(number);
Console.WriteLine($"Число после метода Increment {number}");
//  При передаче аргументов параметрам по значению параметр метода получает не саму переменную, а ее копию и далее работает
//  с этой копией независимо от самой переменной.
#endregion

#region Передача параметров по ссылке
//  При передаче параметров по ссылке перед параметрами используется модификатор ref:
void Increment2(ref int n)
{
    n++;
    Console.WriteLine($"Число в методе Increment2 {n}");
}

int number2 = 5;
Console.WriteLine($"Число до метода Increment2 {number2}");
Increment2(ref number2);
Console.WriteLine($"Число после метода Increment2 {number2}");

//  При передаче значений параметрам по ссылке метод получает адрес переменной в памяти. И, таким образом, если в методе
//  изменяется значение параметра, передаваемого по ссылке, то также изменяется и значение переменной, которая передается
//  на его место.
#endregion

#region Выходные параметры. Модификатор out
//  Выше мы использовали входные параметры. Но параметры могут быть также выходными.
//  Чтобы сделать параметр выходным, перед ним ставится модификатор out:
void Sum2(int a, int b, out int result)
{
    result = a + b;
}
Sum2(4, 5, out int res);
Console.WriteLine(res);
//  Прелесть использования подобных параметров состоит в том, что по сути мы можем вернуть из метода не одно значение, а несколько.
void GetRectangleData
    (int wigth, int height, out int rectArea, out int rectPerimetr)    // можно использовать var, если тип значения не известен
{
    rectArea = wigth * height;
    rectPerimetr = (wigth + height) * 2;
}
GetRectangleData(14, 230, out int area, out int perimetr);
Console.WriteLine($"Area = {area}, Perimetr = {perimetr}");
#endregion

#region Входные параметры. Модификатор in
//  Кроме выходных параметров с модификатором out метод может использовать входные параметры с модификатором in. Модификатор in
//  указывает, что данный параметр будет передаваться в метод по ссылке, однако внутри метода его значение параметра нельзя будет изменить.
void GetRectangleData2
    (in int wigth,in int height, out int rectArea, out int rectPerimetr)    
{
    rectArea = wigth * height;
    rectPerimetr = (wigth + height) * 2;   
}
int w = 10;
int h = 20;
GetRectangleData2(w, h, out int area2, out int perimetr2);
Console.WriteLine($"Area = {area2}, Perimetr = {perimetr2}");
//  Передача по ссылке в некоторых случаях может увеличить произодительность, а использование оператора in,
//  гарантирует что переданные параметры переменн нельзя будет изменить в этом методе.
#endregion
