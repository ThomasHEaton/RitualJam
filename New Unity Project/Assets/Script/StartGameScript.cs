using Assets.HelperClasses;
using UnityEngine;

[RequireComponent(typeof(GridScript))]
public class StartGameScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        var gridScript = this.gameObject.GetComponent<GridScript>();
        gridScript.AddTile(0, 0, new StartingLocation());
    }
}
