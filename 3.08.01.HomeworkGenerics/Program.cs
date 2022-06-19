// Что неправильно в следующем коде? Исправьте его:
//      class Instantiator<T>
//      {
//          public T instance;
//          public Instantiator()
//          {
//               instance = new T();
//          }
//      }

class Instantiator<T>
{
    public T? instance;
    public Instantiator()
    {
        instance = default(T);
    }
}
