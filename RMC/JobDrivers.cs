using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI;
using UnityEngine;

namespace RMC
{
    public class JobDriver_UseCommsConsole : RimWorld.JobDriver_UseCommsConsole
    {
        protected override IEnumerable<Toil> MakeNewToils()
        {
            List<Toil> toils = new List<Toil>();

            toils.Add(Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell));

            Toil openMenuToil = new Toil();

            openMenuToil.initAction = delegate
            {
                Dialog_Negotiation dialog_Negotiation = new Dialog_Negotiation(this.pawn, true);
                dialog_Negotiation.soundAmbient = SoundDefOf.RadioComms_Ambience;
                Find.WindowStack.Add(dialog_Negotiation);
            };

            openMenuToil.defaultCompleteMode = ToilCompleteMode.Instant;
            openMenuToil.FailOnDespawnedOrNull(TargetIndex.A);

            toils.Add(openMenuToil);

            return toils;
        } 
    }

    public class Dialog_Negotiation : Dialog_NodeTree
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

        public Dialog_Negotiation(Pawn negotiator,  bool radioMode) : base(ArmyDialogMaker.MakeDialog(negotiator), radioMode, false, null)
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

                /*FactionRelationKind playerRelationKind = FactionRelationKind.Ally;
                GUI.color = playerRelationKind.GetColor();
                Rect rect5 = new Rect(rect4.x, rect4.y + Text.CalcHeight(playerRelationKind.GetLabel(), rect4.width) + Text.SpaceBetweenLines, rect4.width, 30f);
                Widgets.Label(rect5, playerRelationKind.GetLabel());*/

            Text.Anchor = TextAnchor.UpperLeft;
            GUI.color = Color.white;
            GUI.EndGroup();
            float num = 147f;
            Rect rect6 = new Rect(0f, num, inRect.width, inRect.height - num);
            base.DrawNode(rect6);
        }
    }
}
