using System.Collections.Generic;
using Assets.HelperClasses;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public GameObject[,] Tiles;
    public List<Tuple> TileList;
    public List<Tuple> OpenTiles; 

    public GameObject TilePrefab;
    
	// Use this for initialization
	void Start () {
	    
        TileList = new List<Tuple>()
        {
            new Tuple(){t1 = 0, t2 = 0}
        };

        Tiles = new GameObject[1000,1000];

            
        OpenTiles = new List<Tuple>()
        {
            new Tuple(){t1 = -1, t2 = 0}, new Tuple(){t1 = -1, t2 = -1}, new Tuple(){t1 = 0, t2 = -1}, 
            new Tuple(){t1 = 1, t2 = 0}, new Tuple(){t1 = 0, t2 = 1}, new Tuple(){t1 = -1, t2 = 1}
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject GetTile(int tileX, int tileY)
    {
        return Tiles[500 + tileX, 500 + tileY];
    }

    public Tuple GetTileInUnityPosition(int tileX, int tileY)
    {
        // 100 pixels is 1 Unity Unit/
        return new Tuple(){t1 = 0, t2 = 0};
    }

    public void PlaceTile(GameObject tile, int tileX, int tileY)
    {
        Tiles[500 + tileX, 500 + tileY] = tile;
        TileList.Add(new Tuple(){t1 = tileX, t2 = tileY});
    }

    public List<Tuple> GetOpenTiles()
    {
        // Return all of the open buildable spaces on the board.
        return OpenTiles;
    }

    public void AddTile(int tileX, int tileY)
    {
        var gameTile = (GameObject) Instantiate(TilePrefab, new Vector3(GetTileInUnityPosition(tileX, tileY).t1,
            GetTileInUnityPosition(tileX, tileY).t2, 0), Quaternion.identity);

        var tileScript = gameTile.GetComponent<TileScript>();

        tileScript.Grid = this;

        PlaceTile(gameTile, tileX, tileY);

        UpdateOpenTiles(tileX, tileY);
    }

    private void UpdateOpenTiles(int tileX, int tileY)
    {
        var placedTile = new Tuple() {t1 = tileX, t2 = tileY};
        if (OpenTiles.Contains(placedTile))
        {
            OpenTiles.Remove(placedTile);
        }

        var t = new Tuple() {t1 = tileX - 1, t2 = tileY + 0};
        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple() {t1 = tileX - 1, t2 = tileY - 1};
        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple() {t1 = tileX + 0, t2 = tileY - 1};
        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple() {t1 = tileX + 1, t2 = tileY + 0};
        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple() {t1 = tileX + 0, t2 = tileY + 1};
        if (!TileList.Contains(t) && !OpenTiles.Contains(t))
        {
            OpenTiles.Add(t);
        }
        t = new Tuple() {t1 = tileX - 1, t2 = tileY + 1};
        if (!TileList.Contains(t))
        {
            OpenTiles.Add(t);
        }
    }

    public bool CanAddTile(int tileX, int tileY)
    {
        if (OpenTiles.Contains(new Tuple() {t1 = tileX, t2 = tileY}))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}