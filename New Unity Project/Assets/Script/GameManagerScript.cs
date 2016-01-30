using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.HelperClasses;
using UnityEngine;

namespace Assets.Script
{
    public class GameManagerScript : MonoBehaviour
    {
        public GridScript TileGrid;

        public int CurrentTurnNumber;

        public int Souls;

        public int People;
        public int Money;
        public int Not;
        public int Inf;

        public Income PreviousIncome = new Income();

        public void EndTurn()
        {
            CurrentTurnNumber++;

            var income = TileGrid.GetIncome();

            People += income.People;
            Money += income.Money;
            Not += income.Not;
            Inf += income.Inf;

            PreviousIncome = income;
        }
    }
}
