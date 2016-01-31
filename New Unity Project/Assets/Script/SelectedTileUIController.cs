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
    public Text InformationText;

    public Button ActionButton1;
    public Button ActionButton2;
    public Button ActionButton3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if (GameManager != null && GameManager.SelectedTile != null)
	    {
	        TileText.text = GameManager.SelectedTile.TileName;
            TileDescriptionText.text = GameManager.SelectedTile.TileDescription;
            InformationText.text = GameManager.SelectedTile.GetInformationText();

	        if (GameManager.SelectedTile.TileAction1 != null && GameManager.SelectedTile.AreActionsEnabled)
	        {
	            ActionButton1.GetComponentInChildren<Text>().text = GameManager.SelectedTile.TileAction1.ActionName();
	        }
            ActionButton1.gameObject.SetActive(GameManager.SelectedTile.TileAction1 != null && GameManager.SelectedTile.AreActionsEnabled);

            if (GameManager.SelectedTile.TileAction2 != null && GameManager.SelectedTile.AreActionsEnabled)
            {
                ActionButton2.GetComponentInChildren<Text>().text = GameManager.SelectedTile.TileAction2.ActionName();
            }
            ActionButton2.gameObject.SetActive(GameManager.SelectedTile.TileAction2 != null && GameManager.SelectedTile.AreActionsEnabled);

            if (GameManager.SelectedTile.TileAction3 != null && GameManager.SelectedTile.AreActionsEnabled)
            {
                ActionButton3.GetComponentInChildren<Text>().text = GameManager.SelectedTile.TileAction3.ActionName();
            }
            ActionButton3.gameObject.SetActive(GameManager.SelectedTile.TileAction3 != null && GameManager.SelectedTile.AreActionsEnabled);
	    }
	}

    public void ActionButtonClicked(int number)
    {
        if (number == 3 && GameManager.SelectedTile.TileAction3 != null && GameManager.SelectedTile.AreActionsEnabled)
        {
            Debug.Log("Calling Action 3");
            GameManager.SelectedTile.TileAction3.Action(GameManager, GameManager.TileGrid, GameManager.SelectedTile);
        }
    }
}
