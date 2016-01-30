using System.Collections.Generic;
using Assets.HelperClasses;
using UnityEngine;
using System.Collections;
using UnityEngine.WSA;

public class GridScript : MonoBehaviour
{
    public TileScript[,] Tiles;
    public List<TileScript> TileList; 


	// Use this for initialization
	void Start () {
	    
        TileList = new List<TileScript>();
        Tiles = new TileScript[1000,1000];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public TileScript GetTile(int tileX, int tileY)
    {
        return Tiles[500 + tileX, 500 + tileY];
    }

    public int GetTileInUnityPosition(int tileX, int tileY)
    {
        // 100 pixels is 1 Unity Unit/
        return 0;
    }

    public Tuple GetOpenTiles()
    {
        // Return all of the open buildable spaces on the board.
        return null;
    }
}
