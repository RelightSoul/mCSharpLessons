//  Бинарные файлы. BinaryWriter и BinaryReader
//  Для работы с бинарными файлами предназначена пара классов BinaryWriter и BinaryReader.
//  Эти классы позволяют читать и записывать данные в двоичном формате.

#region BinaryWriter
//  Для создания объекта BinaryWriter можно применять ряд конструкторов. Возьмем наиболее простую:
//              BinaryWriter(Stream stream)
//  в его конструктор передается объект Stream (обычно это объект FileStream).

//  Основные методы класса BinaryWriter

//  Close(): закрывает поток и освобождает ресурсы
//  Flush(): очищает буфер, дописывая из него оставшиеся данные в файл
//  Seek(): устанавливает позицию в потоке
//  Write(): записывает данные в поток. В качестве параметра этот метод может принимать значения примитивных данных:
//          Write(bool)
//          Write(byte)
//          Write(char)
//          Write(decimal)
//          Write(double)
//          Write(Half)
//          Write(short)
//          Write(int)
//          Write(long)
//          Write(sbyte)
//          Write(float)
//          Write(string)
//          Write(ushort)
//          Write(uint)
//          Write(ulong)
//  Либо можно передать массивы типов byte и char
//          Write(byte[])
//          Write(char[])
//          Write(ReadOnlySpan<byte>)
//          Write(ReadOnlySpan<char>)
//  При записи массива дополнительно можно указать, с кого элемента массива надо выполнять запись, а также число
//  записываемых элементов массива:
//          Write(byte[], int, int)
//          Write(char[], int, int)

//Рассмотрим простейшую запись бинарного файла:
string path = "person.dat";

// создаем объект BinaryWriter
using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
{
    // записываем в файл строку
    writer.Write("Tom");
    // записываем в файл число int
    writer.Write(37);
    Console.WriteLine("File has been written");
}
//  Здесь в файл person.dat записываются два значения: строка "Tom" и число 37. Для создание объекта
//  применяется вызов new BinaryWriter(File.Open(path, FileMode.OpenOrCreate))
#endregion

#region BinaryReader
//  Для создания объекта BinaryReader можно применять ряд конструкторов. Возьмем наиболее простую версию:
//              Reader(Stream stream)
//  в его конструктор также передается объект Stream (также обычно это объект FileStream).

//  Основные методы класса BinaryReader
//  Close(): закрывает поток и освобождает ресурсы
//  ReadBoolean(): считывает значение bool и перемещает указатель на один байт
//  ReadByte(): считывает один байт и перемещает указатель на один байт
//  ReadChar(): считывает значение char, то есть один символ, и перемещает указатель на столько байтов,
//  сколько занимает символ в текущей кодировке
//  ReadDecimal(): считывает значение decimal и перемещает указатель на 16 байт
//  ReadDouble(): считывает значение double и перемещает указатель на 8 байт
//  ReadInt16(): считывает значение short и перемещает указатель на 2 байта
//  ReadInt32(): считывает значение int и перемещает указатель на 4 байта
//  ReadInt64(): считывает значение long и перемещает указатель на 8 байт
//  ReadSingle(): считывает значение float и перемещает указатель на 4 байта
//  ReadString(): считывает значение string. Каждая строка предваряется значением длины строки, которое
//  представляет 7-битное целое число

//  С чтением бинарных данных все просто: соответствующий метод считывает данные определенного типа и перемещает
//  указатель на размер этого типа в байтах, например, значение типа int занимает 4 байта, поэтому BinaryReader
//  считает 4 байта и переместит указатель на эти 4 байта.

//  Например, выше в примере с BinaryWriter в файл person.dat записывалась строка и число. Считаем их с помощью
//  BinaryReader:
using (BinaryReader reader = new BinaryReader(File.Open("person.dat", FileMode.Open)))
{
    // считываем из файла строку
    string name = reader.ReadString();
    // считываем из файла число 
    int age = reader.ReadInt32();
    Console.WriteLine($"Name: {name}  Age: {age}");
}
//  Конструктор класса BinaryReader также в качестве параметра принимает объект потока, только в данном случае
//  устанавливаем в качестве режима FileMode.Open: new BinaryReader(File.Open("person.dat", FileMode.Open)).

//  В каком порядке данные были записаны в файл, в таком порядке мы их можем оттуда считать. То есть если сначала
//  записывалась строка, а потом число, то в данном порядке мы их можем считать из файла.
#endregion
