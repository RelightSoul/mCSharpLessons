//В банке в зависимости от суммы вклада начисляемый процент по вкладу может отличаться.
//Напишите консольную программу, в которую пользователь вводит сумму вклада. Если сумма
//вклада меньше 100, то начисляется 5%. Если сумма вклада от 100 до 200, то начисляется 7%.
//Если сумма вклада больше 200, то начисляется 10%. В конце программа должна выводить сумму
//вклада с начисленными процентами
Console.WriteLine("Вычисление процента по вкладу");
Console.WriteLine("Введите сумму взноса");
double depositEmount = Convert.ToDouble(Console.ReadLine());
if (depositEmount<100)
{
    depositEmount = depositEmount + 0.05 * depositEmount;
}
else if (100 <= depositEmount && depositEmount <= 200)
{
    depositEmount = depositEmount + 0.07 * depositEmount;
}
else
{
    depositEmount = depositEmount + 0.1 * depositEmount;
}
Console.WriteLine("Сумма вклада с начисленными процентами: {0}", depositEmount);