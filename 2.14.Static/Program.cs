// Кроме обычных полей, методов, свойств классы и структуры могут иметь статические поля,
// методы, свойства. Статические поля, методы, свойства относятся ко всему классу/всей
// структуре и для обращения к подобным членам необязательно создавать экземпляр класса / структуры.

namespace _2._14.Static
{
    class Program
    {
        static void Main(string[] args)
        {
            Person bob = new(68);
            bob.CheckAge();

            Person tom = new(37);
            tom.CheckAge();

            Person2 bob2 = new(68);
            bob2.CheckAge();
            Console.WriteLine(Person2.RetirementAge);

            Person3 bob3 = new(68);
            Person3.CheckRetirementStatus(bob3);

            Person4 bob4 = new();
            Console.WriteLine(Person4.RetirementAge);

        }
        #region Статические поля
        //  Статические поля хранят состояние всего класса / структуры.
        class Person
        {
            int age;
            public static int retirementAge = 65;
            //  поле retirementAge относится не к отдельную объекту и хранит значение НЕ отдельного объекта
            //  класса Person, а относится ко всему классу Person и хранит общее значение для всего класса.
            public Person(int age)
            {
                this.age = age;
            }
            public void CheckAge()
            {
                if (age >= retirementAge)
                {
                    Console.WriteLine("Уже на пенсии");
                }
                else
                {
                    Console.WriteLine($"Сколько лет до пенсии: {retirementAge - age}");
                }
            }
        }
        #endregion

        #region Статические свойства
        //  Подобным образом мы можем создавать и использовать статические свойства
        
        class Person2
        {
            int age;
            static int retirementAge = 65;
            static int counter = 0;
            //  Нередко статические поля и свойства применяются для хранения счетчиков
            public static int Counter => counter;
            //  В данном случае доступ к статической переменной retirementAge опосредуется с помощью
            //  статического свойства RetirementAge.
            public static int RetirementAge 
            {
                get { return retirementAge; }
                set { if (value > 1 && value < 100) retirementAge = value;}
            }
            public Person2()
            {
                counter++;
            }
            public Person2(int age):this()
            {
                this.age = age;
            }
            public void CheckAge()
            {
                if (age >= retirementAge)
                {
                    Console.WriteLine("Уже на пенсии");
                }
                else
                {
                    Console.WriteLine($"Сколько лет до пенсии: {retirementAge - age}");
                }
            }
        }
        #endregion

        #region Статические методы
        //  Статические методы определяют общее для всех объектов поведение, которое не зависит от
        //  конкретного объекта. Для обращения к статическим методам также применяется имя класса / структуры
        class Person3
        {
            public int Age { get; set; }
            static int retirementAge = 65;
            public Person3(int age) => Age = age;
            public static void CheckRetirementStatus(Person3 person)
            {
                if (person.Age >= retirementAge)
                    Console.WriteLine("Уже на пенсии");
                else
                    Console.WriteLine($"Сколько лет осталось до пенсии: {retirementAge - person.Age}");
            }
        }
        //  Cтатические методы могут обращаться только к статическим членам класса
        #endregion

        #region Статические конструктор
        //  Статические конструкторы имеют следующие отличительные черты

        //  Статические конструкторы не должны иметь модификатор доступа и не принимают параметров

        //  Как и в статических методах, в статических конструкторах нельзя использовать ключевое
        //  слово this для ссылки на текущий объект класса и можно обращаться только к статическим
        //  членам класса

        //  Статические конструкторы нельзя вызвать в программе вручную.Они выполняются автоматически
        //  при самом первом создании объекта данного класса или при первом обращении к его статическим
        //  членам(если таковые имеются)
        class Person4
        {
            static int retirementAge;
            public static int RetirementAge => retirementAge;
            static Person4()
            {
                if (DateTime.Now.Year == 2022)
                    retirementAge = 65;
                else
                    retirementAge = 67;
            }
        }
        //  Статические конструкторы обычно используются для инициализации статических данных,
        //  либо же выполняют действия, которые требуется выполнить только один раз
        #endregion

        #region Статические классы
        //  Статические классы объявляются с модификатором static и могут содержать только
        //  статические поля, свойства и методы. Например, определим класс, который выполняет
        //  ряд арифметических операций:
        static class Operations
        {
            public static int Add(int x, int y) => x + y;
            public static int Subtract(int x, int y) => x - y;
            public static int Multiply(int x, int y) => x * y;
        }
        #endregion
    }
}