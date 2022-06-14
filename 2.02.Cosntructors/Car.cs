using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._02.Cosntructors
{
    class Car
    {
        public int carSpeed;
        public string carName;
        public string carMaker;

        public Car(){}  // 1 Коструктор
        public Car(int speed)  // 2 Коструктор
        {
            carSpeed = speed;
        }
        public Car(int speed, string name) : this(speed)  // 3 Коструктор
        {
            carName = name;
        }
        public Car(int speed, string name, string maker) : this(speed, "SuperCar")  // 4 Коструктор
                                                              // Можно пристоить новые значения при передаче
        {
            carMaker = maker; 
        }

        //  По сути ключевое своло this, указывает на то что передаёт значения конструктору
        //  который их принимает, распределяя работу между кострукторами.
        //  Пример. Поступаю 3 значения при создание, скорость, имя, производитель.
        //  Четвёртый конструктор принимает эти значения, объявляет значение производителя и передаёт работу
        //  следующему 2 коструктору, тот объявляет имя и передаёт первому. 

        //  Мы можем конечно определить один коструктор, который будет применять и обяъвлять значения,
        //  всё зависит от ситуации что нам необходимо. 
        //  public Car(int speed, string name, string maker)
        //  {
        //      carMaker = maker;
        //      carName = name;
        //     carSpeed = speed;
        //  }

        public void PrintCar()
        {
            Console.WriteLine($"{carSpeed} {carName} {carMaker}");
        }
    }    
}
