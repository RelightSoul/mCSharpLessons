//  Атрибуты валидации

//  Для валидации модели мы можем использовать большой набор встроенных атрибутов. Все эти атрибуты представляют классы,
//  унаследованные от класса ValidationAttribute

//  Имеется довольно большое количество атрибутов. Основные из них:

//  Required: данный атрибут указывает, что свойство должно быть обязательно установлено, обязательно должно иметь какое-либо
//  значение. Если свойство имеет значение null, то оно не проходит валидацию. Также не проходят валидацию свойства, которые
//  представляют тип string, и которым присваивается пустая строка.

//  Из его свойств следует отметить свойство AllowEmptyStrings. Если оно имеет значение true, то для строковых свойств разрешено
//  использовать пустые строки:
//      [Required(AllowEmptyStrings = true)]
//      public string? Name { get; set; } = "";

//  RegularExpression: указывает на регулярное выражение, которому должно соответствовать значение свойства. Например, пусть
//  у пользователя определено свойство Phone, которое хранит номер телефона:
//using System.ComponentModel.DataAnnotations;

//CreateUser("+1111-111-2345");   // проходит валидацию
//CreateUser("+11111112345");     // НЕ проходит валидацию
//CreateUser("+0111-111-2345");   // НЕ проходит валидацию

//void CreateUser(string phone)
//{
//    User user = new User(phone);
//    var results = new List<ValidationResult>();
//    var context = new ValidationContext(user);
//    if (Validator.TryValidateObject(user, context, results, true))
//        Console.WriteLine("проходит валидацию");
//    else
//        Console.WriteLine("НЕ проходит валидацию");
//}
//public class User
//{
//    [RegularExpression(@"^\+[1-9]\d{3}-\d{3}-\d{4}$")]
//    public string Phone { get; set; }

//    public User(string phone) => Phone = phone;
//}

//  StringLength: определяет допустимую длину для строковых свойств. В качестве первого параметра он принимает максимально
//  допустимую длину строки. С помощью дополнительного свойства MinimumLength можно установить минимально допустимую
//  длину строки
//  public class User
//  {
//      [StringLength(20, MinimumLength = 3)]
//      public string Name { get; set; }

//      public User(string name) => Name = name;
//  }

//  Range: задает диапазон допустимых числовых значений. В качестве первых двух параметров он принимает минимальное и
//  максимальное значения:
//  [Range(1, 100)]
//  public int Age { get; set; }

//  Compare: позволяет сравнить значение текущего свойства со значением другого свойства, которое передается в этот атрибут.
//  Например, нередко при регистрации пользователя ему предоставляется два поля для ввода пароля, чтобы не ошибиться.
//  В этом случае мы могли бы определить ля регистрации следующий класс:
//public class User2
//{
//    [Required]
//    public string Name { get; set; }

//    [Required]
//    public string? Password { get; set; }

//    [Required]
//    [Compare("Password")]
//    public string? ConfirmPassword { get; set; }

//    public User2(string name) => Name = name;
//}
//      И если значения свойств Password и ConfirmPassword не будут совпадать, тогда мы получим ошибку валидации.

//  Phone: данный атрибут автоматически валидирует значение свойства, является ли оно телефонным номером. Фактически это
//  встроенная альтернатива использованию регулярного выражения, как было показано выше
//  [Phone]
//  public string? Phone { get; set; }

//  EmailAddress: определяет, является ли значение свойства электронным адресом

//  CreditCard: определяет, является ли значение свойства номером кредитной карты

//  Url: определяет, является ли значение свойства гиперссылкой

#region Настройка сообщения об ошибке
//  Класс ValidationAttribute определяет для атрибутов ряд общих свойств и методов, из которых следует выделить свойство
//  ErrorMessage. Это свойство позволяет задать сообщение обошибке.

//  При выводе ошибок валидации .NET использует встроенные локализованные сообщение. А данное свойство как раз и позволяет
//  переопределить сообщение об ошибке:
using System.ComponentModel.DataAnnotations;

CreateUser("Tom", 37);
CreateUser("T", 120);
CreateUser("", -2);

void CreateUser(string name, int age)
{
    User user = new User(name, age);
    var results = new List<ValidationResult>();
    var context = new ValidationContext(user);
    if (!Validator.TryValidateObject(user, context, results, true))
    {
        foreach (var error in results)
        {
            Console.WriteLine(error.ErrorMessage);
        }
    }
    else
        Console.WriteLine($"Объект User успешно создан. Name: {user.Name}");

    Console.WriteLine(); // для разделения
}
public class User
{
    [Required(ErrorMessage = "Не указано имя пользователя")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
    public string Name { get; set; }

    [Range(1, 100, ErrorMessage = "Недопустимый возраст")]
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
#endregion

#region Передача в сообщение об ошибке аргументов
//  Ряд атрибутов с помощью свойств могут принимать различные параметры. И эти параметры можно использовать в сообщении об
//  ошибке. Например, изменим предыдущий код следующим образом:
public class User2
{
    [Required(ErrorMessage = "Не указано имя пользователя")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в диапазоне от {2}-{1} символов")]
    public string Name { get; set; }

    [Range(1, 100, ErrorMessage = "Возраст должен быть в диапазоне {1}-{2}")]
    public int Age { get; set; }

    public User2(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
//  В сообщение об ошибке с помощью плейсхолдеров {0}, { 1}, { 2} и т.д.можно передавать параметры атрибута.Параметр { 0}
//  в любом атрибуте представляет имя свойства, которое валидируется. То есть в данном случае это было бы "Name". {1} здесь
//  - первый параметр атрибута - максимальное количество символов - 50. {2} -второй параметр - значение свойства
//  MinimumLength.То есть в итоге сформируется сообщение Длина имени должна быть в диапазоне от 3-50 символов
#endregion
