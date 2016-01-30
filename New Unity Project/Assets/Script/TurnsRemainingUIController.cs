using System.Net.Mime;
using Assets.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnsRemainingUIController : MonoBehaviour
{

    public GameManagerScript GameManager;
    public Text TurnText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    TurnText.text = GameManager.Souls + " of " + GameManager.SoulsRequired + " in " +
	                    (GameManager.PaymentTurn - GameManager.CurrentTurnNumber) + " turns";
	}
}
