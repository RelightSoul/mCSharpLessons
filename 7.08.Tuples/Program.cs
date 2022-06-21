// Кортежи предоставляют удобный способ для работы с набором значений, который был добавлен в версии C# 7.0.

//  Кортеж представляет набор значений, заключенных в круглые скобки:
var tuple = (5, 10);
//  В данном случае определен кортеж tuple, который имеет два значения: 5 и 10.В дальнейшем мы можем обращаться
//  к каждому из этих значений через поля с названиями
Console.WriteLine(tuple.Item1);  //5 
Console.WriteLine(tuple.Item2);  //10
tuple.Item1 += 26;
Console.WriteLine(tuple.Item1);  //31
//  В данном случае тип определяется неявно. Но мы также можем явным образом указать для переменной кортежа тип:
(int, int) tuple2 = (5, 10);
//  Так как кортеж содержит два числа, то в определении типа нам надо указать два числовых типа. Или другой
//  пример определения кортежа:
(string, int, double) person = ("Tom", 25, 81.23);

//  Мы также можем дать названия полям кортежа:
var tuple3 = (count: 5, sum: 10);
Console.WriteLine(tuple3.count);
//  Теперь чтобы обратиться к полям кортежа используются их имена, а не названия Item1 и Item2.

//  Одной из задач, которую позволяет элегантно решить кортеж - это обмен значениями:
string first = "Java";
string second = "C#";
(first, second) = (second, first);
Console.WriteLine(first);   //C#
Console.WriteLine(second);  //Java

//  Можно использовать, например, при простейшей сортировке массива:
int[] nums = { 54, 7, -41, 2, 4, 2, 89, 33, -5, 12 };
for (int i = 0; i < nums.Length - 1 ; i++)
{
    for (int j = i + 1 ; j < nums.Length; j++)
    {
        if (nums[i] > nums[j])
        {
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }
    }
}
foreach (int i in nums)
{
    Console.Write("{0}\t",i);
}
Console.WriteLine("\n---------------------");

#region Кортеж как результат метода
//  Кортежи могут выступать в качестве результата функции. Например, одной из распространенных ситуаций является
//  возвращение из функции двух и более значений, в то время как функция может возвращать только одно значение.
//  И кортежи представляют оптимальный способ для решения этой задачи:
var tuple4 = GetValue();
Console.WriteLine(tuple4.Item1);
Console.WriteLine(tuple4.Item2);

(int,int) GetValue()
{
    var result = (1, 7);
    return result;
}
Console.WriteLine("---------------------");
//  Здесь определен метод GetValues(), который возвращает кортеж. Кортеж определяется как набор значений,
//  помещенных в круглые скобки. И в данном случае мы возвращаем кортеж из двух элементов типа int, то есть
//  два числа.

//  Другой пример:
var tuple6 = GetValueArray(new int[] { 1, 2, 3, 4, 5, 6, 7 });
Console.WriteLine(tuple6.count);
Console.WriteLine(tuple6.sum);

(int sum, int count) GetValueArray(int[] numbers)
{
    var result = (sum: 0, count: numbers.Length);
    foreach (int i in numbers)
    {
        result.sum += i;
    }
    return result;
}
#endregion

#region Кортеж как параметр метода
//  И также кортеж может передаваться в качестве параметра в метод:
PrintPerson(("Alex", 33));
void PrintPerson((string name, int age) person)
{
    Console.WriteLine($"{person.name}, {person.age}");
}
#endregion
