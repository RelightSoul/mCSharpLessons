//  Все эти типы данных можно разделить на типы значений, еще называемые значимыми типами,
//  (value types) и ссылочные типы (reference types).

//  Значимые:
//  byte, sbyte, short, ushort, int, uint, long, ulong
//  float, double
//  decimal
//  bool
//  char
//  enum
//  struct

//  Ссылочные:
//  object
//  string
//  class
//  interface
//  delegate

//  Тип данных надо учитывать при копировании значений. При присвоении данных объекту значимого типа
//  он получает копию данных. При присвоении данных объекту ссылочного типа он получает не копию объекта,
//  а ссылку на этот объект в хипе.
namespace _2._05.ValueAndReferenceTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            State state1 = new State();
            State state2 = new State();
            state2.x = 1;
            state2.y = 5;
            state1=state2;
            state2.x = 5;
            Console.WriteLine(state1.x);
            Console.WriteLine(state2.x);

            County country1 = new County();
            County country2 = new County();
            country2.x = 2;
            country2.y = 9;
            country1 = country2;
            country2.x = 26;
            Console.WriteLine(country1.x);
            Console.WriteLine(country2.x);

            //  Так как state1 - структура, то при присвоении state1 = state2 она получает копию
            //  структуры state2. А объект класса country1 при присвоении country1 = country2;
            //  получает ссылку на тот же объект, на который указывает country2. Поэтому с изменением
            //  country2, так же будет меняться и country1.

            //  Когда внутри структуры у нас есть переменная ссылочного типа, мы копируем вместе со
            //  структурой ссылку на эту переменную, тем самым её изменение в новой структуре повлечёт
            //  изменения в старой
            #region Объекты классов как параметры методов
            Person p = new Person { name = "Tom", age = 31 };
            ChangePerson(p);
            //  При передаче объекта класса по значению в метод передается копия ссылки на объект.
            //  Эта копия указывает на тот же объект, что и исходная ссылка, потому мы можем изменить
            //  отдельные поля и свойства объекта, но не можем изменить сам объект.
            Console.WriteLine($"Имя и возраст в программе по значению {p.name} {p.age}");      // Alice
            Console.WriteLine();
            ChangePerson(ref p);
            Console.WriteLine($"Имя и возраст в программе с ref {p.name} {p.age}");
        }
        public static void ChangePerson(Person person)
        {
            person.name = "Alice";

            person = new Person { name = "Bill", age = 44 };
            Console.WriteLine($"Имя и возраст в методе {person.name} {person.age}");    // Bill
        }
        public static void ChangePerson(ref Person person)
        {
            person.name = "Alice";

            person = new Person { name = "Bill", age = 44 };
            Console.WriteLine($"Имя и возраст в методе {person.name} {person.age}");    // Bill
        }
        #endregion
    }
    struct State           //значимый тип
    {
        public int x;
        public int y;
    }
    class County           //ссылочный тип
    {
        public int x;
        public int y;
    }
    class Person
    {
        public string name = "";
        public int age; 
    }    
}

