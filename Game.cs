public class Game
{
    public int? Tour;
    public int PersoParJoueur;
    public int maxPlayers;
    public Smallgrid Start;
    public World Monde;
    public Player J1;
    public Player J2;
    public List<Player> Joueurs;
    int[] yPourJoueurAuDepart;
    public Game(int x, int y)
    {
        maxPlayers = x;
        Monde = new World(x, y, 3);
        Start = new Smallgrid(x, y, this);
        J1 = new Player(Monde.Equipe1);
        J2 = new Player(Monde.Equipe2);
        Joueurs = new List<Player>();
        Joueurs.Add(J1);
        Joueurs.Add(J2);
        Monde.Equipe1.Joueur = J1;
        Monde.Equipe2.Joueur = J2;
        yPourJoueurAuDepart = new int[2];
        yPourJoueurAuDepart[0] = 1;
        yPourJoueurAuDepart[1] = Monde.YSize - 2;

        for (int j = 0; j < maxPlayers + 1; j++)
        {
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


        Monde.Environnement.Grille.Infill[0, 0] = "   /\\  "; // l. 12 à XX : Designs des différents joueurs
        Monde.Environnement.Grille.Infill[1, 0] = " /\\__\\ ";
        Monde.Environnement.Grille.Infill[2, 0] = "/    \\\\";

        Monde.Equipe1.Grille.Infill[0, 0] = "  ___  ";
        Monde.Equipe1.Grille.Infill[1, 0] = " (` ´) ";
        Monde.Equipe1.Grille.Infill[2, 0] = " /| |\\ ";

        Monde.Equipe1.Grille.Infill[0, 1] = "  ___  ";
        Monde.Equipe1.Grille.Infill[1, 1] = " [°|°] ";
        Monde.Equipe1.Grille.Infill[2, 1] = " /| |\\ ";

        Monde.Equipe1.Grille.Infill[0, 2] = "  ___  ";
        Monde.Equipe1.Grille.Infill[1, 2] = " (> <) ";
        Monde.Equipe1.Grille.Infill[2, 2] = " // \\\\ ";

        Monde.Equipe1.Grille.Infill[0, 3] = " _/-\\_◊";
        Monde.Equipe1.Grille.Infill[1, 3] = " (O-O)|";
        Monde.Equipe1.Grille.Infill[2, 3] = " /| |\\|";

        Monde.Equipe1.Grille.Infill[0, 4] = " _[O]_U";
        Monde.Equipe1.Grille.Infill[1, 4] = " (` ´)|";
        Monde.Equipe1.Grille.Infill[2, 4] = " /| \\ |";

        Monde.Equipe2.Grille.Infill[0, 0] = "   o   ";
        Monde.Equipe2.Grille.Infill[1, 0] = " /(_)\\ ";
        Monde.Equipe2.Grille.Infill[2, 0] = "  / \\  ";

        Monde.Equipe2.Grille.Infill[0, 2] = "   \"   ";
        Monde.Equipe2.Grille.Infill[1, 2] = " /(_)\\ ";
        Monde.Equipe2.Grille.Infill[2, 2] = "  / \\  ";

        Monde.Equipe2.Grille.Infill[0, 1] = "  |T|  ";
        Monde.Equipe2.Grille.Infill[1, 1] = " /[_]\\ ";
        Monde.Equipe2.Grille.Infill[2, 1] = "  / \\  ";

        Monde.Equipe2.Grille.Infill[0, 3] = "  /ô\\ ◊";
        Monde.Equipe2.Grille.Infill[1, 3] = " /(_)\\|";
        Monde.Equipe2.Grille.Infill[2, 3] = " |/ \\||";

        Monde.Equipe2.Grille.Infill[0, 4] = "  _o_ U";
        Monde.Equipe2.Grille.Infill[1, 4] = " /(_)\\|";
        Monde.Equipe2.Grille.Infill[2, 4] = "  / \\ |";
    }

    public void Play()
    {
        Setup();
        Partie();
    }

    public void TestGame()
    {
        Sprinter w1 = new Sprinter(5, 8, Monde, Monde.Equipe1);
        Warrior w2 = new Warrior(6, 8, Monde, Monde.Equipe1);
        Turn(w1);
        Turn(w2);
    }

    public void Partie()
    {
        while (J1.Score < 2 || J2.Score < 2)
        {
            for (int j = 0; j < PersoParJoueur; j++)
            {
                foreach (Player J in Joueurs)
                {
                    Turn(J.Equipe.Personnages[j]);
                    if (Monde.Balle.CheckForPoint())
                    {
                        ResetRound();
                    }
                }
            }
        }

    }

    public void ResetRound()
    {
        for (int j = 0; j < 2; j++)
        {
            foreach (Character C in Joueurs[j].Equipe.Personnages)
            {
                C.Above = null;
                C.Under = null;
                C.Balle = null;
            }
            Monde.Balle.Porteur = null;
            for (int k = 0; k < Monde.XSize; k++)
            {
                if (Start.Grille[k, 1] != 0)
                {
                    Joueurs[j].Equipe.Personnages[Start.Grille[k, 1] - 1].Tp(k, yPourJoueurAuDepart[j]);
                }
            }
        }
    }
    public void Setup()
    {
        string rep;
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

        J1.Equipe.Personnages = new Character[PersoParJoueur];
        J2.Equipe.Personnages = new Character[PersoParJoueur];

        for (int i = 0; i < PersoParJoueur; i++)
        {
            Start.FillGrid((i + 1) * Monde.XSize / (PersoParJoueur + 1), 1, i + 1);
            Start.FillGrid((i + 1) * Monde.XSize / (PersoParJoueur + 1), Monde.YSize - 2, i + 1);
        }
        Console.Clear();
        WriteSlow($"Voici la configuration recommandée pour ce nombre de joueur par équipe.", 20, 40);
        Console.WriteLine("");
        Monde.ShowLite(Start);
        for (int k = 0; k < Monde.XSize; k++)
        {
            if (Start.Grille[k, 1] != 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteSlow($"{Joueurs[j].Nom}, choisissez la classe de votre joueur {Start.Grille[k, 1]}", 5, 5);
                    Console.WriteLine("");
                    rep = Console.ReadLine();
                    rep.ToLower();
                    switch (rep)
                    {
                        case "warrior":
                            Joueurs[j].Equipe.Personnages[Start.Grille[k, 1] - 1] = new Warrior(k, yPourJoueurAuDepart[j], Monde, Joueurs[j].Equipe);
                            break;
                        case "tank":
                            Joueurs[j].Equipe.Personnages[Start.Grille[k, 1] - 1] = new Tank(k, yPourJoueurAuDepart[j], Monde, Joueurs[j].Equipe);
                            break;
                        case "sprinter":
                            Joueurs[j].Equipe.Personnages[Start.Grille[k, 1] - 1] = new Sprinter(k, yPourJoueurAuDepart[j], Monde, Joueurs[j].Equipe);
                            break;
                        case "summoner":
                            Joueurs[j].Equipe.Personnages[Start.Grille[k, 1] - 1] = new Summoner(k, yPourJoueurAuDepart[j], Monde, Joueurs[j].Equipe);
                            break;
                        case "miner":
                            Joueurs[j].Equipe.Personnages[Start.Grille[k, 1] - 1] = new Miner(k, yPourJoueurAuDepart[j], Monde, Joueurs[j].Equipe);
                            break;
                        default:
                            Console.WriteLine("Type non valide, le personnage sera un Warrior par défaut.");
                            Joueurs[j].Equipe.Personnages[Start.Grille[k, 1] - 1] = new Warrior(k, yPourJoueurAuDepart[j], Monde, Joueurs[j].Equipe);
                            break;
                    }
                }
            }
        }
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

    public void Turn(Character C)
    {
        int Action;
        C.SonTour = true;
        C.Lowest().Active = true;
        C.Active = true;
        //Console.Clear();
        Monde.Show();
        WriteSlow($"{C.Equipe.Joueur.Nom}, quelle est votre action ?", 5, 10);
        Capacite(C);
        Console.WriteLine("");
        Action = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        int x = 0;
        int y = 0;
        if (Action != 3)
        {
            int pos;
            Console.Write("Vous utilisez votre capacité sur la case :");
            Console.WriteLine("");
            Console.WriteLine("   [1]");
            Console.WriteLine("[2]   [3]");
            Console.WriteLine("   [4]");
            Console.WriteLine("");
            pos = int.Parse(Console.ReadLine());

            switch (pos)
            {
                case 1:
                    x = -1;
                    break;
                case 2:
                    y = -1;
                    break;
                case 3:
                    y = 1;
                    break;
                case 4:
                    x = 1;
                    break;

            }
        }

        switch (Action)
        {
            case 1:
                C.Move(C.X + x, C.Y + y);
                break;
            case 2:
                C.Attack(C.X + x, C.Y + y);
                break;
            case 3:
                if (C.Balle != null)
                {
                    C.Passe(C.Nearest());
                }
                break;
            case 4:
                C.SpecialAbility(C.X + x, C.Y + y);
                break;
        }
        C.Active = false;
        C.Lowest().Active = false;
        C.SonTour = false;

    }

    public void Capacite(Character C)
    {
        Console.Write("   Déplacement (1)   |   Attaque (2)   ");
        if (C.Balle == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        Console.Write("   Passe (3)   ");
        Console.ForegroundColor = ConsoleColor.White;
        if (C.Special != null)
        {
            Console.Write($"|   {C.Special} (4)   ");
        }
    }

}