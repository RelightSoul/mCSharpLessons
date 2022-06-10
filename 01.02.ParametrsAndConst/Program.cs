//тип имя_переменной;
string name;    //C# является регистрозависимым языком, поэтому следующие
string Name;    //два определения переменных будут представлять две разные переменные
name = "Tom";
string name2 = "Sam";
Console.WriteLine(name);

name = "Peter";
Console.WriteLine(name);

const string NAME = "Rosee";
//NAME = "Samuel";     нельзя изменить константу

Console.ReadLine();