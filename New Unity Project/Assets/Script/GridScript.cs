using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HelperClasses;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public List<TileScript> TileList;
    public List<GameObject> FadeTileList; 
    public List<Tuple> OpenTiles; 

    public GameObject TilePrefab;
    public GameObject FadeTile;
    
	// Use this for initialization
	void Start () 
    {
	    TileList = new List<TileScript>();
        FadeTileList = new List<GameObject>();
            
        OpenTiles = new List<Tuple>
        {
            new Tuple{t1 = -1, t2 = 0}, new Tuple{t1 = -1, t2 = -1}, new Tuple{t1 = 0, t2 = -1}, 
            new Tuple{t1 = 1, t2 = 0},  new Tuple{t1 = 0, t2 = 1},   new Tuple{t1 = -1, t2 = 1}
        };
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void NextTurn()
    {
        foreach (var openTile in OpenTiles)
        {
            PlaceFadeTile(openTile);
        }
    }

    public TileScript GetTile(int tileX, int tileY)
    {
        var tile = TileList.FirstOrDefault(t => t.X == tileX && t.Y == tileY);
        return tile;
    }

    public FloatTuple GetTileInUnityPosition(int tileX, int tileY)
    {
        // 100 pixels is 1 Unity Unit/
        float xPos = tileX * 2.6f + Math.Abs(tileY % 2) * 1.3f;
        float yPos = tileY * 2;
        return new FloatTuple{t1 = xPos, t2 = yPos};
    }

    public void PlaceTile(TileScript tile, int tileX, int tileY)
    {
        TileList.Add(tile);
    }

    public void PlaceFadeTile(Tuple tileLocation)
    {
        var fadeTile = (GameObject)Instantiate(FadeTile, new Vector3(GetTileInUnityPosition(tileLocation.t1, tileLocation.t2).t1,
            GetTileInUnityPosition(tileLocation.t1, tileLocation.t2).t2, 0), Quaternion.identity);

        FadeTileList.Add(fadeTile);
        
    }

    public void RemoveFadeTiles()
    {
        foreach (var fadeTile in FadeTileList)
        {
            Destroy(fadeTile);
        }
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

        return TileList.Aggregate(income, (current, tile) => current + tile.GetIncome());
    }

    public void AddTile(int tileX, int tileY, TileInformation tileInformation)
    {
        RemoveFadeTiles();

        var gameTile = (GameObject)Instantiate(TilePrefab, new Vector3(GetTileInUnityPosition(tileX, tileY).t1,
            GetTileInUnityPosition(tileX, tileY).t2, 0), Quaternion.identity);

        var tileScript = gameTile.GetComponent<TileScript>();
        tileScript.TileInformation = tileInformation;

        tileScript.X = tileX;
        tileScript.Y = tileY;

        tileScript.Grid = this;

        PlaceTile(tileScript, tileX, tileY);

        UpdateOpenTiles(tileScript);
    }

    private void UpdateOpenTiles(TileScript tileScript)
    {
        var placedTile = new Tuple{ t1 = tileScript.X, t2 = tileScript.Y };
        if (OpenTiles.Contains(placedTile))
        {
            OpenTiles.Remove(placedTile);
        }

        var t = new Tuple{ t1 = tileScript.X - 1, t2 = tileScript.Y + 0 };
        if (!TileList.Contains(tileScript) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple{ t1 = tileScript.X - 1, t2 = tileScript.Y - 1 };
        if (!TileList.Contains(tileScript) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple{ t1 = tileScript.X + 0, t2 = tileScript.Y - 1 };
        if (!TileList.Contains(tileScript) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple{ t1 = tileScript.X + 1, t2 = tileScript.Y + 0 };
        if (!TileList.Contains(tileScript) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple{ t1 = tileScript.X + 0, t2 = tileScript.Y + 1 };
        if (!TileList.Contains(tileScript) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple{ t1 = tileScript.X - 1, t2 = tileScript.Y + 1 };
        if (!TileList.Contains(tileScript) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
    }

    public bool CanAddTile(int tileX, int tileY)
    {
        if (OpenTiles.Contains(new Tuple{ t1 = tileX, t2 = tileY }))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


