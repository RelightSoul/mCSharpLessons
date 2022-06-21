// Определите класс футболиста, который содержит имя футболиста и его номер на поле.
// И определите класс футбольной команды, который хранит 11 футболистов в виде массива
// и обеспечивает доступ к этим футболистам через индексатор.
FootballTeam team = new FootballTeam(new Player[11]);
team[0] = new Player("Alex",7);
team[1] = new Player("Mark", 13);
team[2] = new Player("Vova", 44);
Console.WriteLine("Name {0}, Number {1}",team[1].Name, team[1].Number);


class Player
{
    public string Name { get; set; }
    public int Number { get; set; }
    public Player(string name, int number)
    {
        Name = name;
        Number = number;
    }
}
class FootballTeam
{
    Player[] team;
    public FootballTeam(Player[] team)
    {
        this.team = team;
    }
    public Player this[int number]
    {
        get => team[number];
        set => team[number] = value;
    }
}