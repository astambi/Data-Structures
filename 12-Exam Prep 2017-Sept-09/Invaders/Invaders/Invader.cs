public class Invader : IInvader
{
    public Invader(int damage, int distance)
    {
        this.Damage = damage;
        this.Distance = distance;
    }

    public int Damage { get; set; }

    public int Distance { get; set; }

    public int CompareTo(IInvader other)
    {
        var compare = this.Distance.CompareTo(other.Distance); // priority min distance
        if (compare == 0)
        {
            compare = other.Damage.CompareTo(this.Damage); // priority max damage
        }

        return compare;
    }
}
