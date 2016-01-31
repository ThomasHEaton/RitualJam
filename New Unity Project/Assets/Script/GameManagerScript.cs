using System.Collections.Generic;
using Assets.HelperClasses;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Script
{
    public class GameManagerScript : MonoBehaviour
    {
        public GridScript TileGrid;

        public int CurrentTurnNumber = 1;

        public int Souls = 0;

        public int People = 0;
        public int Money = 0;
        public int Not = 0;
        public int Inf = 0;

        public int SoulsRequired = 10;
        public int PaymentTurn = 10;

        public Income PreviousIncome = new Income();

        public TileInformation SelectedTile;

        public GameObject EndOfGameObject;

        public PurchasableTileScript PurchasableTile1;
        public PurchasableTileScript PurchasableTile2;
        public PurchasableTileScript PurchasableTile3;

        public List<TileInformation> PurchasableTiles;

        public void Start()
        {

        }

        public void StartGame()
        {
            TileGrid.AddTile(0, 0, new StartingLocation());

            var tiles = GetPurchasableTileInformation();

            PurchasableTile1.TileInformation = tiles[0];
            PurchasableTile2.TileInformation = tiles[1];
            PurchasableTile3.TileInformation = tiles[2];

            PurchasableTile1.TileInformation.CanPurchase = true;
            PurchasableTile2.TileInformation.CanPurchase = true;
            PurchasableTile3.TileInformation.CanPurchase = true;

            PurchasableTile1.gameObject.SetActive(true);
            PurchasableTile2.gameObject.SetActive(true);
            PurchasableTile3.gameObject.SetActive(true);

            TileGrid.NextTurn();
        }

        public void EndTurn()
        {
            var income = TileGrid.GetIncome();

            People += income.People;
            Money += income.Money;
            Not += income.Not;
            Inf += income.Inf;

            PreviousIncome = income;

            if (CurrentTurnNumber == PaymentTurn)
            {
                Debug.Log(Souls + " of " + SoulsRequired + " Souls");
                if (Souls >= SoulsRequired)
                {
                    Souls -= SoulsRequired;
                    SoulsRequired *= 2;
                    PaymentTurn += 10;
                }
                else
                {
                    EndOfGameObject.SetActive(true);
                }
            }

            CurrentTurnNumber++;
            Debug.Log("Turn " + CurrentTurnNumber + " of " + PaymentTurn);

            var tiles = GetPurchasableTileInformation();

            // Call all of the OnStartTurn on all of the tiles.
            foreach (var tile in TileGrid.TileList)
            {
                if (tile.TileInformation.OnTurnStart != null)
                {
                    tile.TileInformation.OnTurnStart.Action(this, TileGrid, tile.TileInformation);
                }
            }

            PurchasableTile1.TileInformation = tiles[0];
            PurchasableTile2.TileInformation = tiles[1];
            PurchasableTile3.TileInformation = tiles[2];

            PurchasableTile1.TileInformation.CanPurchase = true;
            PurchasableTile2.TileInformation.CanPurchase = true;
            PurchasableTile3.TileInformation.CanPurchase = true;

            PurchasableTile1.gameObject.SetActive(true);
            PurchasableTile2.gameObject.SetActive(true);
            PurchasableTile3.gameObject.SetActive(true);

            TileGrid.NextTurn();
        }

        public TileInformation[] GetPurchasableTileInformation()
        {
            var tileInfo = new TileInformation[3];
            var listArray = new List<int> { 0, 1, 2, 3, 4, 5 };

            var value1 = Random.Range(0, listArray.Count);
            tileInfo[0] = GetTileFromId(listArray[value1]);
            listArray.Remove(value1);

            var value2 = Random.Range(0, listArray.Count);
            tileInfo[1] = GetTileFromId(listArray[value2]);
            listArray.Remove(value2);

            var value3 = Random.Range(0, listArray.Count);
            tileInfo[2] = GetTileFromId(listArray[value3]);

            return tileInfo;
        }

        public TileInformation GetTileFromId(int value1)
        {
            if (value1 == 0)
            {
                return new CityInformation();
            }
            else if (value1 == 1)
            {
                return new BallPitInformation();
            }
            else if (value1 == 2)
            {
                return new KoolaidStandInformation();
            }
            else if (value1 == 3)
            {
                return new TempleInformation();
            }
            else if (value1 == 4)
            {
                return new SacrificingTableInformation();
            }
            else if (value1 == 5)
            {
                return new KittyHospitalInformation();
            }

            return null;
        }

        public void OnTileAdded()
        {
            PurchasableTile1.gameObject.SetActive(false);
            PurchasableTile2.gameObject.SetActive(false);
            PurchasableTile3.gameObject.SetActive(false);
        }
    }
}
