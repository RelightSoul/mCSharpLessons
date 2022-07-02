//  Работа с JSON
//  Сериализация в JSON. JsonSerializer

//JSON(JavaScript Object Notation) является одним из наиболее популярных форматов для хранения и передачи данных.
//И платформа .NET предоставляет функционал для работы с JSON.

//Основная функциональность по работе с JSON сосредоточена в пространстве имен System.Text.Json. Ключевым типом
//является класс JsonSerializer, который и позволяет сериализовать объект в json и, наоборот, десериализовать код
//json в объект C#.

//Для сохранения объекта в json в классе JsonSerializer определен статический метод Serialize() и его асинхронный
//двойник SerializeAsyc(), которые имеют ряд перегруженных версий. Некоторые из них:

//string Serialize(Object obj, Type type, JsonSerializerOptions options): сериализует объект obj типа type и
//возвращает код json в виде строки. Последний необязательный параметр options позволяет задать дополнительные опции сериализации

//string Serialize<T>(T obj, JsonSerializerOptions options): типизированная версия сериализует объект obj типа T
//и возвращает код json в виде строки.

//Task SerializeAsync(Stream utf8Json, Object obj, Type type, JsonSerializerOptions options): сериализует объект
//obj типа type и записывает его в поток utf8Json. Последний необязательный параметр options позволяет задать
//дополнительные опции сериализации

//Task SerializeAsync<T>(Stream utf8Json, T obj, JsonSerializerOptions options): типизированная версия сериализует
//объект obj типа T в поток utf8Json.

//Для десериализации кода json в объект C# применяется метод Deserialize() и его асинхронный двойник
//DeserializeAsync(), которые имеют различные версии. Некоторые из них:

//object? Deserialize(string json, Type type, JsonSerializerOptions options): десериализует строку json в объект
//типа type и возвращает десериализованный объект. Последний необязательный параметр options позволяет задать
//дополнительные опции десериализации

//T? Deserialize<T>(string json, JsonSerializerOptions options): десериализует строку json в объект типа T и
//возвращает его.

//ValueTask<object?> DeserializeAsync(Stream utf8Json, Type type, JsonSerializerOptions options, CancellationToken
//token): десериализует данные из потока utf8Json, который представляет объект JSON, в объект типа type. Последние
//два параметра необязательны: options позволяет задать дополнительные опции десериализации, а token устанавливает
//CancellationToken для отмены задачи. Возвращается десериализованный объект, обернутый в ValueTask

//ValueTask<T?> DeserializeAsync<T>(Stream utf8Json, JsonSerializerOptions options, CancellationToken token):
//десериализует данные из потока utf8Json, который представляет объект JSON, в объект типа T. Возвращается
//десериализованный объект, обернутый в ValueTask

//Рассмотрим применение класса на простом примере. Сериализуем и десериализуем простейший объект:
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

Person tom = new Person("Tom", 37);
string json = JsonSerializer.Serialize(tom);
Console.WriteLine(json);
Person? restoredPerson = JsonSerializer.Deserialize<Person>(json);
Console.WriteLine(restoredPerson?.Name);
//  Здесь вначале сериализуем с помощью метода JsonSerializer.Serialize() объект типа Person в стоку с кодом json.
//  Затем обратно получаем из этой строки объект Person посредством метода JsonSerializer.Deserialize().

//  Хотя в примере выше сериализовался/десериализовался объект класса, но подобным способом мы также можем
//  сериализовать/десериализовать структуры.

#region Некоторые замечания по сериализации/десериализации
//  Объект, который подвергается десериализации, должен иметь либо конструктор без параметров, либо конструктор,
//  для всех параметров которого в десериализуемом json-объекте есть значения (соответствие между параметрами
//  конструктора и свойствами json-объекта устанавливается на основе названий, причем регистр не играет значения).

//  Сериализации подлежат только публичные свойства объекта (с модификатором public).
#endregion

#region Запись и чтение файла json
//  Поскольку методы SerializeAsyc/DeserializeAsync могут принимать поток типа Stream, то соответственно мы можем
//  использовать файловый поток для сохранения и последующего извлечения данных:
using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
{
    Person tom2 = new Person("Tom", 37);
    await JsonSerializer.SerializeAsync<Person>(fs, tom2);
    Console.WriteLine("Data has been saved to file");
}
using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
{
    Person? person = await JsonSerializer.DeserializeAsync<Person>(fs);
    Console.WriteLine($"Name: {person?.Name}  Age: {person?.Age}");
}
//  В данном случае вначале данные сохраняются в файл user.json и затем считываются из него.
#endregion

#region Настройка сериализации с помощью JsonSerializerOptions
//По умолчанию JsonSerializer сериализует объекты в минимифицированный код. С помощью дополнительного параметра
//типа JsonSerializerOptions можно настроить механизм сериализации/десериализации, используя свойства
//JsonSerializerOptions. Некоторые из его свойств:

//AllowTrailingCommas: устанавливает, надо ли добавлять после последнего элемента в json запятую. Если равно true,
//запятая добавляется

//DefaultIgnoreCondition: устанавливает, будут ли сериализоваться/десериализоваться в json свойства со значениями
//по умолчанию

//IgnoreReadOnlyProperties: аналогично устанавливает, будут ли сериализоваться свойства, предназначенные только
//для чтения

//WriteIndented: устанавливает, будут ли добавляться в json пробелы (условно говоря, для красоты). Если равно
//true устанавливаются дополнительные пробелы
var options = new JsonSerializerOptions
{
    WriteIndented = true
};
string json2 = JsonSerializer.Serialize<Person>(tom, options);
Console.WriteLine(json2);
Person? restoredPerson2 = JsonSerializer.Deserialize<Person>(json2);
Console.WriteLine(restoredPerson2?.Name);
#endregion

#region Настройка сериализации с помощью атрибутов
//  По умолчанию сериализации подлежат все публичные свойства. Кроме того, в выходном объекте json все названия
//  свойств соответствуют названиям свойств объекта C#. Однако с помощью атрибутов JsonIgnore и JsonPropertyName.

//  Атрибут JsonIgnore позволяет исключить из сериализации определенное свойство. А JsonPropertyName позволяет
//  замещать оригинальное название свойства. Пример использования:
class Person2
{
    [JsonPropertyName("firstname")]
    public string Name { get; }
    [JsonIgnore]
    public int Age { get; set; }
    public Person2(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
//  В данном случае свойство Age будет игнорироваться, а для свойства Name будет использоваться псевдоним
//  "firstname".
#endregion

class Person
{
    public string Name { get; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}