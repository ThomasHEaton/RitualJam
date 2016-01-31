using Assets.HelperClasses;
using Assets.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PurchasableTileScript : MonoBehaviour
{
    private GameManagerScript _gameManager;


    public TileInformation TileInformation;
    public Text SelectionArrow;

	// Use this for initialization
	void Start ()
	{
	    _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_gameManager.SelectedTile == TileInformation)
	    {
	        SelectionArrow.gameObject.SetActive(true);
	    }
	    else
	    {
	        SelectionArrow.gameObject.SetActive(false);
	    }

	    if (TileInformation != null)
	    {
	        GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprites/" + TileInformation.SpriteName);
	    }

        
	}

    public void OnSelected()
    {
        Debug.Log("Purchasable Tile Clicked!");

        var gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameManager.GetComponent<GameManagerScript>().SelectedTile = TileInformation;
    }
}
