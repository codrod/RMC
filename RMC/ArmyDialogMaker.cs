using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace RMC
{
    public static class ArmyDialogMaker
    {
        public static UnitDef reinforcements = new UnitDef();
        public static int arrivalTick = 0;

        public static DiaNode MakeDialog(Pawn negotiator)
        {
            DiaNode rootNode = new DiaNode("Hello");

            rootNode.options.Add(MakeReinforceDialog(negotiator, rootNode));

            DiaOption disconnectOption = new DiaOption("(" + "Disconnect".Translate() + ")");
            disconnectOption.resolveTree = true;
            rootNode.options.Add(disconnectOption);

            return rootNode;
        }

        public static DiaOption MakeReinforceDialog(Pawn negotiator, DiaNode rootNode)
        {
            ArmyDef army = ArmyDef.GetFactionArmy(negotiator.Faction);
            //UnitDef reinforcements = army.GetReinforcements(negotiator.Map);

            DiaOption reinforceOption = new DiaOption("Request reinforcements");

            if (Find.TickManager.TicksGame > arrivalTick)
            {
                DiaNode unitDialog = new DiaNode("Choose a unit:");
                reinforceOption.link = unitDialog;

                foreach (UnitDef unit in army.unitList)
                {
                    DiaOption option = new DiaOption(unit.label);

                    if (unit.SubtractUnit(army.GetAllSoldiersInArmy()).GetUnitSize() > 0)
                    {
                        option.action = delegate
                        {
                            reinforcements = unit.SubtractUnit(army.GetAllSoldiersInArmy());
                            option.link = MakeConfirmReinforceDialog(negotiator, rootNode, army);
                        };
                    }
                    else
                        option.Disable("Already reinforced");

                    unitDialog.options.Add(option);
                }

                unitDialog.options.Add(new DiaOption("Cancel")
                {
                    link = rootNode
                });
            }
            else
                reinforceOption.Disable("Reinforcements are coming");

            return reinforceOption;
        }

        public static DiaNode MakeConfirmReinforceDialog(Pawn negotiator, DiaNode rootNode, ArmyDef army)
        {
            DiaNode confirmDialog = new DiaNode("The reinforcements will cost " + reinforcements.GetUnitCost() + " silver and take " + (float)reinforcements.GetUnitSpawnTime() / GenDate.TicksPerDay + " days to arrive.");

            DiaOption confirmOption = new DiaOption("Confirm".Translate());
            confirmDialog.options.Add(confirmOption);
            if (negotiator.Map.resourceCounter.Silver >= reinforcements.GetUnitCost())
            {
                confirmOption.action = delegate
                {
                    rootNode.options[0].Disable("Reinforcements are coming");

                    MapUtilities.DestroyThingsInMap(negotiator.Map, ThingDef.Named("Silver"), reinforcements.GetUnitCost());

                    arrivalTick = Find.TickManager.TicksGame + reinforcements.GetUnitSpawnTime();

                    IncidentParms incidentParms = new IncidentParms();
                    incidentParms.target = negotiator.Map;
                    incidentParms.faction = negotiator.Faction;
                    incidentParms.forced = true;
                    Find.Storyteller.incidentQueue.Add(army.reinforceIncident, arrivalTick /*120000*/, incidentParms, 240000);
                };
            }
            else
                confirmOption.Disable("Not enough silver");

            confirmDialog.options.Add(new DiaOption("Cancel")
            {
                link = rootNode
            });

            DiaNode sentDialog = new DiaNode("The reinforcements will arrive in " + (float)reinforcements.GetUnitSpawnTime() / GenDate.TicksPerDay + " days.");
            confirmOption.link = sentDialog;
            sentDialog.options.Add(new DiaOption("OK".Translate())
            {
                link = rootNode
            });

            return confirmDialog;
        }
    }
}
