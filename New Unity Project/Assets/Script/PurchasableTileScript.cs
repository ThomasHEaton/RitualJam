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

	    if (TileInformation != null && !TileInformation.CanAfford(_gameManager))
	    {
	        var color = this.GetComponent<Button>().image.color;
	        color.r = 0.5f;
	        color.b = 0.5f;
	        color.g = 0.5f;

	        this.GetComponent<Button>().image.color = color;
	    }
	    else
	    {
            var color = this.GetComponent<Button>().image.color;
            color.r = 1f;
            color.b = 1f;
            color.g = 1f;

            this.GetComponent<Button>().image.color = color;
	    }

        
	}

    public void OnSelected()
    {
        var gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameManager.GetComponent<GameManagerScript>().SelectedTile = TileInformation;
    }
}
