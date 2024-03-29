﻿// Задан следующий трехмерный массив:
int[,,] mas = { { { 1, 2 },{ 3, 4 } },
                { { 4, 5 }, { 6, 7 } },
                { { 7, 8 }, { 9, 10 } },
                { { 10, 11 }, { 12, 13 } }
              };
// С помощью циклов переберите все элементы этого массива и выведите их на консоль в следующем виде:
// {{{1 , 2} , {3 , 4}} , {{4 , 5} , {6 , 7}} , {{7 , 8}, {9 , 10}} , {{10 , 11} , {12 , 13}}}

// GetUpperBound  возвращает индекс последнего элемента в определенной размерности
// GetLength возвращает количество элементов в определенной размерности

//Допустим у нас пятимерный массив int[,,,x,] bigArray, я хочу узнать количество элементов в 4 измерении где сейчас x, элементы 
//считаются слева на право от 0, то есть я должен указать 3 в GetLength(3), чтобы получить x

Console.Write("{");
for (int i = 0; i < mas.GetLength(0); i++)
{
    Console.Write("{");
    for (int j = 0; j < mas.GetLength(1); j++)
    {
        Console.Write("{");
        for (int k = 0; k < mas.GetLength(2); k++)
        {
            Console.Write($"{mas[i,j,k]}");
            if (k < mas.GetLength(2) - 1)                     // нужно ставить запятые между элементами
            {
                Console.Write(" , ");
            }
        }
        Console.Write("}");
        if (j < mas.GetLength(1) - 1)
        {
            Console.Write(" , ");
        }
    }
    Console.Write("}");
    if (i < mas.GetLength(0) - 1)
    {
        Console.Write(" , ");
    }
}
Console.Write("}");