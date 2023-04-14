public abstract class Character
{
    public string? Special;
    public bool Active;  //Active et SonTour ont presque la meme utilité mais Active permet de "tricher" pour optimiser le code. (voir l'affichage du monde dans World.cs, FillLine())
    public bool SonTour;
    public World Monde;
    public Team Equipe;
    public int X;
    public int Y;
    public int Strength;
    public int Hp;
    public int Id;
    public int NumOrder;
    public Ball? Balle;
    public Character? Above;
    public Character? Under;
    public Character(int x, int y, World m, Team t)
    {
        X = x;
        Y = y;
        Monde = m;
        Equipe = t;
        Equipe.Grille.FillGrid(this);
    }

    public void Attack(int x, int y)
    {
        if (this.Equipe.Ennemis.Grille.Check(x, y))
        {
            this.Equipe.Ennemis.Grille.Grille[x, y].Hp -= this.Strength;
            if (this.Equipe.Ennemis.Grille.Grille[x, y].Balle != null)
            {
                this.Equipe.Ennemis.Grille.Grille[x, y].Balle = null;
                this.Monde.Balle.Porteur = this;
                this.Balle = this.Monde.Balle;
                this.Monde.Balle.X = this.X;
                this.Monde.Balle.Y = this.Y;
            }
        }
        if (this.Equipe.Grille.Check(x, y))
        {
            this.Equipe.Grille.Grille[x, y].Hp -= this.Strength;
        }
    }
    public void Passe(Character? ch)
    {
        if (ch != null)
        {
            Balle.X = ch.X;
            Balle.Y = ch.Y;
            ch.Balle = Balle;
            Balle.Porteur = ch;
            Balle = null;
        }
    }

    public virtual void SpecialAbility(int x, int y) { }
    public void Move(int x, int y)
    {
        if (!this.Equipe.Ennemis.Grille.Check(x, y) && !this.Monde.Environnement.Grille.Check(x, y))
        {
            this.Tp(x, y);
            if (Balle != null)
            {
                Balle.X = x;
                Balle.Y = y;
            }
            if (this.Monde.Balle.X == x && this.Monde.Balle.Y == y)
            {
                this.Monde.Balle.Porteur = this;
                this.Balle = this.Monde.Balle;
            }
        }
        else
        {
            Console.WriteLine("Impossible de se déplacer ici...");
        }

    }
    public Character Nearest()
    {
        int increment = (Equipe.YStart - Monde.YSize / 2) / Math.Abs(Equipe.YStart - Monde.YSize / 2);
        Character? chRet = null;
        int yBoucle = this.Y;
        int xBoucle = 0;
        Console.Write(yBoucle);
        while ((yBoucle > 0) && (yBoucle < Monde.YSize) && (chRet == null && chRet != this))
        {
            xBoucle = 0;
            Console.Write(yBoucle);
            while (xBoucle < Monde.XSize && chRet == null)
            {
                Console.Write(xBoucle);
                if (xBoucle != this.X && yBoucle != this.Y)
                {
                    chRet = Equipe.Grille.Grille[xBoucle, yBoucle];
                }
                xBoucle += 1;

            }
            yBoucle += increment;
        }
        Console.Write(chRet.Hp);
        if (chRet == null)
        {
            return this;
        }
        return chRet;
    }
    public void Tp(int x, int y)
    {
        if ((this.Equipe.Monde.XSize > x) && (this.Equipe.Monde.YSize > y) && (0 < y) && (0 < x))
        {

            if (this.Under == null)
            {
                Equipe.Grille.Grille[X, Y] = null;
            }
            else
            {
                Equipe.Grille.Grille[X, Y] = this.Lowest();
                this.Lowest().Active = false;
                this.Under.Above = null;
                this.Under = null;
            }

            if (Equipe.Grille.Check(x, y))
            {
                Equipe.Grille.Grille[x, y].Stack(this);
            }
            else
            {
                Equipe.Grille.Grille[x, y] = this;
            }
            this.Follow(x, y);
        }
    }
    public void Follow(int x, int y)
    {
        this.X = x;
        this.Y = y;
        if (this.Above != null)
        {
            this.Above.Follow(x, y);
        }
    }
    public Character Lowest()
    {
        if (this.Under == null)
        {
            return this;
        }
        return this.Under.Lowest();
    }
    public void Stack(Character ch)
    {
        if (this.Above == null)
        {
            this.Above = ch;
            ch.Under = this;
        }
        else
        {
            this.Above.Stack(ch);
        }
    }
    public Character[] StackTabler()
    {
        Character[] res = new Character[3];
        if (this.Above == null)
        {
            res[0] = this;
            res[1] = this;
            res[2] = this;
        }
        if (this.Above != null)
        {
            if (this.Above.Above != null)
            {
                res[0] = this;
                res[1] = this.Above;
                res[2] = this.Above.Above;
            }
            else
            {
                res[0] = this.Above;
                res[1] = this;
                res[2] = this;
            }
        }
        return res;
    }
}



public class Warrior : Character
{
    public Warrior(int x, int y, World m, Team e) : base(x, y, m, e)
    {
        Id = 0;
        Strength = 5;
        Hp = 5;
    }
}

public class Tank : Character
{
    public Tank(int x, int y, World m, Team e) : base(x, y, m, e)
    {
        Id = 1;
        Strength = 2;
        Hp = 9;
    }
}

public class Sprinter : Character
{

    public Sprinter(int x, int y, World m, Team e) : base(x, y, m, e)
    {
        Id = 2;
        Strength = 2;
        Hp = 1;
        Special = "Sprint";
    }
    public override void SpecialAbility(int x, int y)
    {
        int dirX = x - X;
        int dirY = y - Y;
        Move(x + dirX, y + dirY);
        Move(x + dirX, y + dirY);
    }

}

public class Summoner : Character
{
    public Summoner(int x, int y, World m, Team e) : base(x, y, m, e)
    {
        Id = 3;
        Strength = 5;
        Hp = 4;
        Special = "Summon";
    }
    public override void SpecialAbility(int x, int y)
    {
        if (!Monde.Equipe1.Grille.Check(x, y) && !Monde.Equipe2.Grille.Check(x, y))
        {
            Monde.Environnement.Grille.Grille[x, y] = new Obstacle(x, y, this.Monde, Monde.Environnement);
        }
        else
        {
            Console.WriteLine("Cela échoue !");
        }

    }
}

public class Miner : Character
{

    public Miner(int x, int y, World m, Team e) : base(x, y, m, e)
    {
        Id = 4;
        Strength = 2;
        Hp = 3;
        Special = "Destroy";
    }
    public override void SpecialAbility(int x, int y)
    {
        Monde.Environnement.Grille.Grille[x, y] = null;
    }
}



public class Obstacle : Character
{
    public Obstacle(int x, int y, World m, Team e) : base(x, y, m, e)
    {
        Id = 0;
        Strength = 0;
        Hp = 999;
    }
}


