public class Ball
{
    public int X;
    public int Y;
    public World Monde;
    public Character? Porteur;

    public Ball(int x, int y, World monde)
    {
        X = x;
        Y = y;
        Monde = monde;
    }

    public bool CheckForPoint()
    {
        if (Porteur != null)
        {
            if (X < 1 && Porteur.Equipe == Monde.Equipe1)
            {
                Monde.Equipe1.Joueur.Score += 1;
            }
            if (X >= Monde.XSize - 1 && Porteur.Equipe == Monde.Equipe2)
            {
                Monde.Equipe2.Joueur.Score += 1;
            }
        }
        return true;
    }

}