public class Game
{
    public int? Tour;
    public int PersoParJoueur;
    public int maxPlayers;
    public Smallgrid Start;
    public World Monde;
    public Player J1;
    public Player J2;
    public Game(int x, int y)
    {
        maxPlayers = x;
        Monde = new World(x, y, 3);
        Start = new Smallgrid(x, y, this);
        J1 = new Player(Monde.Equipe1);
        J2 = new Player(Monde.Equipe2);


        for (int j = 0; j < maxPlayers + 1; j++)
        {
            Console.Write("a");
            if (j < 10)
            {
                Start.Infill[0, j] = $" {j} ";
            }
            else if (j < 100)
            {
                Start.Infill[0, j] = $"{j} ";
            }
            else
            {
                Start.Infill[0, j] = $"{j}";
            }
        }


        Monde.Environnement.Infill[0, 0] = "  XXX  "; // l. 12 à XX : Designs des différents joueurs
        Monde.Environnement.Infill[1, 0] = "  X X  ";
        Monde.Environnement.Infill[2, 0] = "  XXX  ";

        Monde.Equipe1.Grille.Infill[0, 0] = "  ___  ";
        Monde.Equipe1.Grille.Infill[1, 0] = " (` ´) ";
        Monde.Equipe1.Grille.Infill[2, 0] = " /| |\\ ";

        Monde.Equipe1.Grille.Infill[0, 1] = "  ___  ";
        Monde.Equipe1.Grille.Infill[1, 1] = " [°|°] ";
        Monde.Equipe1.Grille.Infill[2, 1] = " /| |\\ ";

        Monde.Equipe2.Grille.Infill[0, 0] = "   o   ";
        Monde.Equipe2.Grille.Infill[1, 0] = " /(_)\\ ";
        Monde.Equipe2.Grille.Infill[2, 0] = "  / \\  ";

        Monde.Equipe2.Grille.Infill[0, 1] = "  |T|  ";
        Monde.Equipe2.Grille.Infill[1, 1] = " /[_]\\ ";
        Monde.Equipe2.Grille.Infill[2, 1] = "  / \\  ";


    }

    public void Play()
    {
        int tailleLigne = 100;
        Console.Clear();
        WriteSlow("Bienvenue dans cette partie de Poulpy Bowl !", 20, 40);
        Console.WriteLine("");
        WriteSlow("Joueur 1, veuillez entrer votre nom :", 20, 40);
        Console.WriteLine("");
        J1.Nom = Console.ReadLine();
        WriteSlow("Joueur 2, veuillez entrer votre nom :", 20, 40);
        Console.WriteLine("");
        J2.Nom = Console.ReadLine();
        for (int i = 0; i < tailleLigne / 2; i++) // l. 42 à 64 : effet visuel de début de partie
        {
            System.Threading.Thread.Sleep(25);
            Console.Clear();
            WriteMultiple("#", i, 0);
            WriteMultiple(" ", tailleLigne - 2 * i, 0);
            WriteMultiple("#", i, 0);
        }
        for (int i = (tailleLigne / 2) - 1; i >= 0; i--)
        {
            System.Threading.Thread.Sleep(10);
            Console.Clear();
            WriteMultiple("#", i, 0);
            WriteMultiple("-", tailleLigne - 2 * i, 0);
            WriteMultiple("#", i, 0);
        }
        Console.Clear();
        WriteMultiple("-", tailleLigne / 2 - (J1.Nom.Length + J2.Nom.Length + 6) / 2, 0);
        Console.Write($" {J1.Nom} VS {J2.Nom} ");
        WriteMultiple("-", tailleLigne / 2 - (J1.Nom.Length + J2.Nom.Length + 6) / 2, 0);
        Console.WriteLine("");
        WriteMultiple("- ", tailleLigne / 2, 5);
        Console.Write("-");
        Console.WriteLine("");
        Console.WriteLine("");
        WriteSlow($"Combien de joueurs par équipe ? (max : {maxPlayers})", 20, 65);
        Console.WriteLine("");
        PersoParJoueur = int.Parse(Console.ReadLine());
        while (PersoParJoueur > maxPlayers)
        {
            Console.Clear();
            Console.WriteLine($"Erreur : max {maxPlayers} joueurs");
            PersoParJoueur = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < PersoParJoueur; i++)
        {
            Start.FillGrid((i + 1) * Monde.XSize / (PersoParJoueur + 1), 1, i + 1);
            Start.FillGrid((i + 1) * Monde.XSize / (PersoParJoueur + 1), Monde.YSize - 2, i + 1);
        }
        Console.Clear();
        WriteSlow($"Voici la configuration recommandée pour ce nombre de joueur par équipe.", 20, 40);
        Console.WriteLine("");
        Monde.ShowLite(Start);


    }


    public void WriteMultiple(string S, int k, int delay) //Ecrit le string S, k fois.
    {
        for (int j = 0; j < k; j++)
        {
            Console.Write(S);
            System.Threading.Thread.Sleep(delay);
        }
    }

    public void WriteSlow(string S, int delay, int spaceDelay)
    {
        foreach (char c in S)
        {
            if (c == ' ')
            {
                System.Threading.Thread.Sleep(spaceDelay);
            }
            Console.Write(c);
            System.Threading.Thread.Sleep(delay);
        }
    }
}