string hello = "Привет мир";
Console.WriteLine(hello);
Console.WriteLine("Добро пожаловать в C#");
Console.WriteLine("Пока мир...");
Console.WriteLine(24.5);
Console.WriteLine();

string name = "Tom";
int age = 34;
double heigth = 1.7;
Console.WriteLine($"Name:{name}  Age:{age}  Height:{heigth}");
Console.WriteLine("Name:{0}  Age:{1}  Height:{2}",name,age,heigth);
Console.WriteLine();

Console.WriteLine("Введите имя: ");
string? curName = Console.ReadLine();
Console.WriteLine($"Привет {curName}");

Console.WriteLine("Введите возраст: ");
int curAge = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Введите размер платы: ");
decimal curPay = Convert.ToDecimal(Console.ReadLine());
