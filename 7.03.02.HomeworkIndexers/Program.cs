// При попытке доступа по индексу, который выходит за границы массива мы получим исключение.
// Исправьте код индексатора таким образом, чтобы он позволял избежать использования некорректных индексов.
FootballTeam team = new FootballTeam(new Player[11]);
team[0] = new Player("Alex", 7);
team[1] = new Player("Mark", 13);
team[2] = new Player("Vova", 44);
Console.WriteLine("Name {0}, Number {1}", team[1]?.Name, team[1]?.Number);
team[20] = new Player("Vova", 44);
Console.WriteLine(team[20]?.Name);

Console.ReadLine();


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
    public Player? this[int number]
    {
        get
        {
            if (0 <= number && number < team.Length)
                return team[number];
            else
                return null;
        }
        set
        {
            if (0 <= number && number < team.Length)
            {
                team[number] = value;
            }
        }
    }
}