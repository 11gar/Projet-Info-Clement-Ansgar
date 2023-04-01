public class Grid
{
    public string[,] Infill;
    public Character?[,] Grille;
    public Grid(int x, int y)
    {
        Grille = new Character[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Grille[i, j] = null;
            }
        }
        Infill = new string[3, 10];
    }
    public bool Check(int x, int y)
    {
        if (Grille[x, y] != null)
        {
            return true;
        }
        return false;
    }

    public void FillGrid(Character ch)
    {
        Grille[ch.X, ch.Y] = ch;
    }
}