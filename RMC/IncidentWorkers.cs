using Verse;
using Verse.AI;
using Verse.AI.Group;
using System.Reflection;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RMC
{
    public class IncidentParms_Deploy : IncidentParms
    {
        public UnitDef reinforcements = new UnitDef();
    }

    public class IncidentWorker_Deploy : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 arrivalCell = new IntVec3();
            ArmyDef armyDef = null;
            UnitDef reinforcements;

            Log.Message("" + GenDate.DaysPassed + " "+parms.faction);

            if (parms.faction == null)
            {
                if (GenDate.DaysPassed == 0)
                    parms.faction = map.ParentFaction;
                else
                    return true;
            }

            armyDef = ArmyDef.GetFactionArmy(parms.faction);

            try
            {
                if (GenDate.DaysPassed == 0)
                    reinforcements = armyDef.startingUnit;
                else
                    reinforcements = ((IncidentParms_Deploy)parms).reinforcements;
            }
            catch (InvalidCastException)
            {
                reinforcements = new UnitDef();
            }

            if (armyDef.useDropPods == true || GenDate.DaysPassed == 0)
                RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith(MapUtilities.IsSafeLandingAreaInMap(map), map, out arrivalCell);
            else
                RCellFinder.TryFindRandomPawnEntryCell(out arrivalCell, map, 1.0f);

            armyDef.SendToMap(armyDef.GenerateUnit(reinforcements).Cast<Thing>(), map, arrivalCell);

            if(GenDate.DaysPassed > 0)
                Find.LetterStack.ReceiveLetter(def.letterLabel, def.letterText, LetterDefOf.PositiveEvent, new TargetInfo(arrivalCell, map, false));

            return true;
        }
    }
}