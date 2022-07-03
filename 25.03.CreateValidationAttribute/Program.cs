//  Создание своих атрибутов валидации

//  Несмотря на то, что .NET предоставляет нам большой набор встроенных атрибутов валидации, может потребоваться более
//  изощренные в плане логики атрибуты. И в этом случае мы можем определить свои классы атрибутов.

//  Для создания атрибута нам надо унаследовать свой класс от класса ValidationAttribute и реализовать его метод IsValid():

//  При создании атрибута надо понимать, к чему именно он будет применяться - к свойству модели или ко всей модели в целом.
//  Рассмотрим обе ситуации.

#region Атрибут уровня свойства
using System.ComponentModel.DataAnnotations;

public class UserNameAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string userName)
        {
            if (userName != "admin")    // если имя не равно admin
                return true;
            else
                ErrorMessage = "Некорректное имя: admin";
        }
        return false;
    }
}
public class User
{
    [UserName]
    public string Name { get; set; }

    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
//  Класс атрибута наследуется от класса ValidationAttribute и реалует его метод IsValid(). В данный метод передается
//  значение, которое валидируется.

//  Данный атрибут будет применяться к строковому свойству, поэтому в метод IsValid(object? value) в качестве value будет
//  передаваться строка. Поэтому в ходе программы мы можем преобразовать значение value к строке:
//  if (value is string userName)
//  Далее в методе мы проверяем, равно ли значение свойства строке "admin". Если НЕ равно, то возвращаем true. Это значит,
//  что свойство, к которому будет применяться данный атрибут, прошло валидацию. Если же строка равна "admin", то возвращаем
//  false и устанавливаем сообщение об ошибке. Если же в метод передана не строка, также возвращаем false.
#endregion

#region Атрибуты валидации уровня класса
//  Подобным образом определим еще один атрибут, который будет применяться ко всему классу User в целом:
public class UserValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is User2 user)
        {
            if (user.Name == "Tom" && user.Age == 37)
            {
                ErrorMessage = "Имя не должно быть Tom и возраст одновременно не должен быть равен 37";
                return false;
            }
            return true;
        }
        return false;
    }
}
//  Поскольку атрибут будет применяться ко всей модели, то в метод IsValid в качестве параметра value будет передаваться
//  объект User. Как правило, атрибуты, которые применяются ко всей модели, валидируют сразу комбинацию свойств класса.
//  В данном случае смотрим, чтобы имя и возраст одновременно не были равны "Tom" и 37.
[UserValidation]
public class User2
{
    public string Name { get; set; }

    public int Age { get; set; }

    public User2(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
#endregion

#region Передача в атрибут значений
//  Выше оба атрибута сравнивали валидируемое значение с некоторым жестко заданным в коде набором значений. Однако мы
//  можем определить через конструктор механизм передачи в атрибут значений из вне. Например, изменим атрибут UserValidation:
public class UserValidation2Attribute : ValidationAttribute
{
    public string InvalidName { get; set; }
    public int InvalidAge { get; set; }
    public UserValidation2Attribute(string name, int age)
    {
        InvalidName = name;
        InvalidAge = age;
    }
    public override bool IsValid(object? value)
    {
        if (value is User3 user)
        {
            if (user.Name == InvalidName && user.Age == InvalidAge)
            {
                ErrorMessage = $"Имя не должно быть равно {InvalidName} и возраст одновременно не должен быть равен {InvalidAge}";
                return false;
            }
            return true;
        }
        return false;
    }
}
[UserValidation2("Bob", 41)]
public class User3
{
    public string Name { get; set; }

    public int Age { get; set; }

    public User3(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
#endregion