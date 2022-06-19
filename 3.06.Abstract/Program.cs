//  Главное отличие абстрактных классов от обычных состоит в том, что мы НЕ можем использовать
//  конструктор абстрактного класса для создания экземпляра класса.

//  Тем не менее абстрактные классы полезны для описания некоторого общего функционала,
//  который могут наследовать и использовать производные классы:

abstract class Transport
{
    public string Name { get; set; }

    // Мы не можем использовать конкструктор для создания экземпляра абстрактного класса, но может его определить
    public Transport(string name)      
    {
        Name = name;
    }

    public void Move()
    {
        Console.WriteLine("Траспорт двигается");
    }
}
class Car : Transport
{
    public Car(string name) : base(name)
    {
    }
}
class Aircraft : Transport
{
    public Aircraft(string name) : base(name)
    {
    }
}
class Ship : Transport
{
    public Ship(string name) : base(name)
    {
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car car = new Car("Car1");
        Ship ship = new Ship("Ship1");
        Aircraft aircraft = new Aircraft("Aircraft1");

        car.Move();    // имеют общую черту наследуемую от Transport
        ship.Move();
        aircraft.Move();

        Car2 car2 = new Car2("Car1");
        Ship2 ship2 = new Ship2("Ship1");
        Aircraft2 aircraft2 = new Aircraft2("Aircraft1");

        car2.Move();    
        ship2.Move();
        aircraft2.Move();
    }
}

#region Абстрактные члены классов
//  Кроме обычных свойств и методов абстрактный класс может иметь абстрактные члены классов,
//  которые определяются с помощью ключевого слова abstract и не имеют никакого функционала.
//  В частности, абстрактными могут быть

//      Методы

//      Свойства

//      Индексаторы

//      События

//  Абстрактные члены классов не должны иметь модификатор private. При этом производный класс
//  обязан переопределить и реализовать все абстрактные методы и свойства, которые имеются в
//  базовом абстрактном классе. При переопределении в производном классе такой метод или свойство
//  также объявляются с модификатором override (как и при обычном переопределении виртуальных методов и свойств).
//  Также следует учесть, что если класс имеет хотя бы один абстрактный метод
//  (или абстрактные свойство, индексатор, событие), то этот класс должен быть определен как абстрактный.

//  Абстрактные члены также, как и виртуальные, являются частью полиморфного интерфейса.
//  Но если в случае с виртуальными методами мы говорим, что класс-наследник наследует реализацию,
//  то в случае с абстрактными методами наследуется интерфейс, представленный этими абстрактными методами.
#endregion

#region Абстрактные методы
abstract class Transport2
{
    public string Name { get; set; }        
    public Transport2(string name)
    {
        Name = name;
    }
    // Делаем метод Move абстрактным, а его реализацию перекладываем на производные классы
    public abstract void Move();
}
class Car2 : Transport2
{
    public Car2(string name) : base(name)
    {
    }

    public override void Move()
    {
        Console.WriteLine("Машина едет");
    }
}
class Aircraft2 : Transport2
{
    public Aircraft2(string name) : base(name)
    {
    }

    public override void Move()
    {
        Console.WriteLine("Самолёт летит");
    }
}
class Ship2 : Transport2
{
    public Ship2(string name) : base(name)
    {
    }

    public override void Move()
    {
        Console.WriteLine("Корабль плывёт");
    }
}
#endregion

#region Абстрактные свойства
//  Следует отметить использование абстрактных свойств. Их определение похоже на определение автосвойств.
abstract class Transport3
{
    public abstract int Speed { get; set; }    
}
class Car3 : Transport3
{
    public int speed;    
    public override int Speed 
    {
        get { return speed; }
        set { speed = value;}
    }
}
class Aircraft3 : Transport3
{
    public override int Speed 
    {
        get
        { if (Speed<20) { Console.WriteLine("У самолёта не может быть такая маленькая скорость");} 
                return Speed;}
        set => Speed = value;
    }
}
class Ship3 : Transport3
{
    public override int Speed { get; set; }
}
//  В классе Transport определено абстрактное свойство Speed, которое должно хранить скорость транспортного
//  средства. Оно похоже на автосвойство, но это не автосвойство. Так как данное свойство не должно иметь
//  реализацию, то оно имеет только пустые блоки get и set. В производных классах мы можем переопределить
//  это свойство, сделав его полноценным свойством (как в классе Ship), либо же сделав его автоматическим
//  (как в классе Aircraft).
#endregion

#region Отказ от реализации абстрактных членов
//  Производный класс обязан реализовать все абстрактные члены базового класса. Однако мы можем
//  отказаться от реализации, но в этом случае производный класс также должен быть определен как абстрактный:
abstract class Transport4
{
    public abstract int Speed { get; set; }
}
abstract class Car4 : Transport4
{
}
#endregion