using System;
using Assets.Script;

namespace Assets.HelperClasses
{
    [Serializable]
    public class TileInformation
    {
        public string TileName;
        public string TileDescription;

        public string SpriteName;

        public bool CanPurchase;

        public int SoulsCost;
        public int PeopleCost;
        public int MoneyCost;
        public int InfCost;
        public int NotCost;

        public int DeltaSouls;
        public int DeltaPeople;
        public int DeltaMoney;
        public int DeltaNot;
        public int DeltaInf;

        public TileAction OnPlacement;

        public TileAction TileAction1;
        public TileAction TileAction2;
        public TileAction TileAction3;

        public string GetInformationText()
        {
            var costString = "";
            costString += SoulsCost != 0 ? "Souls: " + SoulsCost + "\n": "";
            costString += PeopleCost != 0 ? "Followers: " + PeopleCost + "\n" : "";
            costString += MoneyCost != 0 ? "Money: " + MoneyCost + "\n" : "";
            costString += InfCost != 0 ? "Influence: " + InfCost + "\n" : "";
            costString += NotCost != 0 ? "Notority: " + NotCost + "\n" : "";

            if (costString != "")
            {
                costString = "Costs:\n\n" + costString + "\n\n";
            }

            var perTurn = "";
            perTurn += DeltaSouls != 0 ? "Souls: " + DeltaSouls + "\n" : "";
            perTurn += DeltaPeople != 0 ? "Followers: " + DeltaPeople + "\n" : "";
            perTurn += DeltaMoney != 0 ? "Money: " + DeltaMoney + "\n" : "";
            perTurn += DeltaInf != 0 ? "Influence: " + DeltaInf + "\n" : "";
            perTurn += DeltaNot != 0 ? "Notority: " + DeltaNot + "\n" : "";

            if (perTurn != "")
            {
                perTurn = "Per Turn:\n\n" + perTurn;
            }

            return costString + perTurn;
        }
    }

    public abstract class TileAction
    {
        public abstract void Action(GameManagerScript gameManager, GridScript grid, TileScript tile);
    }

    [Serializable]
    public class StartingLocation : TileInformation
    {
        public StartingLocation()
        {
            TileName = "Starting Location";
            TileDescription = "A tiny shack in the woods";

            SpriteName = "StartingShack";

            DeltaPeople = 3;
            DeltaMoney = 1;
            DeltaInf = 1;
        }
    }

    [Serializable]
    public class CityInformation : TileInformation
    {
        public CityInformation()
        {
            TileName = "City";
            TileDescription = "Have 10 more influence than notoriety generation around this tile to gain 10 followers.";

            SpriteName = "CityTile";
        }   
    }

    [Serializable]
    public class KoolaidStandInformation : TileInformation
    {
        public KoolaidStandInformation()
        {
            TileName = "Koolaid Stand";
            TileDescription = "Little Billy has a great business idea and just rolled with it.";

            SpriteName = "LemonTileAlt";

            PeopleCost = -1;
            DeltaMoney = 2;
        }
    }

    [Serializable]
    public class KittyHospitalInformation : TileInformation
    {
        public KittyHospitalInformation()
        {
            TileName = "Kitty Hospital";
            TileDescription = "The famous debate of Save the Animals or Kill the Animals.";

            SpriteName = "KittyHospitalTile";

            MoneyCost = -4;
            DeltaMoney = -1;
            DeltaInf = 3;
        }
    }

    [Serializable]
    public class BallPitInformation : TileInformation
    {
        public BallPitInformation()
        {
            TileName = "Ball Pit";
            TileDescription = "Seriously?";

            SpriteName = "BallpitTile";

            MoneyCost = -2;
            DeltaInf = 1;
        }
    }

    [Serializable]
    public class SacrificingTableInformation : TileInformation
    {
        public SacrificingTableInformation()
        {
            TileName = "Sacrifice Table";
            TileDescription = "Kill people to save the human race!";

            SpriteName = "SacrificeTable";

            TileAction1 = new SacrficeAction1();
            TileAction2 = new SacrficeAction2();
            TileAction3 = new SacrficeAction3();
        }
    }

    public class SacrficeAction1 : TileAction
    {
        public override void Action(GameManagerScript gameManager, GridScript grid, TileScript tile)
        {
            gameManager.People -= 1;
            gameManager.Not += 1;
        }
    }
    public class SacrficeAction2 : TileAction
    {
        public override void Action(GameManagerScript gameManager, GridScript grid, TileScript tile)
        {
            gameManager.People -= 2;
            gameManager.Not += 1;
        }
    }
    public class SacrficeAction3 : TileAction
    {
        public override void Action(GameManagerScript gameManager, GridScript grid, TileScript tile)
        {
            gameManager.People -= 3;
            gameManager.Not += 1;
        }
    }

    [Serializable]
    public class BankInformation : TileInformation
    {
        public BankInformation()
        {
            TileName = "Bank";
            TileDescription = "Too big to fail?";

            SpriteName = "BankTile";

            MoneyCost = -5;
            PeopleCost = -2;
            InfCost = -1;

            DeltaMoney = 4;
        }
    }

    [Serializable]
    public class PRFirmInformation : TileInformation
    {
        public PRFirmInformation()
        {
            TileName = "PR Firm";
            TileDescription = "You're never going to be able to pay these guys enough";

            SpriteName = "PRFirmTile";

            MoneyCost = -4;
            PeopleCost = -1;
            
            DeltaMoney = -2;
            DeltaNot = -1;
        }
    }

    [Serializable]
    public class TempleInformation : TileInformation
    {
        public TempleInformation()
        {
            TileName = "Temple";
            TileDescription = "";

            SpriteName = "Temple";

            MoneyCost = -8;
            PeopleCost = -1;
            NotCost = 1;

            // TODO - Doubles the influence and money gained from the tiles around it.
            TileAction1 = new SacrficeAction1();
        }
    }

    // Forced Buildings
    [Serializable]
    public class PoliceStationInformation : TileInformation
    {
        public PoliceStationInformation()
        {
            TileName = "Police Station";
            TileDescription = "You've acquired too much notority and they've set up a station in your district.  Get it down or there will be more.";

            SpriteName = "PoliceTile";

            DeltaMoney = -3;
        }
    }

    [Serializable]
    public class ChurchInformation : TileInformation
    {
        public ChurchInformation()
        {
            TileName = "Church";
            TileDescription = "You've acquired too much notority and they've set up a church in your district.  Get it down or there will be more.";

            SpriteName = "Church";

            DeltaInf = -3;
        }
    }
}
