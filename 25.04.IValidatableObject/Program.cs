//  Самовалидация модели

//  Нам необязательно определять правила валидации модели в виде атрибутов. Мы можем применить к классу интерфейс
//  IValidatableObject и реализовать его метод Validate():
//public interface IValidatableObject
//{
//    /// <summary>Determines whether the specified object is valid.</summary>
//    /// <param name="validationContext">The validation context.
//    /// <returns>A collection that holds failed-validation information.</returns>
//    IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
//}

//Метод Validate в качестве параметра получает объект ValidationContext, который собственно и проводит непосредственную
//валидацию. В качестве результата метод должен возвращать коллекцию объектов ValidationResult, которые представляют
//результат валидации.

//Фактически при применении этого интерфейса класс будет сам себя валидировать.

//Итак, реализуем этот интерфейс в классе User:
using System.ComponentModel.DataAnnotations;

public class User : IValidatableObject
{
    public string Name { get; set; }

    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add(new ValidationResult("Не указано имя"));

        if (Name.Length < 2 || Name.Length > 20)
            errors.Add(new ValidationResult("Некорректная длина имени"));

        if (this.Age < 1 || this.Age > 100)
            errors.Add(new ValidationResult("Недопустимый возраст"));

        return errors;
    }
}
//  Здесь в методе Validate() проверяем значения свойств и, если свойство не проходит валидацию, добавляем в список
//  errors соответствующее сообщение об ошибке.

//  И в основной части программы мы также можем применять валидацию к объекту User:
class Program
{
    static void Main(string[] args)
    {
        Validate(new User("Bob", 41));
        Validate(new User("T", 120));
        Validate(new User("", 0));

        void Validate(User user)
        {
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
                Console.WriteLine("Пользователь прошел валидацию");
            Console.WriteLine();
        }
    }
}