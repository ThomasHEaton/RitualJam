using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.HelperClasses
{
    [Serializable]
    public class TileInformation
    {
        public string TileName;
        public string TileDescription;

        public int DeltaSouls;

        public int DeltaPeople;
        public int DeltaMoney;
        public int DeltaNot;
        public int DeltaInf;
    }

    public class StartingLocation : TileInformation
    {
        public StartingLocation()
        {
            TileName = "Starting Location";
            TileDescription = "A tiny shack in the woods";

            DeltaPeople = 1;
            DeltaMoney = 1;
            DeltaNot = 1;
            DeltaInf = 1;
        }
    }
}
