using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._02.Cosntructors
{
    class Person
    {
        public string Name;
        public int Age;
        public string Sex = "Undefined";

        #region Создание конструкторов
        //  На уровне кода конструктор представляет метод, который называется по имени класса, который
        //  может иметь параметры, но для него не надо определять возвращаемый тип.
        public Person()
        {
            Console.WriteLine("Создание объекта Person");
            Name = "Tom";
            Age = 37;
        }
        //  Определив конструктор, мы можем вызвать его для создания объекта Person:
        //  Person tom = new Person();  // Создание объекта Person в классе Program

        //  Подобным образом мы можем определять и другие конструкторы в классе:
        public Person(string name) { Name = name; Age = 15; }
        public Person(string name, int age) { Name = name; Age = age; }
        //  Теперь в классе определено три конструктора, каждый из которых принимает различное количество
        //  параметров и устанавливает значения полей класса        
        #endregion

        #region Ключевое слово this
        //  Ключевое слово this представляет ссылку на текущий экземпляр/объект класса.
        //  В каких ситуациях оно нам может пригодиться?
        public Person(string Name, int Age, string Sex)
        {
            this.Name = Name;
            this.Age = Age;
            this.Sex = Sex;
        }
        //  чтобы разграничить параметры и поля класса, если их имя совпадает
        #endregion

        #region Цепочка вызова конструкторов
        // Рассмотрим на примере нового класса Car
        #endregion

        public void Print()
        {
            Console.WriteLine($"Имя = {Name}, Возраст = {Age}, Пол = {Sex}");
        }

        //  Метод деконструктора
        internal void Deconstruct(out string name, out int age)
        {
            name = Name;
            age = Age;
        }
    }
    
}
