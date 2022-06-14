// Перепишите предыдущую программу, только вместо цикла for используйте цикл while.

Console.WriteLine("Расчёт начислений");
Console.Write("Введите сумму вклада: ");
decimal curSUM = Convert.ToDecimal(Console.ReadLine());
Console.Write("Введите количество месяцев: ");
int mValue = Convert.ToInt32(Console.ReadLine());
while (mValue > 0)
{
    curSUM += curSUM * 0.07m;
    mValue-- ;
}
Console.WriteLine("Конечная сумма вклада равна: {0}", curSUM);