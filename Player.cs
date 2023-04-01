public class Player
{
    public string Nom;
    public Game Partie;
    public Team Equipe;
    public Player(Team equipe)
    {
        Equipe = equipe;
    }
}