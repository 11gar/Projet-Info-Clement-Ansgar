public class Team
{
    public World Monde;
    public Grid Grille;
    public List<Character> Personnages;
    public Team(World monde)
    {
        Monde = monde;
        Personnages = new List<Character>();
        Grille = new Grid(Monde.XSize, monde.YSize);
    }


}