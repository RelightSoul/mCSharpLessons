// Изменим предыдущую задачу. Допустим, банк периодически начисляет по всем вкладам кроме
// процентов бонусы. И, допустим, сейчас банк решил доначислить по всем вкладам 15 единиц
// вне зависимости от их суммы. Измените программу таким образом, чтобы к финальной сумме
// дочислялись бонусы.
Console.WriteLine("Вычисление процента по вкладу");
Console.WriteLine("Введите сумму взноса");
double depositEmount = Convert.ToDouble(Console.ReadLine());
double bankBonuses = 15;
if (depositEmount < 100)
{
    depositEmount += 0.05 * depositEmount;
}
else if (100 <= depositEmount && depositEmount <= 200)
{
    depositEmount += 0.07 * depositEmount;
}
else
{
    depositEmount += 0.1 * depositEmount;
}
Console.WriteLine("Сумма вклада с начисленными процентами и бонусом от банка: {0}", depositEmount + bankBonuses);
