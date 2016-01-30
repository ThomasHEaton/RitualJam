using Assets.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ResourceUIControllerScript : MonoBehaviour
{

    public GameManagerScript GameManager;

    public Text SoulsText;
    public Text PeopleText;
    public Text MoneyText;
    public Text InfluenceText;
    public Text NotorietyText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    SoulsText.text = "Souls: " + GameManager.Souls + " (" + SignedNumberString(GameManager.PreviousIncome.Soul) + ")";

        PeopleText.text = "People: " + GameManager.People + " (" + SignedNumberString(GameManager.PreviousIncome.People) + ")";
        MoneyText.text = "Money: " + GameManager.Money + " (" + SignedNumberString(GameManager.PreviousIncome.Money) + ")";
        InfluenceText.text = "Influence: " + GameManager.Inf + " (" + SignedNumberString(GameManager.PreviousIncome.Inf) + ")";
        NotorietyText.text = "Noteriety: " + GameManager.Not + " (" + SignedNumberString(GameManager.PreviousIncome.Not) + ")";
	}

    public string SignedNumberString(int number)
    {
        string s = "";
        if (number > 0)
        {
            s += "+";
        }

        if (number < 0)
        {
            s += "-";
        }

        s += number;

        return s;
    }
}
