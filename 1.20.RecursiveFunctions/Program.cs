//  Рекурсивные функции

//  Рекурсивная функция представляет такую конструкцию, при которой функция вызывает саму себя.
#region Рекурсивная функция факториала
int Factorial(int number)
{
    if (number == 1)
        return 1;

    return number * Factorial(number - 1);    // рекурсивный спуск
}

int a = Factorial(5);                  // 5 * 4 * 3 * 2 * Factorial(1)
Console.WriteLine(a);
#endregion

#region Рекурсивная функция Фибоначчи
int Fibonachi(int number)
{
    if (number == 0 || number == 1)
        return number;
    return Fibonachi(number - 1) + Fibonachi(number - 2);
}
Console.WriteLine(Fibonachi(6));
#endregion

#region Рекурсии и циклы
//  Это простейшие пример рекурсивных функций, которые призваны дать понимание работы рекурсии. В то же время
//  для обоих функций вместо рекурсий можно использовать циклические конструкции. И, как правило, альтернативы
//  на основе циклов работают быстрее и более эффективны, чем рекурсия. Например, вычисление чисел Фибоначчи с помощью циклов:
static int Fibonachi2(int number)
{
    int result = 0;
    int b = 1;
    int tmp;

    for (int i = 0; i < number; i++)
    {
        tmp = result;
        result = b;
        b += tmp;
    }
    return result;
}
Console.WriteLine(Fibonachi2(6));
#endregion