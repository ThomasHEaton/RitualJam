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
//	    Debug.Log(GameManager);
//
//        Debug.Log(GameManager.Souls);
//	    Debug.Log(GameManager.SoulsRequired);
//	    Debug.Log(GameManager.PaymentTurn);
//	    Debug.Log(GameManager.CurrentTurnNumber + 1);
        
	    TurnText.text = GameManager.Souls + " of " + GameManager.SoulsRequired + " in " +
	                    (GameManager.PaymentTurn - GameManager.CurrentTurnNumber + 1) + " turns";
	}
}
