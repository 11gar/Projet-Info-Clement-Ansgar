public class Player
{
    public string Nom;
    public int Score;
    public Game Partie;
    public Team Equipe;
    public Player(Team equipe)
    {
        Equipe = equipe;
        Score = 0;
    }
}