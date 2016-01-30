using System.Collections.Generic;
using System.Linq;
using Assets.HelperClasses;
using UnityEngine;

public class GridScript : MonoBehaviour
{
//    public GameObject[,] Tiles;
    public List<TileScript> TileList;
    public List<Tuple> OpenTiles; 

    public GameObject TilePrefab;
    
	// Use this for initialization
	void Start () {
	    
//        TileList = new List<Tuple>()
//        {
//            new Tuple(){t1 = 0, t2 = 0}
//        };

        TileList = new List<TileScript>();
            
        OpenTiles = new List<Tuple>()
        {
            new Tuple(){t1 = -1, t2 = 0}, new Tuple(){t1 = -1, t2 = -1}, new Tuple(){t1 = 0, t2 = -1}, 
            new Tuple(){t1 = 1, t2 = 0}, new Tuple(){t1 = 0, t2 = 1}, new Tuple(){t1 = -1, t2 = 1}
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public TileScript GetTile(int tileX, int tileY)
    {
        var tile = TileList.FirstOrDefault(t => t.X == tileX && t.Y == tileY);
        return tile;
    }

    public Tuple GetTileInUnityPosition(int tileX, int tileY)
    {
        // 100 pixels is 1 Unity Unit/
        return new Tuple(){t1 = 0, t2 = 0};
    }

    public void PlaceTile(TileScript tile, int tileX, int tileY)
    {
        TileList.Add(tile);
    }

    public List<Tuple> GetOpenTiles()
    {
        // Return all of the open buildable spaces on the board.
        return OpenTiles;
    }

    // Income Numbers
    public Income GetIncome()
    {
        var income = new Income();
        foreach (var tile in TileList)
        {
            income = income + tile.GetIncome();
        }

        return income;
    }

    public void AddTile(int tileX, int tileY, TileInformation tileInformation)
    {
        var gameTile = (GameObject)Instantiate(TilePrefab, new Vector3(GetTileInUnityPosition(tileX, tileY).t1,
            GetTileInUnityPosition(tileX, tileY).t2, 0), Quaternion.identity);

        var tileScript = gameTile.GetComponent<TileScript>();
        tileScript.TileInformation = tileInformation;

        tileScript.X = tileX;
        tileScript.Y = tileY;

        tileScript.Grid = this;

        PlaceTile(tileScript, tileX, tileY);

        UpdateOpenTiles(tileX, tileY);
    }

    private void UpdateOpenTiles(int tileX, int tileY)
    {
//        var placedTile = new Tuple() { t1 = tileX, t2 = tileY };
//        if (OpenTiles.Contains(placedTile))
//        {
//            OpenTiles.Remove(placedTile);
//        }
//
//        var t = new Tuple() { t1 = tileX - 1, t2 = tileY + 0 };
//        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
//        {
//            OpenTiles.Add(t);
//        }
//        t = new Tuple() { t1 = tileX - 1, t2 = tileY - 1 };
//        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
//        {
//            OpenTiles.Add(t);
//        }
//        t = new Tuple() { t1 = tileX + 0, t2 = tileY - 1 };
//        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
//        {
//            OpenTiles.Add(t);
//        }
//        t = new Tuple() { t1 = tileX + 1, t2 = tileY + 0 };
//        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
//        {
//            OpenTiles.Add(t);
//        }
//        t = new Tuple() { t1 = tileX + 0, t2 = tileY + 1 };
//        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
//        {
//            OpenTiles.Add(t);
//        }
//        t = new Tuple() { t1 = tileX - 1, t2 = tileY + 1 };
//        if (!TileList.Contains(t))
//        {
//            OpenTiles.Add(t);
//        }
    }

    public bool CanAddTile(int tileX, int tileY)
    {
        if (OpenTiles.Contains(new Tuple() { t1 = tileX, t2 = tileY }))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


