using System;
using Assets.Script;

namespace Assets.HelperClasses
{
    [Serializable]
    public abstract class TileInformation
    {
        public string TileName;
        public string TileDescription;

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
    }

    public abstract class TileAction
    {
        public abstract void Action(GameManagerScript gameManager, GridScript grid, TileScript tile);
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

    public class CityInformation : TileInformation
    {
        public CityInformation()
        {
            TileName = "City";
            TileDescription = "10 more influence than notoriety to gain 10 followers.";


        }   
    }

    public class KoolaidStandInformation : TileInformation
    {
        public KoolaidStandInformation()
        {
            TileName = "Koolaid Stand";
            TileDescription = "Little Billy has a great business idea and just rolled with it.";

            PeopleCost = -1;
            DeltaMoney = 2;
        }
    }

    public class KittyHospitalInformation : TileInformation
    {
        public KittyHospitalInformation()
        {
            TileName = "Kitty Hospital";
            TileDescription = "The famous debate of Save the Animals or Kill the Animals.";

            MoneyCost = -4;
            DeltaMoney = -1;
            DeltaInf = 3;
        }
    }

    public class BallPitInformation : TileInformation
    {
        public BallPitInformation()
        {
            TileName = "Ball Pit";
            TileDescription = "Seriously?";

            MoneyCost = -2;
            DeltaInf = 1;
        }
    }

    public class SacrificingTableInformation : TileInformation
    {
        public SacrificingTableInformation()
        {
            TileName = "Sacrifice Table";
            TileDescription = "Kill people to save the human race!";

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

    public class BankInformation : TileInformation
    {
        public BankInformation()
        {
            TileName = "Bank";
            TileDescription = "Too big to fail?";

            MoneyCost = -5;
            PeopleCost = -2;
            InfCost = -1;

            DeltaMoney = 4;
        }
    }

    public class PRFirmInformation : TileInformation
    {
        public PRFirmInformation()
        {
            TileName = "PR Firm";
            TileDescription = "You're never going to be able to pay these guys enough";

            MoneyCost = -4;
            PeopleCost = -1;
            
            DeltaMoney = -2;
            DeltaNot = -1;
        }
    }

    public class TempleInformation : TileInformation
    {
        public TempleInformation()
        {
            TileName = "Temple";
            TileDescription = "";

            MoneyCost = -8;
            PeopleCost = -1;
            NotCost = 1;

            // TODO - Doubles the influence and money gained from the tiles around it.
            TileAction1 = new SacrficeAction1();
        }
    }

    // Forced Buildings
    public class PoliceStationInformation : TileInformation
    {
        public PoliceStationInformation()
        {
            TileName = "Police Station";
            TileDescription = "You've acquired too much notority and they've set up a station in your district.  Get it down or there will be more.";

            DeltaMoney = -3;
        }
    }

    public class ChurchInformation : TileInformation
    {
        public ChurchInformation()
        {
            TileName = "Church";
            TileDescription = "You've acquired too much notority and they've set up a church in your district.  Get it down or there will be more.";

            DeltaInf = -3;
        }
    }
}
