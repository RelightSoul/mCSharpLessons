//  Локальные функции представляют функции, определенные внутри других методов. Локальная функция, как правило,
//  содержит действия, которые применяются только в рамках ее метода

#region Локальные функции
void Compare(int[] numbers1, int[] numbers2)
{
    int numbers1Sum = 0;
    int numbers2Sum = 0;

    foreach (int i in numbers1)
    {
        numbers1Sum += i;
    }

    foreach (int i in numbers2)
    {
        numbers2Sum += i;
    }

    if (numbers1Sum > numbers2Sum)
    {
        Console.WriteLine("сумма чисел массива numbers1 больше");
    }
    else
    {
        Console.WriteLine("сумма чисел массива numbers1 меньше");
    }
}

int[] numbers1 = { 1, 2, 3, 4, 5 };
int[] numbers2 = { 1, 2, 3, 4, 5, 6, 7 };
Compare(numbers1, numbers2);
//  Здесь есть один недостаток: повторяется действия, которые вычисляют сумму массива.

void Compare2(int[] numbers1, int[] numbers2)
{
    int numbers1Sum = Sum(numbers1);
    int numbers2Sum = Sum(numbers2);

    if (numbers1Sum > numbers2Sum)
    {
        Console.WriteLine("сумма чисел массива numbers1 больше");
    }
    else
    {
        Console.WriteLine("сумма чисел массива numbers1 меньше");
    }

    int Sum(int[] numbers)
    {
        int result = 0;
        foreach (int i in numbers)
        {
            result += i;
        }
        return result;
    }
}
//  Здесь подсчет суммы вынесен в локальную функцию Sum, которая принимает массив и возвращает его сумму.
//  Вне её метода локальная функция не может использоваться.
#endregion

#region Статические локальные функции
//  Особенностью функции определяются с помощью ключевого слова static является то, что они не могут обращаться
//  к переменным окружения, то есть метода, в котором статическая функция определена.

//  Сначала определим локальную функцию, которая имеет доступ окружению:
int[] numbers3 = { -3, -2, -1, 0, 1, 2, 3 };
int[] numbers4 = { 3, -4, 5, -6, 7 };

int Sum2(int[] numbers)
{
    int limit = 0;
    int result = 0;
    foreach (int number in numbers)
    {
        if (IsPassed(number)) result += number;
    }
    return result;

    bool IsPassed(int number)
    {
        return number > limit;
    }
}

//  Теперь функция IsPassed не может обращаться к переменным, в этом случае надо передать значения в виде параметров, 
//  либо определить непосредственно в локальной функции
int Sum3(int[] numbers)
{
    int limit = 0;
    int result = 0;
    foreach (int number in numbers)
    {
        if (IsPassed(number,limit)) result += number;
    }
    return result;

    static bool IsPassed(int number, int limit)
    {
        return number > limit;
    }
}
#endregion