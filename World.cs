public class World
{
    int CaseSize;
    string characers = "┏ ┓ ┗ ┛ │ ┼ ─ ";
    public int XSize;
    public int YSize;
    public Grid? Environnement;
    public Grid? Equipe1;
    public Grid? Equipe2;
    List<Grid> Grilles;
    public World(int x, int y, int size)
    {
        CaseSize = size;
        XSize = x;
        YSize = y;
        Environnement = new Grid(x, y);
        Equipe1 = new Grid(x, y);
        Equipe2 = new Grid(x, y);
        Grilles = new List<Grid>();
        Grilles.Add(Environnement);
        Grilles.Add(Equipe1);
        Grilles.Add(Equipe2);
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
                        Console.Write(G.Infill[k]);
                        alr = true;
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
