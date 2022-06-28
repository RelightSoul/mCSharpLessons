//  Делегаты в запросах LINQ

//  Если мы обратимся к определению многих методов расширений LINQ, то увидим, что в качестве параметра
//  многие из них принимают делегаты например, Func<TSource, bool>, например, определение метода Where:
//public static IEnumerable<TSource> Where<TSource>(
//    this IEnumerable<TSource> source,
//    Func<TSource, bool> predicate
//)

//  Хотя, как правило, в качестве делегата в подобные методы удобно передавать лямбда-выражения. Но тем
//  не менее мы также можем передать полноценные методы. Например:
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };

var result = people.Where(LenghtIs3);

foreach (var person in result)
    Console.WriteLine(person);

bool LenghtIs3(string name) => name.Length == 3;
//  Здесь метод LenghtIs3 проверяет, равна ли длина строки 3 символам. Так как в данном случае набор элементов,
//  к которому применяется метод Where, содержит объекты string, то в метод в качестве параметра передается
//  объект этого типа. Возвращаемый тип должен представлять тип bool: если true, то объект string соответствует
//  условию и попадает в выходную коллекцию.

//  Рассмотрим другой пример. Пусть метод Select() применяется к коллекции целых чисел и преобразует каждое
//  число в его квадрат:

int[] numbers = { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 };
var result2 = numbers.Where(numb => numb > 0).Select(Square);

foreach (int num in result2)
{
    Console.WriteLine(num);
}

int Square(int n) => n * n;
//  Метод Select в качестве параметра принимает тип Func<TSource, TResult> selector. Так как у нас набор объектов
//  int, то входным параметром делегата также будет объект типа int. В качестве типа выходного параметра выберем
//  int, так как здесь квадрат числа - это целочисленное значение.