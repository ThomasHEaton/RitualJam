using System.Collections.Generic;
using Assets.HelperClasses;
using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using UnityEngine.WSA;

public class GridScript : MonoBehaviour
{
    public TileScript[,] Tiles;
    public List<TileScript> TileList;


    public Object TilePrefab;

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

    public Tuple GetTileInUnityPosition(int tileX, int tileY)
    {
        // 100 pixels is 1 Unity Unit/
        return new Tuple() {t1 = 0, t2 = 0};
    }

    public Tuple GetOpenTiles()
    {
        // Return all of the open buildable spaces on the board.
        return null;
    }

    public void AddTile(int tileX, int tileY, TileInformation tile)
    {
        var tileObject = (GameObject)Instantiate(TilePrefab, Vector3.zero, Quaternion.identity);
        var tileInformation = tileObject.GetComponent<TileScript>();
        tileInformation.TileInformation = tile;

        TileList.Add(tileObject.GetComponent<TileScript>());
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

}
