public class World
{
    int CaseSize;
    string characters = "┌ ┐ └ ┘ │ ┼ ─ ";
    public int XSize;
    public int YSize;
    public Team Environnement;
    public Team Equipe1;
    public Team Equipe2;
    public List<Grid> Grilles;
    public Ball Balle;
    public World(int x, int y, int size)
    {
        CaseSize = size;
        XSize = x;
        YSize = y;
        Grilles = new List<Grid>();
        Equipe1 = new Team(this, 0);
        Equipe2 = new Team(this, YSize);
        Equipe1.Ennemis = Equipe2;
        Equipe2.Ennemis = Equipe1;
        Environnement = new Team(this, 0);
        Balle = new Ball(this.XSize / 2, this.YSize / 2, this);
        Grilles.Add(Environnement.Grille);
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
                    if (G.CheckActive(Math.Min(x + 1, XSize - 1), j) || G.CheckActive(x, Math.Min(j + 1, YSize - 1)) || G.CheckActive(Math.Max(x - 1, 0), j) || G.CheckActive(x, Math.Max(j - 1, 0)))
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    if (Balle.X == x && Balle.Y == j)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    if ((G.Check(x, j)) && (alr == false))
                    {
                        string str1 = G.Infill[2, G.Grille[x, j].Id].Substring(0, 3);
                        string str2 = G.Infill[2, G.Grille[x, j].Id].Substring(4, 3);
                        G.Infill[2, G.Grille[x, j].Id] = str1 + G.Grille[x, j].Hp.ToString()[0] + str2; //.Replace(' ', G.Grille[x, j].Hp.ToString()[0]);

                        if (G.Grille[x, j].SonTour)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        if (G.Grille[x, j].Above == null)
                        {
                            Console.Write(G.Infill[k, G.Grille[x, j].Id]);
                            alr = true;
                        }
                        else
                        {
                            if (G.Grille[x, j].Above.Above == null)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                if (G.Grille[x, j].StackTabler()[k].SonTour)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                }
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
                    Console.ForegroundColor = ConsoleColor.White;

                }
                if (alr == false)
                {
                    Console.Write("       ");

                }
                Console.BackgroundColor = ConsoleColor.Black;
                alr = false;
            }
            Console.Write("│");
            Console.WriteLine("");

        }

    }

}
