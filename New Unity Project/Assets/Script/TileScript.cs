using Assets.HelperClasses;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    protected bool Equals(TileScript other)
    {
        return base.Equals(other) && Equals(Grid, other.Grid) && X == other.X && Y == other.Y && Equals(TileInformation, other.TileInformation);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = base.GetHashCode();
            hashCode = (hashCode*397) ^ (Grid != null ? Grid.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ X;
            hashCode = (hashCode*397) ^ Y;
            hashCode = (hashCode*397) ^ (TileInformation != null ? TileInformation.GetHashCode() : 0);
            return hashCode;
        }
    }

    public GridScript Grid;

    public int X;
    public int Y;

    public TileInformation TileInformation;

	// Use this for initialization
	void Start ()
	{
	    if (TileInformation != null && TileInformation.SpriteName != null && TileInformation.SpriteName != "")
	    {
            var sprite = Resources.Load<Sprite>("Sprites/" + TileInformation.SpriteName);
            GetComponent<SpriteRenderer>().sprite = sprite;
	    }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Income GetIncome()
    {
        return new Income() { Soul = TileInformation.DeltaSouls, People = TileInformation.DeltaInf, Inf = TileInformation.DeltaInf, Money = TileInformation.DeltaMoney, Not = TileInformation.DeltaNot };
    }

    void PlaceTile()
    {
        
    }

    public override bool Equals(object o)
    {
        if (ReferenceEquals(null, o)) return false;
        if (ReferenceEquals(this, o)) return true;
        if (o.GetType() != this.GetType()) return false;
        return Equals((TileScript) o);
    }
}
