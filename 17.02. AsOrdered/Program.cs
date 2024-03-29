﻿// При выполнении параллельного запроса порядок данных в результирующей выборки может быть не предсказуем. 
int[] numbers = new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, };
var squares = from i in numbers.AsParallel()
              where i > 0
              select Square(i);
foreach (int i in squares)
{
    Console.WriteLine(i);
}
int Square(int n) => n * n;
//  В данном случае программа вычисляет квадраты чисел, которые больше 0. Однако в результате работы программы
//  мы можем получить неупорядоченный вывод. То есть данные склеиваются в общий набор неупорядоченно.

//  Если в запросе применяются операторы или методы сортировки в запросе, данные автоматически упорядочиваются:
var squares2 = from i in numbers.AsParallel()
               where i > 0
               orderby i
               select Square(i);
foreach (int i in squares2)
{
    Console.WriteLine(i);
}
//  Однако не всегда оператор orderby или метод OrderBy используются в запросах. Более того они упорядочивают
//  результирующую выборку в соответствии с результатами, а не в соответствии с исходной последовательностью.
//  В этих случаях мы может применять метод AsOrdered():
var squares3 = from i in numbers.AsParallel().AsOrdered()
               where i > 0
               select Square(i);
//  В этом случае результат уже будет упорядоченным в соответствии с тем, как элементы располагаются в исходной
//  последовательности. В то же время надо понимать, что упорядочивание в параллельной операции приводит к
//  увеличению издержек, поэтому подобный запрос будет выполняться медленнее, чем неупорядоченный. И если
//  задача не требует возвращение упорядоченного набора, то лучше не применять метод AsOrdered.

//  Кроме того, если в программе предстоят какие-нибудь манипуляции с полученным набором, однако упорядочивание
//  больше не требуется, мы можем применить метод AsUnordered():

// выбираем числа больще 4 без упорядочивания результата
var query = from n in squares.AsUnordered()
            where n > 4
            select n;
