//  Параметры методов

#region Параметры
//   тип_метода имя_метода(тип_параметра1 параметр1, тип_параметра2 параметр2, ...)
//   {
//          // действия метода
//   }

// Параметры позволяют передать в метод некоторые входные данные. Параметры определяются
// через заятую в скобках после названия метода в виде:

void PrintMessage(string message)
{
    Console.WriteLine(message);
}

PrintMessage("Hell world =)");
PrintMessage("C# best programing language");

void Sum(int x, int y)
{
    int result = x + y;
    Console.WriteLine(result);
}

Sum(15, 33);
//Если параметрами метода передаются значения переменных, то таким переменным должно быть присвоено значение.
#endregion

#region Соответствие параметов и аргументов по типу данных
//При передаче значений параметрам важно учитывать тип параметров: между аргументами и параметрами должно быть соответствие по типу.
void PrintPerson(string name, int age) => Console.WriteLine($"Name: {name}, Age: {age}");
PrintPerson("Vlad", 32);   // name "Vlad",age 32
#endregion

#region Необязательные параметры
//По умолчанию при вызове метода необходимо предоставить значения для всех его параметров. Но C# также
//позволяет использовать необязательные параметры. Для таких параметров нам необходимо объявить значение
//по умолчанию. Также следует учитывать, что после необязательных параметров все последующие параметры
//также должны быть необязательными

void PrintEmployee(string name, int age = 0, string company = "Undefined")
{
    Console.WriteLine("Name: {0}, Age: {1}, Company: {2}",name,age,company);
}
PrintEmployee("Roman");
PrintEmployee("Alex", 33, "Microsoft");
#endregion

#region Именованные параметры
//В предыдущих примерах при вызове методов значения для параметров передавались в порядке объявления этих параметров в методе.
//То есть аргументы передавались параметрам по позиции. Но мы можем нарушить подобный порядок, используя именованные параметры
void NewPrintEmployee(string name, int age = 0, string company = "Undefined")
{
    Console.WriteLine("Name: {0}, Age: {1}, Company: {2}", name, age, company);
}
int empAge = 40;
NewPrintEmployee(age: empAge, name: "Samuel");
#endregion