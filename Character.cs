public abstract class Character
{
    public World Monde;
    public Team Equipe;
    public int X;
    public int Y;
    public int Strength;
    public int Speed;
    public int Hp;
    public int Id;
    public int NumOrder;
    public Character? Above;
    public Character? Under;
    public Character(int x, int y, World m, Team t, int num)
    {
        X = x;
        Y = y;
        Monde = m;
        Equipe = t;
        Equipe.Grille.FillGrid(this);
        NumOrder = num;
    }
    public void Move(int x, int y)
    {
        this.Tp(X + x, Y + y);
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
                Equipe.Grille.Grille[X, Y] = this.Lowest(X, Y);
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
    public Character Lowest(int x, int y)
    {
        if (this.Under == null)
        {
            return this;
        }
        return this.Under.Lowest(x, y);
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
    public Warrior(int x, int y, World m, Team e, int num) : base(x, y, m, e, num)
    {
        Id = 0;
        Strength = 10;
        Hp = 8;
        Speed = 5;
    }
}

public class Tank : Character
{
    public Tank(int x, int y, World m, Team e, int num) : base(x, y, m, e, num)
    {
        Id = 1;
        Strength = 2;
        Hp = 20;
        Speed = 2;
    }
}

public class Obstacle : Character
{
    public Obstacle(int x, int y, World m, Team e) : base(x, y, m, e, 99)
    {
        Id = 0;
        Strength = 0;
        Hp = 999;
        Speed = 0;
    }
}

