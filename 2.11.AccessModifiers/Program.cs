//  В языке C# применяются следующие модификаторы доступа:

//  private: закрытый или приватный компонент класса или структуры. Приватный компонент доступен
//  только в рамках своего класса или структуры.

//  private protected: компонент класса доступен из любого места в своем классе или в производных
//  классах, которые определены в той же сборке.

//  protected: такой компонент класса доступен из любого места в своем классе или в производных
//  классах. При этом производные классы могут располагаться в других сборках.

//  internal: компоненты класса или структуры доступен из любого места кода в той же сборке, однако
//  он недоступен для других программ и сборок.

//  protected internal: совмещает функционал двух модификаторов protected и internal. Такой компонент
//  класса доступен из любого места в текущей сборке и из производных классов, которые могут
//  располагаться в других сборках.

//  public: публичный, общедоступный компонент класса или структуры. Такой компонент доступен
//  из любого места в коде, а также из других программ и сборок.
using MyLibrary;
class State
{
    // все равно, что private string defaultVar;
    string defaultVar = "default";
    // поле доступно только из текущего класса
    private string privateVar = "private";
    // доступно из текущего класса и производных классов, которые определены в этом же проекте
    protected private string protectedPrivateVar = "protected private";
    // доступно из текущего класса и производных классов
    protected string protectedVar = "protected";
    // доступно в любом месте текущего проекта
    internal string internalVar = "internal";
    // доступно в любом месте текущего проекта и из классов-наследников в других проектах
    protected internal string protectedInternalVar = "protected internal";
    // доступно в любом месте программы, а также для других программ и сборок
    public string publicVar = "public";

    // по умолчанию имеет модификатор private
    void Print() => Console.WriteLine(defaultVar);

    // метод доступен только из текущего класса
    private void PrintPrivate() => Console.WriteLine(privateVar);

    // доступен из текущего класса и производных классов, которые определены в этом же проекте
    protected private void PrintProtectedPrivate() => Console.WriteLine(protectedPrivateVar);

    // доступен из текущего класса и производных классов
    protected void PrintProtected() => Console.WriteLine(protectedVar);

    // доступен в любом месте текущего проекта
    internal void PrintInternal() => Console.WriteLine(internalVar);

    // доступен в любом месте текущего проекта и из классов-наследников в других проектах
    protected internal void PrintProtectedInternal() => Console.WriteLine(protectedInternalVar);

    // доступен в любом месте программы, а также для других программ и сборок
    public void PrintPublic() => Console.WriteLine(publicVar);
}

class StateConsumer
{
    public void PrintState()
    {
        State newState = new State();

        // переменная internalVar с модификатором internal доступна из любого места текущего проекта
        Console.WriteLine(newState.internalVar);

        // переменная protectedInternalVar так же доступна из любого места текущего проекта
        Console.WriteLine(newState.protectedInternalVar);

        // переменная publicVar общедоступна
        Console.WriteLine(newState.publicVar);

        newState.PrintInternal();

        newState.PrintProtectedInternal();

        newState.PrintPublic();
    }
}