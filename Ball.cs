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
            Console.WriteLine(Porteur.Hp);
            Console.WriteLine(Porteur.X);
            if (Porteur.X >= (Monde.XSize - 1) && Porteur.Equipe == Monde.Equipe1)
            {
                Monde.Equipe1.Joueur.Score += 1;
                return true;
            }
            if (Porteur.X < 1 && Porteur.Equipe == Monde.Equipe2)
            {
                Monde.Equipe2.Joueur.Score += 1;
                return true;
            }
        }
        return false;

    }

}