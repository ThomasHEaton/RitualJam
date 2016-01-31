using Assets.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        gameManager.GetComponent<GameManagerScript>().SelectedTile = GetComponent<TileScript>().TileInformation;

        var position = Camera.main.transform.position;
        position.x = this.transform.position.x;
        position.y = this.transform.position.y;
        Camera.main.transform.position = position;

    }
}
