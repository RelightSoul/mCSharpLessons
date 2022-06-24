// Для упрощения работы с датами и временем в .NET 6 были добавлены две дополнительные структуры -
// DateOnly и TimeOnly.

#region DateOnly
//  Структура DateOnly представляет дату. Для создания структуры можно использовать ряд ее конструкторов.
//      DateOnly()
//      DateOnly(int year, int month, int day)
//      DateOnly(int year, int month, int day, System.Globalization.Calendar calendar)

//  При использовании конструктора без параметров структура инициализируется датой 01.01.0001:
using System.Globalization;

DateOnly someDate = new DateOnly();   // 01.01.0001

//  Вторая версия конструктора принимает год, месяц и число, которыми инициализируется структура:
DateOnly someDate2 = new DateOnly(1989,12,29);
Console.WriteLine(someDate2);         // 29.12.1989

//  Третья версия конструктора в дополнение к году, месяцу и числу, также принимает объект календаря -
//  объект System.Globalization.Calendar, относительно которого будет расcчитываться дата. Класс Calendar
//  является абстрактным, но .NET предоставляет ряд встроенных типов календарей. Например, расcчитаем
//  дату относительно юлианского календаря:

DateOnly julDate = new DateOnly(1989, 12, 29, new JulianCalendar());
Console.WriteLine(julDate);   // 11.01.1990
//  В данном случае для .NET переданная в конструктор дата - 06.01.2022 расценивается как дата юлианского
//  календаря. При выводе на консоль мы видим тот же день только относительно григорианского календаря.
#endregion

#region Свойства DateOnly
//Свойства DateOnly
//С помощью свойств структуры можно получить отдельные составляющие даты:

//Day: возвращает день даты

//DayNumber: возвращает количество прошедших дней с 1 января 0001 года относительно григорианского календаря

//DayOfWeek: возвращает день недели

//DayOfYear: возвращает день года

//MaxValue: возвращает максимально возможную дату (статическое свойство)

//MinValue: возвращает самую раннюю возможную дату (статическое свойство)

//Month: возвращает месяц

//Year: возвращает год
DateOnly nDate = new DateOnly(2022,1,6);
Console.WriteLine(nDate.Day);         // 6
Console.WriteLine(nDate.DayNumber);   // 738160
Console.WriteLine(nDate.DayOfWeek);   // Thursday
Console.WriteLine(nDate.DayOfYear);   // 6
Console.WriteLine(nDate.Month);       // 1
Console.WriteLine(nDate.Year);        // 2022
#endregion

#region Методы DateOnly
//С помощью методов DateOnly можно производить некоторые операции с датами. Некоторые из них:
//AddDays(int days): добавляет к дате некоторое количество дней
//AddMonths(int months): добавляет к дате некоторое количество месяцев
//AddYears(int years): добавляет к дате некоторое количество лет
//ToDateTime(TimeOnly): возвращает объект DateTime, который в качестве даты исппользует текущий объект DateOnly,
//а в качестве времени - значение параметра в виде TimeOnly
//ToLongDateString(): выводит текущий объект DateOnly в виде подробной даты
//ToShortDateString(): выводит текущий объект DateOnly в виде сжатой даты

//Также в классе есть ряд статических методов. Некоторые из них:
//FromDateTime(DateTime dateTime): на основе значения DateTime, переданного через параметр,
//создает и возвращает объект DateOnly
//FromDayNumber(int days): на основе количества дней создает и возвращает объект DateOnly
//Parse(string date): конвертирует строковое представление даты в объект DateOnly
//ParseExact(string date, string format): конвертирует строковое представление даты в объект DateOnly,
//применяя определенный формат
//TryParse(String, DateOnly): конвертирует строковое представление даты в объект DateOnly.
//При успешной конвертации возвращает true, а параметр типа DateOnly содержит созданную дату
//TryParseExact(String, String, DateOnly): конвертирует строковое представление даты в объект
//DateOnly, применяя определенный формат. При успешной конвертации возвращает true, а параметр
//типа DateOnly содержит созданную дату
#endregion

#region TimeOnly
//  Структура TimeOnly представляет время в диапазоне от 00:00:00 до 23:59:59.9999999. Для создания
//  структуры можно использовать ряд ее конструкторов.
//      TimeOnly()
//      TimeOnly(long ticks)
//      TimeOnly(int hour, int minute)
//      TimeOnly(int hour, int minute, int second)
//      TimeOnly(int hour, int minute, int second, int millisecond)

//  При использовании конструктора без параметров структура инициализируется временем 0.00:
TimeOnly time = new TimeOnly();
Console.WriteLine(time); // 0:00

//  Дополнительно с помощью других версий конструктора можно установить количество часов, минут, секунд
//  и миллисекунд:
TimeOnly time1 = new TimeOnly(4, 30);
Console.WriteLine(time1);   // 4: 30

TimeOnly time2 = new TimeOnly(14, 23, 30);
Console.WriteLine(time2);   // 14: 23
#endregion

#region Свойства TimeOnly
//С помощью свойств структуры можно получить отдельные составляющие времени:

//Hour: возвращает количество часов

//Minute: возвращает количество минут

//Second: возвращает количество секунд

//Millisecond: возвращает количество миллисекунд

//Ticks: возвращает количество тиков

//MaxValue: возвращает максимально возможное время (статическое свойство)

//MinValue: возвращает минимально возможное время(статическое свойство)
TimeOnly time3 = new TimeOnly(14, 23, 30);
Console.WriteLine(time3.Hour);       // 14
Console.WriteLine(time3.Minute);     // 23
Console.WriteLine(time3.Second);     // 30
#endregion

#region Методы TimeOnly
//С помощью методов TimeOnly можно производить некоторые операции с временем. Некоторые из них:
//AddHours(double hours): добавляет к времени некоторое количество часов
//AddMinutes(double minutes): добавляет к времени некоторое количество минут
//Add(TimeSpan value): добавляет время из объекта TimeSpan
//ToLongTimeString(): выводит текущий объект TimeOnly в виде подробного времени
//ToShortTimeString(): выводит текущий объект TimeOnly в виде сжатого времени

//Также в классе есть ряд статических методов. Некоторые из них:
//FromDateTime(DateTime dateTime): на основе значения DateTime, переданного через параметр, создает и
//возвращает объект TimeOnly
//FromTimeSpan(TimeSpan value): на основе объекта TimeSpan создает и возвращает объект TimeOnly
//Parse(string time): конвертирует строковое представление времени в объект TimeOnly
//ParseExact(string timee, string format): конвертирует строковое представление времени в объект TimeOnly,
//применяя определенный формат
//TryParse(string time, TimeOnly result): конвертирует строковое представление времени в объект TimeOnly.
//При успешной конвертации возвращает true, а параметр типа TimeOnly содержит сконвертированное время
//TryParseExact(string time, string format, TimeOnly result): конвертирует строковое представление времени
//в объект TimeOnly, применяя определенный формат. При успешной конвертации возвращает true, а параметр
//типа TimeOnly содержит сконвертированное время
TimeOnly time4 = TimeOnly.Parse("06:33:22");
Console.WriteLine(time4);        // 6:33
time4 = time4.AddHours(1);       // 7:33
time4 = time4.AddMinutes(-23);   // 7:10

Console.WriteLine(time4.ToShortTimeString());  // 7:10
Console.WriteLine(time4.ToLongTimeString());   // 7:10:22
#endregion
