using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.Sound;
using UnityEngine;

namespace RMC
{
    public class Dialog_Trade : Window
    {
        protected Pawn negotiator;
        protected static readonly Vector2 AcceptButtonSize = new Vector2(160f, 40f);
        protected static readonly Vector2 OtherBottomButtonSize = new Vector2(160f, 40f);
        private Vector2 scrollPosition;
        ArmyDef army;
        UnitDef reinforcements = new UnitDef();

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(720f, 600f);
            }
        }

        public Dialog_Trade(Pawn negotiator, bool radioMode)
        {
            this.negotiator = negotiator;
            this.army = ArmyDef.GetFactionArmy(negotiator.Faction);

            this.forcePause = true;
            this.absorbInputAroundWindow = true;
            this.soundAppear = SoundDefOf.CommsWindow_Open;
            this.soundClose = SoundDefOf.CommsWindow_Close;

            for (int i = 0; i < army.rankList.Count; i++)
                reinforcements.Add(army.rankList[i]);
        }

        public override void DoWindowContents(Rect inRect)
        {
            GUI.BeginGroup(inRect);
            inRect = inRect.AtZero();

            Rect position = new Rect(0f, 0f, inRect.width, 58f);
            GUI.BeginGroup(position);
            Text.Font = GameFont.Medium;

            Rect rect = new Rect(0f, 0f, position.width / 2f, position.height);
            Text.Anchor = TextAnchor.UpperLeft;
            Widgets.Label(rect, Faction.OfPlayer.Name.Truncate(rect.width, null));

            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.UpperLeft;
            Rect rect3 = new Rect(0f, 27f, position.width / 2f, position.height / 2f);
            Widgets.Label(rect3, "Negotiator".Translate() + ": " + negotiator.LabelShort);

            GUI.EndGroup();

            Rect rect6 = new Rect(0f, 58f, 116f, 30f);
            Widgets.Label(rect6, negotiator.Map.resourceCounter.Silver.ToString());

            Rect rect62 = new Rect(inRect.width - 116f, 58f, 100f, 30f);
            GUI.color = Color.red;
            Text.Anchor = TextAnchor.MiddleRight;
            Widgets.Label(rect62, "-" + reinforcements.GetUnitCost().ToString());

            Text.Anchor = TextAnchor.MiddleLeft;
            GUI.color = Color.white;

            float num3 = inRect.width - 16f;
            Widgets.DrawLineHorizontal(0f, 87f, num3);
            float num2 = 30f;

            Rect mainRect = new Rect(0f, 58f + num2, inRect.width, inRect.height - 58f - 38f - num2 - 20f);

            this.FillMainRect(mainRect);

            Rect rect7 = new Rect(inRect.width / 2f - Dialog_Trade.AcceptButtonSize.x / 2f, inRect.height - 55f, Dialog_Trade.AcceptButtonSize.x, Dialog_Trade.AcceptButtonSize.y);
            if (Widgets.ButtonText(rect7, "AcceptButton".Translate(), true, false, true))
            {
                Action action = delegate
                {
                    MapUtilities.DestroyThingsInMap(negotiator.Map, ThingDef.Named("Silver"), (int)reinforcements.GetUnitCost());

                    int arrivalTick = Find.TickManager.TicksGame + reinforcements.GetUnitSpawnTime();

                    IncidentParms_Deploy incidentParms = new IncidentParms_Deploy();
                    incidentParms.target = negotiator.Map;
                    incidentParms.faction = negotiator.Faction;
                    incidentParms.forced = true;
                    incidentParms.reinforcements = reinforcements;
                    Find.Storyteller.incidentQueue.Add(DefDatabase<IncidentDef>.GetNamed("RMC_IncidentDef_Deploy"), arrivalTick, incidentParms, 240000);

                    this.Close(true);
                };

                if (negotiator.Map.resourceCounter.Silver >= reinforcements.GetUnitCost())
                {
                    action();
                }
                else
                {
                    SoundDefOf.ClickReject.PlayOneShotOnCamera(null);
                    Messages.Message("MessageColonyCannotAfford".Translate(), MessageTypeDefOf.RejectInput, false);
                }

                Event.current.Use();
            }

            Rect rect8 = new Rect(rect7.x - 10f - Dialog_Trade.OtherBottomButtonSize.x, rect7.y, Dialog_Trade.OtherBottomButtonSize.x, Dialog_Trade.OtherBottomButtonSize.y);
            if (Widgets.ButtonText(rect8, "ResetButton".Translate(), true, false, true))
            {
                SoundDefOf.Tick_Low.PlayOneShotOnCamera(null);

                reinforcements.soldierList.Clear();

                for (int i = 0; i < army.rankList.Count; i++)
                    reinforcements.Add(army.rankList[i]);
            }

            Rect rect9 = new Rect(rect7.xMax + 10f, rect7.y, Dialog_Trade.OtherBottomButtonSize.x, Dialog_Trade.OtherBottomButtonSize.y);
            if (Widgets.ButtonText(rect9, "CancelButton".Translate(), true, false, true))
            {
                this.Close(true);
                Event.current.Use();
            }

            GUI.EndGroup();
        }

        private void FillMainRect(Rect mainRect)
        {
            Text.Font = GameFont.Small;
            float height = 6f + army.rankList.Count * 30f;
            Rect viewRect = new Rect(0f, 0f, mainRect.width - 16f, height);
            Widgets.BeginScrollView(mainRect, ref this.scrollPosition, viewRect, true);
            float num = 6f;
            float num2 = this.scrollPosition.y - 30f;
            float num3 = this.scrollPosition.y + mainRect.height;
            int i = 0;

            foreach (RankDef rank in reinforcements.soldierList.Keys)
            {
                if (num > num2 && num < num3)
                {
                    Rect rect = new Rect(0f, num, viewRect.width, 30f);
                    DrawTradeableRow(rect, i, rank, reinforcements.soldierList[rank]);
                }

                num += 30f;
                i++;
            }

            Widgets.EndScrollView();
        }

        public void DrawTradeableRow(Rect rect, int index, RankDef rank, RankCount rankCount)
        {
            if (index % 2 == 1)
            {
                Widgets.DrawLightHighlight(rect);
            }
            Text.Font = GameFont.Small;
            GUI.BeginGroup(rect);

            float num = rect.width;

            Rect rect2 = new Rect(0f, 0f, 150f, rect.height);
            if (Mouse.IsOver(rect2))
            {
                Widgets.DrawHighlight(rect2);
            }

            Rect rect3 = new Rect(num - 75f, 0f, 75f, rect.height);
            rect3.xMin += 5f;
            rect3.xMax -= 5f;
            Widgets.Label(rect2, rank.label);

            Rect rect4 = new Rect(150f, 0f, 75f, rect.height);
            Widgets.Label(rect4, GenText.ToStringMoney(rank.cost));

            num -= 175f;
            Rect rect5 = new Rect(num - 240f, 0f, 240f, rect.height);

            Rect rect6 = rect5.ContractedBy(2f);
            rect3.xMax -= 15f;
            rect3.xMin += 16f;

            Widgets.TextFieldNumeric<int>(rect3, ref rankCount.count, ref rankCount.editBuffer, 0.0f, 99.0f);

            num -= 240f;
            num -= 175f;
            GenUI.ResetLabelAlign();
            GUI.EndGroup();
        }
    }

    /*
public static class ArmyDialogMaker
{
//public static UnitDef reinforcements = new UnitDef();
//public static int arrivalTick = 0;

/*
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
            Find.Storyteller.incidentQueue.Add(army.reinforceIncident, arrivalTick, incidentParms, 240000);
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
*/

    /*
    public class Dialog_Reinforce : Dialog_NodeTree
    {
        protected Pawn negotiator;
        private const float TitleHeight = 70f;
        private const float InfoHeight = 60f;

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(720f, 600f);
            }
        }

        public Dialog_Reinforce(Pawn negotiator,  bool radioMode) : base(ArmyDialogMaker.MakeDialog(negotiator), radioMode, false, null)
        {
            this.negotiator = negotiator;
        }

        //FactionDialogMaker
        public override void DoWindowContents(Rect inRect)
        {
            GUI.BeginGroup(inRect);
            Rect rect = new Rect(0f, 0f, inRect.width / 2f, 70f);
            Rect rect2 = new Rect(0f, rect.yMax, rect.width, 60f);
            Rect rect3 = new Rect(inRect.width / 2f, 0f, inRect.width / 2f, 70f);
            Rect rect4 = new Rect(inRect.width / 2f, rect.yMax, rect.width, 60f);
            Text.Font = GameFont.Medium;
            Widgets.Label(rect, this.negotiator.LabelCap);
            Text.Anchor = TextAnchor.UpperRight;
            Widgets.Label(rect3, negotiator.Faction.Name);
            Text.Anchor = TextAnchor.UpperLeft;
            Text.Font = GameFont.Small;
            GUI.color = new Color(1f, 1f, 1f, 0.7f);
            Widgets.Label(rect2, "SocialSkillIs".Translate(negotiator.skills.GetSkill(SkillDefOf.Social).Level));
            Text.Anchor = TextAnchor.UpperRight;
            Widgets.Label(rect4, ((Settlement)negotiator.Map.Parent).Name);

            Text.Anchor = TextAnchor.UpperLeft;
            GUI.color = Color.white;
            GUI.EndGroup();
            float num = 147f;
            Rect rect6 = new Rect(0f, num, inRect.width, inRect.height - num);
            base.DrawNode(rect6);
        }
    }
    */
}
