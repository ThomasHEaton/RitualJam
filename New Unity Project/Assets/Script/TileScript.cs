using System;
using Assets.HelperClasses;
using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour
{
    public GridScript Grid;

    public int X;
    public int Y;

    public TileInformation TileInformation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Income GetIncome()
    {
        return new Income() { Soul = TileInformation.DeltaSouls, People = TileInformation.DeltaPeople, Inf = TileInformation.DeltaInf, Money = TileInformation.DeltaMoney, Not = TileInformation.DeltaNot };
    }

    void PlaceTile()
    {
        
    }
}
