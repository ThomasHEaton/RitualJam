﻿using System;
using System.Linq;
using Assets.Script;
using Debug = UnityEngine.Debug;

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
        public TileAction OnTurnStart;

        public bool AreActionsEnabled = true;
        public TileAction TileAction1;
        public TileAction TileAction2;
        public TileAction TileAction3;

        public string GetInformationText()
        {
            var costString = "";

            if (CanPurchase)
            {
                costString += SoulsCost != 0 ? "Souls: " + SoulsCost + "\n" : "";
                costString += PeopleCost != 0 ? "Followers: " + PeopleCost + "\n" : "";
                costString += MoneyCost != 0 ? "Money: " + MoneyCost + "\n" : "";
                costString += InfCost != 0 ? "Influence: " + InfCost + "\n" : "";
                costString += NotCost != 0 ? "Notority: " + NotCost + "\n" : "";

                if (costString != "")
                {
                    costString = "Costs:\n\n" + costString + "\n\n";
                }
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

        public bool CanAfford(GameManagerScript gmScript)
        {
            if (SoulsCost < 0)
            {
                if (!(gmScript.Souls + SoulsCost >= 0))
                {
                    Debug.Log("Can't afford Souls");
                    return false;
                }
            }

            if (PeopleCost < 0)
            {
                if (!(gmScript.People + PeopleCost >= 0))
                {
                    Debug.Log("Can't afford People");
                    return false;
                }
            }

            if (MoneyCost < 0)
            {
                if (!(gmScript.Money + MoneyCost >= 0))
                {
                    Debug.Log("Can't afford Money");
                    return false;
                }
            }

            if (InfCost < 0)
            {
                if (!(gmScript.Inf + InfCost >= 0))
                {
                    Debug.Log("Can't afford Influence");
                    return false;
                }
            }

            if (NotCost < 0)
            {
                if (!(gmScript.Not + NotCost >= 0))
                {
                    Debug.Log("Can't afford Notoriety");
                    return false;
                }
            }

            return true;
        }
    }

    [Serializable]
    public abstract class TileAction
    {
        public abstract bool CanUse(GameManagerScript gameManager, GridScript grid, TileInformation tile);
        public abstract string ActionName();
        public abstract void Action(GameManagerScript gameManager, GridScript grid, TileInformation tile);
    }

    [Serializable]
    public class StartingLocation : TileInformation
    {
        public StartingLocation()
        {
            TileName = "Starting Location";
            TileDescription = "A tiny shack in the woods";

            SpriteName = "StartingShack";

            DeltaMoney = 1;
            DeltaInf = 1;
        }
    }

    [Serializable]
    public class CityInformation : TileInformation
    {
        public bool BonusReceived;

        public CityInformation()
        {
            TileName = "City";
            TileDescription = "Have 10 more influence than notoriety generation around this tile to gain 10 followers.";

            SpriteName = "CityTile";

            BonusReceived = false;

            OnPlacement = new CityUnlocking(this);
        }   
    }

    [Serializable]
    public class CityUnlocking : TileAction
    {
        private CityInformation _tile;

        public CityUnlocking(CityInformation tile)
        {
            _tile = tile;
        }

        public override bool CanUse(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            return true;
        }

        public override string ActionName()
        {
            return "";
        }

        public override void Action(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            if (_tile.BonusReceived) return;

            var neighbours = grid.GetNeighbours(grid.TileList.First(t => t.TileInformation == _tile));

            var influenceCount = 0;
            foreach (var neighbour in neighbours)
            {
                influenceCount += neighbour.TileInformation.DeltaInf;
                influenceCount -= neighbour.TileInformation.DeltaNot;
            }

            if (influenceCount >= 10)
            {
                Debug.Log("Got Bonus!!!!");

                _tile.BonusReceived = true;
                gameManager.People += 10;
            }
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

            OnTurnStart = new RefreshActions();

            TileAction1 = new SacrficeAction1();
            TileAction2 = new SacrficeAction2();
            TileAction3 = new SacrficeAction3();
        }
    }

    [Serializable]
    public class SacrficeAction1 : TileAction
    {
        public override bool CanUse(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            return gameManager.People >= 1;
        }

        public override string ActionName()
        {
            return "-Follower, +Souls, +Not";
        }

        public override void Action(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            gameManager.Souls += 1;

            gameManager.People -= 1;
            gameManager.Not += 1;

            tile.AreActionsEnabled = false;
        }
    }
    [Serializable]
    public class SacrficeAction2 : TileAction
    {
        public override bool CanUse(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            return gameManager.People >= 2;
        }

        public override string ActionName()
        {
            return "-2 Followers, +2 Souls, +Not";
        }

        public override void Action(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            gameManager.Souls += 2;

            gameManager.People -= 2;
            gameManager.Not += 1;

            tile.AreActionsEnabled = false;
        }
    }
    [Serializable]
    public class SacrficeAction3 : TileAction
    {
        public override bool CanUse(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            return gameManager.People >= 3;
        }

        public override string ActionName()
        {
            return "-3 Followers, +3 Souls, +Not";
        }

        public override void Action(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            gameManager.Souls += 3;

            gameManager.People -= 3;
            gameManager.Not += 1;

            tile.AreActionsEnabled = false;
        }
    }

    [Serializable]
    public class RefreshActions : TileAction
    {
        public override bool CanUse(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            return true;
        }

        public override string ActionName()
        {
            return "";
        }

        public override void Action(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            tile.AreActionsEnabled = true;
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
            OnPlacement = new AllBuildingBuff(this);

            OnTurnStart = new RefreshActions();

            TileAction1 = new SacrficeAction1();
        }
    }

    public class AllBuildingBuff : TileAction
    {
        private TileInformation _tile;
        public AllBuildingBuff(TileInformation tile)
        {
            _tile = tile;
        }

        public override bool CanUse(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            return true;
        }

        public override string ActionName()
        {
            return "";
        }

        public override void Action(GameManagerScript gameManager, GridScript grid, TileInformation tile)
        {
            if (tile != _tile) return;

            var tileList = grid.TileList;
            var tileScript = grid.TileList.FirstOrDefault(x => x.TileInformation == tile);

            if (tileScript != null)
            {
                if (tileScript.Y%2 == 0)
                {
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X + 1 && ts.Y == tileScript.Y + 0))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X + 1 && ts.Y == tileScript.Y + 0);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y + 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y + 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                }
                else
                {
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y + 0);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X + 0 && ts.Y == tileScript.Y - 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                    if (tileList.Any(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1))
                    {
                        var buffTile = tileList.First(ts => ts.X == tileScript.X - 1 && ts.Y == tileScript.Y - 1);
                        buffTile.TileInformation.DeltaInf *= 2;
                        buffTile.TileInformation.DeltaMoney *= 2;
                    }
                }
            }

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

            NotCost = -1;
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

            NotCost = -1;
            DeltaInf = -3;
        }
    }
}
