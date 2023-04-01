public class Smallgrid
{
    public Game Jeu;
    public string[,] Infill;
    public int[,] Grille;
    public Smallgrid(int x, int y, Game jeu)
    {
        Jeu = jeu;
        Grille = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {

                Grille[i, j] = 0;
            }
        }
        Infill = new string[Jeu.maxPlayers + 1, jeu.maxPlayers + 1];
    }
    public bool Check(int x, int y)
    {
        if (Grille[x, y] != 0)
        {
            return true;
        }
        return false;
    }

    public void FillGrid(int x, int y, int k)
    {
        Grille[x, y] = k;
    }
}