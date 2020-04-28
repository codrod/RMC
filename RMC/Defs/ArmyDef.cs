﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace RMC
{
    public class ArmyDef : Def
    {
        public FactionDef factionDef = null;
        public bool useDropPods = true;
        public List<UnitDef> unitList = new List<UnitDef>();
        public List<RankDef> rankList = new List<RankDef>();
        public UnitDef startingUnit = new UnitDef();

        public ArmyDef()
        {

        }

        public static ArmyDef Named(string defName)
        {
            return DefDatabase<ArmyDef>.GetNamed(defName);
        }

        public static ArmyDef GetFactionArmy(Faction faction)
        {
            ArmyDef army = null;

            foreach (ArmyDef curArmy in DefDatabase<ArmyDef>.AllDefs)
            {
                if (faction.def.defName == curArmy.factionDef.defName)
                {
                    army = curArmy;
                    break;
                }
            }

            return army;
        }

        public RankDef GetPawnRank(Pawn pawn)
        {
            foreach (RankDef rank in rankList)
                if (pawn.kindDef.defName == rank.pawnKindDef.defName)
                    return rank;

            return null;
        }

        public UnitDef CreateUnitOfPawns(IEnumerable<Pawn> pawns)
        {
            UnitDef unit = new UnitDef();

            foreach (Pawn pawn in pawns)
                unit.Add(GetPawnRank(pawn));

            return unit;
        }

        public UnitDef GetAllSoldiersInArmy()
        {
            return CreateUnitOfPawns(PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists);
        }

        public UnitDef GetAllSoldiersInMap(Map map)
        {
            return CreateUnitOfPawns(map.mapPawns.FreeColonistsAndPrisoners);
        }

        public IEnumerable<Thing> SendToMap(IEnumerable<Thing> things, Map map, IntVec3 centerCell)
        {
            if (useDropPods == true)
                DropPodUtility.DropThingsNear(centerCell, map, things);
            else
                MapUtilities.SpawnThingsInMap(map, centerCell, things);

            return things;
        }
    }
}
