using Assets.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanEndTurnScript : MonoBehaviour
{

    public GameManagerScript GameManagerScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	    GetComponent<Button>().interactable = GameManagerScript.CanEndTurn;
	}
}
