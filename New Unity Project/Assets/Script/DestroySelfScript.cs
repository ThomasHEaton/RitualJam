using UnityEngine;
using System.Collections;

public class DestroySelfScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
