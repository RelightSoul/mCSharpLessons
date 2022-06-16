namespace _2._10.MyLibrary
{
    public class Person
    {
        string name;
        public Person(string name)
        {
            this.name = name;
        }

        public void Print() => Console.WriteLine($"Name = {name}");
    }
    public class PublicState
    {
        internal void PringtInternal() => Console.WriteLine("Internal");
        protected internal void PrintProtectedInternal() => Console.WriteLine("Protected internal");
        public void PrintPublic() => Console.WriteLine("Public");
    }
    class DefaultState { }
    internal class InternalState { }
}

//После компиляции библиотеки классов в папке проекта в каталоге bin/Debug/net6.0 мы
//сможем найти скомпилированный файл dll (MyLib.dll). Подключим его в основной проект.
//Для этого в основном проекте нажмем правой кнопкой на узел Dependencies и в контекстном
//меню выберем пункт Add Project Reference...

//Далее нам откроется окно для добавления библиотек. В этом окне выберем пункт Solution (Решение),
//который позволяет увидеть все библиотеки классов из проектов текущего решения, поставим
//отметку рядом с нашей библиотекой и нажмем на кнопку OK

// После успешного подключения библиотеки в главном проекте изменим файл Program.cs, чтобы
// он использовал класс Person из библиотеки классов