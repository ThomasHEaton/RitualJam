using Assets.HelperClasses;
using Assets.Script;
using UnityEngine;
using System.Collections;

public class PlaceNewTileScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnMouseDown()
    {
        Debug.Log("Stay Alive");

        var gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();

        var tileInfo = GetComponent<TileScript>();

        Debug.Log(tileInfo.X + ", " + tileInfo.Y);

        if (gameManager.TileGrid.CanAddTile(tileInfo.X, tileInfo.Y))
        {
            Debug.Log("Don't Not Stay Alive");
            gameManager.TileGrid.AddTile(tileInfo.X, tileInfo.Y, new BallPitInformation());
        }
    }
}
