using Assets.Script;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileScript))]
public class ClickToDisplayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        var gameManager = GameObject.FindGameObjectWithTag("GameController");

        Debug.Log(gameManager);
        Debug.Log(GetComponent<TileScript>());
        gameManager.GetComponent<GameManagerScript>().SelectedTile = GetComponent<TileScript>();
    }
}
