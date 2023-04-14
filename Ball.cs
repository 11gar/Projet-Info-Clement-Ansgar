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

}