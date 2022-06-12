// Массив параметров и ключевое слово params

//Во всех предыдущих примерах мы использовали постоянное число параметров. Но, используя ключевое слово params,
//мы можем передавать неопределенное количество параметров

void Sum(params int[] numbers)
{
    int result = 0;
    foreach (int i in numbers)
    {
        result += i;
    }
    Console.WriteLine(result);
}

int[] nums = {1,2,3,4,5,6,7};
Sum(nums);
Sum(1, 3, 4, 5);
Sum(5, 6);
Sum();

//Если же нам надо передать какие- то другие параметры, то они должны указываться до параметра с ключевым словом params:
void Sum2(int initialValue, params int[] numbers)
{
    int result = initialValue;
    foreach (var n in numbers)
    {
        result += n;
    }
    Console.WriteLine(result);
}

int[] nums2 = { 1, 2, 3, 4, 5 };
Sum2(10, nums2);  // число 10 - передается параметру initialValue
Sum2(1, 2, 3, 4); 
Sum2(1, 2, 3);
Sum2(20);
// после параметра с модификатором params мы НЕ можем указывать другие параметры.

//Также этот способ передачи параметров надо отличать от передачи массива в качестве параметра:
void Sum3(int[] numbers, int initialValue)
{
    int result = initialValue;
    foreach (var n in numbers)
    {
        result += n;
    }
    Console.WriteLine(result);
}

int[] nums3 = { 1, 2, 3, 4, 5 };
Sum3(nums3, 10);
//Так как метод Sum принимает в качестве параметра массив без ключевого слова params, то при его вызове
//нам обязательно надо передать в качестве первого параметра массив.
