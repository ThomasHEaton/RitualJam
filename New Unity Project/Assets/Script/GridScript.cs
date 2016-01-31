﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Assets.HelperClasses;
using Assets.Script;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    private GameManagerScript _gameManagerScript;

    public List<TileScript> TileList;
    public List<GameObject> FadeTileList; 
    public HashSet<Tuple> OpenTiles; 

    public GameObject TilePrefab;
    public GameObject FadeTile;
    
	// Use this for initialization
	void Start ()
	{
	    _gameManagerScript = GetComponent<GameManagerScript>();

	    TileList = new List<TileScript>();
        FadeTileList = new List<GameObject>();
            
//        OpenTiles = new HashSet<Tuple>
//        {
//            new Tuple{t1 = -1, t2 = 0}, new Tuple{t1 = -1, t2 = -1}, new Tuple{t1 = 0, t2 = -1}, 
//            new Tuple{t1 = 1, t2 = 0},  new Tuple{t1 = 0, t2 = 1},   new Tuple{t1 = -1, t2 = 1}
//        };

        OpenTiles = new HashSet<Tuple>
        {
            new Tuple{t1 = 0, t2 = 0}
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

        var tileInfo = fadeTile.GetComponent<TileScript>();
        tileInfo.X = tileLocation.t1;
        tileInfo.Y = tileLocation.t2;

        FadeTileList.Add(fadeTile);
        
    }

    public void RemoveFadeTiles()
    {
        foreach (var fadeTile in FadeTileList)
        {
            Destroy(fadeTile);
        }
    }

    public HashSet<Tuple> GetOpenTiles()
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
        tileScript.TileInformation.CanPurchase = false;

        tileScript.X = tileX;
        tileScript.Y = tileY;

        tileScript.Grid = this;

        PlaceTile(tileScript, tileX, tileY);

        UpdateOpenTiles(tileScript);

        _gameManagerScript.OnTileAdded(tileInformation);
    }

    private void UpdateOpenTiles(TileScript tileScript)
    {
        var placedTile = new Tuple{ t1 = tileScript.X, t2 = tileScript.Y };
        OpenTiles.Remove(OpenTiles.First(ts => ts.t1 == tileScript.X && ts.t2 == tileScript.Y));

        if (tileScript.Y % 2 == 0)
        {
            var t = new Tuple {t1 = tileScript.X - 1, t2 = tileScript.Y + 0};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X - 1, t2 = tileScript.Y - 1};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 0, t2 = tileScript.Y - 1};
            if (!TileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 1, t2 = tileScript.Y + 0};
            if (!TileList.Any(ts => ts.X == tileScript.X + 1 && ts.Y == tileScript.Y + 0))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 0, t2 = tileScript.Y + 1};
            if (!TileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y + 1))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X - 1, t2 = tileScript.Y + 1};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 1))
            {
                OpenTiles.Add(t);
            }
        }
        else
        {
            var t = new Tuple {t1 = tileScript.X - 1, t2 = tileScript.Y + 0};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 0, t2 = tileScript.Y - 1};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 1, t2 = tileScript.Y - 1};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 1, t2 = tileScript.Y + 0};
            if (!TileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 1, t2 = tileScript.Y + 1};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                OpenTiles.Add(t);
            }
            t = new Tuple {t1 = tileScript.X + 0, t2 = tileScript.Y + 1};
            if (!TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                OpenTiles.Add(t);
            }
        }

        foreach (var tile in TileList)
        {
            OpenTiles.RemoveWhere(os => os.t1 == tile.X && os.t2 == tile.Y);
        }

    }

    public bool CanAddTile(int tileX, int tileY)
    {
        if (OpenTiles.Contains(new Tuple(){t1 = tileX, t2 = tileY}))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<TileScript> GetNeighbours(TileScript tileScript)
    {
        var tiles = new List<TileScript>();

        if (tileScript.Y % 2 == 0)
        {
            var t = new Tuple { t1 = tileScript.X - 1, t2 = tileScript.Y + 0 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X - 1, t2 = tileScript.Y - 1 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 0, t2 = tileScript.Y - 1 };
            if (TileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 1, t2 = tileScript.Y + 0 };
            if (TileList.Any(ts => ts.X == tileScript.X + 1 && ts.Y == tileScript.Y + 0))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X + 1 && ts.Y == tileScript.Y + 0);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 0, t2 = tileScript.Y + 1 };
            if (TileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y + 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y + 1);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X - 1, t2 = tileScript.Y + 1 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 1);
                tiles.Add(buffTile);
            }
        }
        else
        {
            var t = new Tuple { t1 = tileScript.X - 1, t2 = tileScript.Y + 0 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 0, t2 = tileScript.Y - 1 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 1, t2 = tileScript.Y - 1 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 1, t2 = tileScript.Y + 0 };
            if (TileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 1, t2 = tileScript.Y + 1 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                tiles.Add(buffTile);
            }
            t = new Tuple { t1 = tileScript.X + 0, t2 = tileScript.Y + 1 };
            if (TileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
            {
                var buffTile = TileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                tiles.Add(buffTile);
            }
        }

        return tiles;
    } 
}


