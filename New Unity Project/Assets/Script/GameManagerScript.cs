using Assets.HelperClasses;
using UnityEngine;

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

            TileGrid.NextTurn();
        }
    }
}
