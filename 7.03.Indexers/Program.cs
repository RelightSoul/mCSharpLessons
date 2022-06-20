// Индексаторы

//  Индексаторы позволяют индексировать объекты и обращаться к данным по индексу. Фактически с помощью
//  индексаторов мы можем работать с объектами как с массивами. По форме они напоминают свойства со
//  стандартными блоками get и set, которые возвращают и присваивают значение.

//  Формальное определение индексатора:

//  возвращаемый_тип this[Тип параметр1, ...]
//  {
//       get { ... }
//       set { ... }
//  }

//  В отличие от свойств индексатор не имеет названия. Вместо него указывается ключевое слово this, после
//  которого в квадратных скобках идут параметры. Индексатор должен иметь как минимум один параметр

//  Посмотрим на примере. Допустим, у нас есть класс Person, который представляет человека, и класс Company,
//  который представляет некоторую компанию, где работают люди. Используем индексаторы для определения класса
//  Company:

class Person
{
    public string Name { get; }
    public Person(string name) => Name = name;
}
class Company
{
    Person[] personal;
    public Company(Person[] people)
    {
        personal = people;
    }
    // Индексатор
    public Person this[int index]
    {
        //  {
        //       get => personal[index];
        //       set => personal[index] = value;
        //  }
        get
        {
            if (index >= 0 && index < personal.Length)        
                return personal[index];        
            else        
                throw new ArgumentOutOfRangeException();
        }
        set
        {
            if (index >= 0 && index < personal.Length)        
                personal[index] = value;
        }
    }
}

//  Для хранения персонала компании в классе определен массив personal, который состоит из объектов Person.
//  Для доступа к этим объектам определен индексатор.

//  Индексатор в принципе подобен стандартному свойству. Во-первых, для индексатора определяется тип в данном
//  случае тип Person. Тип индексатора определяет, какие объекты будет получать и возвращать индексатор.

//  Во-вторых, для индексатора определен параметр int index, через который обращаемся к элементам внутри
//  объекта Company.

//  Поскольку индексатор имеет тип Person, то в блоке get нам надо возвратить объект этого типа с помощью
//  оператора return. Здесь мы можем определить разнообразную логику. В данном случае просто возвращаем
//  объект из массива personal.

class Program
{
    static void Main(string[] args)
    {
        var microsoft = new Company(new[]{
            new Person("Tom"), new Person("Bob"), new Person("Sam"), new Person("Alice")});

        Person firstPerson = microsoft[0];
        Console.WriteLine(firstPerson.Name);
        microsoft[0] = new Person("Alex");
        Console.WriteLine(microsoft[0].Name);

        //----------- Индексы -------------
        Console.WriteLine();

        User tom = new User();
        tom["name"] = "Tom";
        tom["email"] = "tom@gmail.ru";
        tom["phone"] = "+1234556767";
        Console.WriteLine(tom["name"]);

        //----------- Применение нескольких параметров -------------
        Matrix matrix = new Matrix();
        Console.WriteLine(matrix[0, 0]);
        matrix[0, 0] = 111;
        Console.WriteLine(matrix[0, 0]);

        //----------- Перегрузка индексаторов -------------
        var microsoft33 = new Company33(new Person33[] { new("Tom"), new("Bob"), new("Sam") });

        Console.WriteLine(microsoft33[0].Name);      // Tom
        Console.WriteLine(microsoft33["Bob"].Name);  // Bob
    }
}
#region Индексы
//  Индексатор получает набор индексов в виде параметров. Однако индексы необязательно должны представлять
//  тип int, устанавливаемые/возвращаемые значения необязательно хранить в массиве. Например, мы можем
//  рассматривать объект как хранилище атрибутов/свойств и передавать имя атрибута в виде строки:
class User
{
    string name = "";
    string email = "";
    string phone = "";
    public string this[string propname]
    {
        get
        {
            switch (propname)
            {
                case "name": return name;
                case "email": return email;
                case "phone":return phone;
                default:throw new Exception("Unknow Property");
            }
        }
        set 
        {
            switch (propname)
            {
                case "name": name = value;
                    break;
                case "email": email = value;
                    break;
                case "phone": phone = value;
                    break;
            }
        }
    }
}
#endregion

#region Применение нескольких параметров
//  Также индексатор может принимать несколько параметров. Допустим, у нас есть класс, в котором хранилище
//  определено в виде двухмерного массива или матрицы:
class Matrix
{
    int[,] numbers = new int[,] { { 1, 2, 4 }, { 2, 3, 6 }, { 3, 4, 8 } };
    public int this[int i, int j]
    {
        get => numbers[i, j];
        set => numbers[i, j] = value;
    }
}
//  Следует учитывать, что индексатор не может быть статическим и применяется только к экземпляру класса.
//  Но при этом индексаторы могут быть виртуальными и абстрактными и могут переопределяться в произодных классах.
#endregion

#region Блоки get и set
//  Как и в свойствах, в индексаторах можно опускать блок get или set, если в них нет необходимости. Например,
//  удалим блок set и сделаем индексатор доступным только для чтения:
class Matrix2
{
    int[,] numbers = new int[,] { { 1, 2, 4 }, { 2, 3, 6 }, { 3, 4, 8 } };
    public int this[int i, int j]
    {
        get => numbers[i, j];
    }
}
//  Также мы можем ограничивать доступ к блокам get и set, используя модификаторы доступа. Например, сделаем
//  блок set приватным:
class Matrix3
{
    int[,] numbers = new int[,] { { 1, 2, 4 }, { 2, 3, 6 }, { 3, 4, 8 } };
    public int this[int i, int j]
    {
        get => numbers[i, j];
        private set => numbers[i, j] = value;
    }
}
#endregion

#region Перегрузка индексаторов
//  Подобно методам индексаторы можно перегружать. В этом случае также индексаторы должны отличаться по
//  количеству, типу или порядку используемых параметров. Например:
class Person33
{
    public string Name { get; }
    public Person33(string name)
    {
        Name = name;
    }
}
class Company33
{
    Person33[] personal33;
    public Company33(Person33[] personal33)
    {
        this.personal33 = personal33;   
    }
    public Person33 this[int index]
    {
        get
        {
            return personal33[index];
        }
        set
        {
            personal33[index] = value;
        }
    }
    public Person33 this[string index]
    {
        get        
        {
            foreach (var p in personal33)
            {
                if (p.Name == index) return p;
            }
            throw new Exception("Нет такой персоны");
        }
    }
}
//  В данном случае класс Company содержит две версии индексатора. Первая версия получает и устанавливает
//  объект Person по индексу, а вторая - только получае объект Person по его имени.
#endregion
