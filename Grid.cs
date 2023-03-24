public class Grid
{
    public string[] Infill;
    public int[,] Grille;
    public Grid(int x, int y)
    {
        Grille = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Grille[i, j] = 0;
            }
        }
        Infill = new string[3];
    }
    public bool Check(int x, int y)
    {
        if (Grille[x, y] != 0)
        {
            return true;
        }
        return false;
    }

    public void FillGrid(int x, int y, int num)
    {
        Grille[x, y] = num;
    }
}