//  Класс Word представляет слово, где свойство Target хранит перевод слова. А класс Dictionary представляет
//  словарь слов и хранит все слова в приватном массиве.

//  Добавьте в класс Dictionary индексатор таким образом, чтобы с помощью индексатора можно было по слову
//  получить или изменить его перевод.
Dictionary dict = new Dictionary();
Console.WriteLine(dict["blue"]);
dict["blue"] = "голубой";
Console.WriteLine(dict["blue"]);

class Word
{
    public string Source { get; }
    public string Target { get; set; }
    public Word(string source, string target)
    {
        Source = source;
        Target = target;
    }
}
class Dictionary
{
    Word[] words;
    public Dictionary()
    {
        words = new Word[]
        {
            new Word("red", "красный"),
            new Word("blue", "синий"),
            new Word("green", "зеленый")
        };
    }
    public string this[string word]
    {
        get
        {
            Word w1 = null;
            foreach (Word w in words)
            {
                if (w.Source == word)
                {
                    w1 = w;
                    break;
                }
            }
            return w1?.Target;
        }
        set
        {
            foreach (Word w in words)
            {
                if (w.Source == word)
                {
                    w.Target = value;
                    break;
                }
            }
        }
    }
}