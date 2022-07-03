//  Валидация модели
//  Основы валидации модели
using System.ComponentModel.DataAnnotations;
//  Большую роль в приложении играет валидация модели или проверка вводимых данных на корректность. Например,
//  у нас есть класс пользователя, в котором определено свойство для хранения возраста. И нам было бы нежелательно,
//  чтобы пользователь вводил какое-либо отрицательное число или заведомо невозможный возвраст, например, миллион лет.

//  В программе мы можем проверять вводимые данные с помощью условных конструкций:
CreateUser("Tom", 37);
CreateUser("b", -4);
CreateUser("", 130);

void CreateUser(string name, int age)
{
    User user = new User(name, age);
    if (user.Name.Length >= 3 && user.Name.Length <= 50)
    {
        Console.WriteLine(user.Name);
    }
    else
    {
        Console.WriteLine("Incorrect name!");
    }
    // проверяем корректность значения свойства Age
    // если оно в диапазоне от 1 до 100, то оно корректно
    if (age >= 1 && age <= 100)
        Console.WriteLine($"Age: {user.Age}\n");
    else
        Console.WriteLine("Incorrect age!\n");
}
//  Здесь предполагается, что имя должно иметь больше 1 символа, а возраст должен быть в диапазоне от 1 до 100. Однако
//  в классе может быть гораздо больше свойств, для которых надо осуществлять проверки. А это привет к тому, что увеличится
//  значительно код программы за счет проверок. К тому же задача валидации данных довольно часто встречается в приложениях.
//  Поэтому фреймворк .NET предлагает гораздо более удобный функционал в виде атрибутов из пространства имен
//  System.ComponentModel.DataAnnotations. Итак, изменим класс User
CreateUser2("Tom", 37);
CreateUser2("b", -4);
CreateUser2("", 130);

void CreateUser2(string name, int age)
{
    User user = new User(name, age);
    var context = new ValidationContext(user);
    var results = new List<ValidationResult>();
    if (!Validator.TryValidateObject(user,context,results,true))
    {
        Console.WriteLine("Не удалось создать объект User");
        foreach (var error in results)
        {
            Console.WriteLine(error.ErrorMessage);
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine(user.Name);
    }
}
//Здесь определен метод CreateUser, который принимает два значения и с их помощью создает объект User. В этом методе
//используются классы ValidationResult, Validator и ValidationContext, которые предоставляются пространством имен
//System.ComponentModel.DataAnnotations и которые управляют валидацией.

//Вначале мы создаем контекст валидации - объект ValidationContext. В качестве первого параметра в конструктор этого
//класса передается валидируемый объект, то есть в данном случае объект User.
//var context = new ValidationContext(user);
//Собственно валидацию производит класс Validator и его метод TryValidateObject(). В этот метод передается валидируемый
//объект (в данном случае объект user), контекст валидации, список объектов ValidationResult и булевый параметр,
//который указывает, надо ли валидировать все свойства.
//var results = new List<ValidationResult>();
//if (!Validator.TryValidateObject(user, context, results, true))
//{
//    //.......
//}
//Если метод Validator.TryValidateObject() возвращает false, значит объект не проходит валидацию. Если модель не проходит
//валидацию, то список объектов ValidationResult оказывается заполенным. А каждый объект ValidationResult содержит
//информацию о возникшей ошибке. Класс ValidationResult имеет два ключевых свойства: MemberNames - список свойств,
//для которых возникла ошибка, и ErrorMessage - собственно сообщение об ошибке.

//  Если применяются типы record, то атрибуты валидации можно указать непосредственно перед определением свойства:
public record class User2(

    [property: Required]
    [property: StringLength(50, MinimumLength = 3)]
    string Name,

    [property: Range(1, 100)] int Age
);
//  В этом случае перед названием атрибута указывается оператор property:

//  Таким образом, вместо кучи условных конструкций для проверки значений свойств модели мы можем использовать один метод
//  Validator.TryValidateObject(), а все правила валидации определить в виде атрибутов.

public class User
{
    [Required]
    [StringLength (50,MinimumLength = 3)]
    public string Name { get; set; }
    [Range(1,100)]
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
//  Все правила валидации модели в System.ComponentModel.DataAnnotations определяются в виде атрибутов. В данном
//  случае используются три атрибута: классы RequiredAttribute, StringLengthAttribute и RangeAttribute. В коде
//  необязательно использовать суффикс Attribute, поэтому он, как правило, отбрасывается. Атрибут Required требует
//  обзательного наличия значения. Атрибут StringLength устанавливает максимальную и минимальную длину строки, а атрибут
//  Range устанавливает диапазон приемлемых значений.
