using Verse;
using Verse.AI;
using Verse.AI.Group;
using Harmony;
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
    public class IncidentWorker_Deploy : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 arrivalCell = new IntVec3();
            ArmyDef armyDef = null;
            UnitDef reinforcements = ArmyDialogMaker.reinforcements;

            armyDef = ArmyDef.GetFactionArmy(parms.faction);

            if (armyDef.useDropPods == true || armyDef.deployIncident.defName == def.defName)
                RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith(MapUtilities.IsSafeLandingAreaInMap(map), map, out arrivalCell);
            else
                RCellFinder.TryFindRandomPawnEntryCell(out arrivalCell, map, 1.0f);

            armyDef.SendToMap(armyDef.GenerateUnit(reinforcements).Cast<Thing>(), map, arrivalCell);
            armyDef.GiveMinFood(armyDef.GetAllSoldiersInMap(map).GetUnitSize(), map, arrivalCell);

            if (def.defName == armyDef.reinforceIncident.defName)
                Find.LetterStack.ReceiveLetter(def.letterLabel, def.letterText, LetterDefOf.PositiveEvent, new TargetInfo(arrivalCell, map, false));

            return true;
        }
    }
}