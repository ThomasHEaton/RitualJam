using System.Net.Mime;
using Assets.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectedTileUIController : MonoBehaviour
{

    public GameManagerScript GameManager;
    public Text TileText;
    public Text TileDescriptionText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if (GameManager != null && GameManager.SelectedTile != null)
	    {
	        TileText.text = GameManager.SelectedTile.TileName;
            TileDescriptionText.text = GameManager.SelectedTile.TileDescription;
	    }


	}
}
