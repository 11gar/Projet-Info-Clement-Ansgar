public class World
{
    int CaseSize;
    string characters = "┌ ┐ └ ┘ │ ┼ ─ ";
    public int XSize;
    public int YSize;
    public Grid? Environnement;
    public Team Equipe1;
    public Team Equipe2;
    public List<Grid> Grilles;
    public World(int x, int y, int size)
    {
        CaseSize = size;
        XSize = x;
        YSize = y;
        Environnement = new Grid(x, y);

        Grilles = new List<Grid>();
        Grilles.Add(Environnement);
        Equipe1 = new Team(this);
        Equipe2 = new Team(this);
        Grilles.Add(Equipe1.Grille);
        Grilles.Add(Equipe2.Grille);


    }
    public void Show()
    {
        for (int i = 0; i < XSize; i++)
        {
            for (int j = 0; j < YSize; j++)
            {
                Console.Write("┼───────");

            }
            Console.Write("┼");
            Console.WriteLine("");
            FillLine(i);

        }
        for (int j = 0; j < YSize; j++)
        {
            Console.Write("┼───────");
        }
        Console.Write("┼");
        Console.WriteLine("");
    }

    public void ShowLite(Smallgrid G)
    {
        for (int i = 0; i < XSize; i++)
        {
            for (int j = 0; j < YSize; j++)
            {
                Console.Write("┼───");
            }
            Console.Write("┼");
            Console.WriteLine("");
            for (int j = 0; j < YSize; j++)
            {
                Console.Write("│");
                if (G.Check(i, j))
                {
                    Console.Write(G.Infill[0, G.Grille[i, j]]);

                }
                else
                {
                    Console.Write("   ");
                }
            }
            Console.Write("│");
            Console.WriteLine("");
        }
        for (int j = 0; j < YSize; j++)
        {
            Console.Write("┼───");
        }
        Console.Write("┼");
        Console.WriteLine("");
    }

    public void FillLine(int x)
    {
        bool alr = false;
        for (int k = 0; k < 3; k++)
        {
            for (int j = 0; j < YSize; j++)
            {
                Console.Write("│");
                foreach (Grid G in Grilles)
                {
                    if ((G.Check(x, j)) && (alr == false))
                    {
                        if (G.Grille[x, j].Above == null)
                        {
                            Console.Write(G.Infill[k, G.Grille[x, j].Id]);
                            alr = true;
                        }
                        else
                        {
                            if (G.Grille[x, j].Above.Above == null)
                            {
                                Console.Write(G.Infill[(int)Math.Floor((decimal)(k + 2) / 2), G.Grille[x, j].StackTabler()[k].Id]);
                                alr = true;
                            }
                            else
                            {
                                Console.Write(G.Infill[1, G.Grille[x, j].StackTabler()[k].Id]);
                                alr = true;
                            }
                        }
                    }
                }
                if (alr == false)
                {
                    Console.Write("       ");
                }
                alr = false;
            }
            Console.Write("│");
            Console.WriteLine("");

        }

    }

}
