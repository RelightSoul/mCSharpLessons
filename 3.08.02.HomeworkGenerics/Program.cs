//  Напишите обобщенный класс, который может хранить в массиве объекты любого типа.
//  Кроме того, данный класс должен иметь методы для добавления данных в массив,
//  удаления из массива, получения элемента из массива по индексу и метод, возвращающий длину массива.

//  Для упрощения работы можно пересоздавать массив при каждой операции добавления и удаления

class Program
{
    static void Main(string[] args)
    {
        MyArray<int> arr = new MyArray<int>();
        arr.MyArrayLength();
        arr.Add(4);
        arr.Add(44);
        arr.Add(73);
        arr.Add(26);
        arr.MyArrayLength();
        Console.WriteLine(arr.MyArrayLength());
        Console.WriteLine(arr.ArrayElement(3));
        for (int i = 0; i < arr.MyArrayLength(); i++)
            Console.Write($"{arr.ArrayElement(i)}\t");
        Console.WriteLine();
        arr.Remove(73);
        for (int i = 0; i < arr.MyArrayLength(); i++)
            Console.Write($"{arr.ArrayElement(i)}\t");
    }
}
class MyArray<T>
{
    public T[] array;
    public MyArray()
    {
        this.array = new T[0];
    }

    public void Add(T s)
    {
        T[] newArray = new T[array.Length + 1];
        for (int i = 0; i < array.Length; i++) 
            newArray[i] = array[i];
        newArray[array.Length] = s;
        array = newArray;        
    }

    public void Remove(T s)    // удаление числа по значению
    {
        int index = -1;
        for (int i = 0; i < array.Length; i++)
            if (array[i].Equals(s))
            {
                index = i;
                break;
            }
        if (index > -1)
        {
            int j = 0;
            T[] newArray = new T[array.Length - 1];
            for (int i = 0; i < array.Length; i++)
                if (i == index)
                continue;
                else
                {
                    newArray[j] = array[i];
                    j++;
                }
            array = newArray;
        }
    }

    public T ArrayElement(int x)
    {
        if (x >= 0 && x < array.Length)
            return array[x];        
        else
            throw new IndexOutOfRangeException();
    }

    public int MyArrayLength()
    {
        return array.Length;
    }
}

