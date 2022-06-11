// Задачи с массивами

#region Количество положительных чисел
int[] numbers = { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
int result = 0;
foreach (int i in numbers)
{
    if (i > 0)
    {
        result++;
    }
}
Console.WriteLine(result);
#endregion
Console.WriteLine();
#region Инверсия массива
int[] newArray = { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
int n = newArray.Length;  //длина
int k = n / 2;            //середина
int temp;                 //для обмена значений
for (int i = 0; i < k; i++)
{
    temp = newArray[i];
    newArray[i] = newArray[n - i - 1];
    newArray[n - i - 1] = temp;
}
foreach (int i in newArray)
{
    Console.Write($"{i}\t");
}
#endregion
Console.WriteLine();
Console.WriteLine();
#region Программа сортировки массива
int[] nums = { 54, 7, -41, 2, 4, 2, 89, 33, -5, 12 };
int newTemp;

//Сортировка
for (int i = 0; i < nums.Length - 1; i++)       //nums.Length - 1 потому что нам не нужно сравнивать последнее полученное число с самим собой
                                                //по сути можно написать и nums.Length, но это будет менее производительно.
{
    for (int j = i + 1; j < nums.Length; j++)
    {
        if (nums[i] > nums[j])
        {
            newTemp = nums[i];
            nums[i] = nums[j];
            nums[j] = newTemp;
        }
    }
}
Console.WriteLine("Отсортированный массив:");
for (int i = 0; i < nums.Length; i++)
{
    Console.Write("{0}\t", nums[i]);
}
Console.WriteLine();
#endregion