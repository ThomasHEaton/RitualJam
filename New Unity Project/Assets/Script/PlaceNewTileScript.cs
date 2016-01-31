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
        var gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();

        var tileInfo = GetComponent<TileScript>();

        if (gameManager.TileGrid.CanAddTile(tileInfo.X, tileInfo.Y) && gameManager.SelectedTile != null && gameManager.SelectedTile.CanPurchase && gameManager.SelectedTile.CanAfford(gameManager))
        {

            gameManager.Souls += gameManager.SelectedTile.SoulsCost;
            gameManager.People += gameManager.SelectedTile.PeopleCost;
            gameManager.Money += gameManager.SelectedTile.MoneyCost;
            gameManager.Inf += gameManager.SelectedTile.InfCost;
            gameManager.Not += gameManager.SelectedTile.NotCost;


            gameManager.TileGrid.AddTile(tileInfo.X, tileInfo.Y, gameManager.SelectedTile);
        }
    }
}
