//if (условие)
//{
//    выполняемые инструкции
//}

int num1 = 8;
int num2 = 6;

#region if
if (num1>num2)  // если true, выполняется
{
    Console.WriteLine($"Число {num1} больше числа {num2}");
}
// Если блок if содержит одну инструкцию, то мы можем его сократить, убрав фигурные скобки:
if (num1 > num2)
    Console.WriteLine($"Число {num1} больше числа {num2}");
// Также мы можем соединить сразу несколько условий, используя логические операторы:
if (num1 > num2 && num1==8)
    Console.WriteLine($"Число {num1} больше числа {num2}");
#endregion

#region else
// Если мы захотим, чтобы при несоблюдении условия также выполнялись действия, мы можем добавить блок else:
if (num1 > num2) 
    Console.WriteLine($"Число {num1} больше числа {num2}");
else
    Console.WriteLine($"Число {num1} меньше числа {num2}");
#endregion

#region else if   
// else if, мы можем обрабатывать дополнительные условия
if (num1 > num2)
    Console.WriteLine($"Число {num1} больше числа {num2}");
else if (num1 < num2)
    Console.WriteLine($"Число {num1} меньше числа {num2}");
else
    Console.WriteLine($"Число {num1} равно числу {num2}");
#endregion

#region Тернарная операция
// [первый операнд - условие] ? [второй операнд] : [третий операнд]
// Если условие равно true, то возвращается второй операнд; если условие равно false, то третий
int x = 3;
int y = 2;
int z = x < y ? (x + y) : (x - y);
Console.WriteLine(z);  //1
#endregion
