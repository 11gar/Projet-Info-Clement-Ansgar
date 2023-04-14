public class Team
{
    public Player Joueur;
    public int YStart;
    public Team Ennemis;
    public World Monde;
    public Grid Grille;
    public Character[] Personnages;
    public Team(World monde, int yStart)
    {
        Monde = monde;
        YStart = yStart;
        Grille = new Grid(Monde.XSize, monde.YSize);
    }


}