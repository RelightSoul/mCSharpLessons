using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._01.ClassesStructuresNamespace
{
    class Person
    {
        public string Name = "Undefined";
        public int Age;

        public void Print()
        {
            Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
        }
    }
}
