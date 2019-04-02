using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using Harmony;

namespace RMC
{
    public static class PawnGenerator
    {
        public static Pawn GeneratePawnWithRank(RankDef rank)
        {
            Pawn pawn = Verse.PawnGenerator.GeneratePawn(new PawnGenerationRequest(rank.pawnKindDef, Find.World.factionManager.FirstFactionOfDef(rank.pawnKindDef.defaultFactionType), PawnGenerationContext.NonPlayer, -1, false, false, false, false, true, true, 1f, true, true, true, false, false, false, false, null, null, null, null, null, null, null, null));

            if(rank.destroyInventory)
            {
                pawn.inventory.DestroyAll();
                pawn.carryTracker.DestroyCarriedThing();
            }

            ForceBackstory(pawn, rank);

            if (rank.trainingDef != null) rank.trainingDef.Train(pawn);

            if (rank.weapon != null)
            {
                pawn.equipment.DestroyAllEquipment();
                pawn.equipment.AddEquipment((ThingWithComps)ThingMaker.MakeThing(rank.weapon));
            }

            if (rank.equipmentDef != null) rank.equipmentDef.Equip(pawn);

            return pawn;
        }

        private static void ForceBackstory(Pawn pawn, RankDef rank)
        {
            if (rank.childhood != null)
            {
                BackstoryDatabase.TryGetWithIdentifier(rank.childhood.identifier, out pawn.story.childhood);

                if (rank.childhood.bodyTypeDef != null)
                    pawn.story.bodyType = rank.childhood.bodyTypeDef;
            }

            if (rank.adulthood != null)
            {
                BackstoryDatabase.TryGetWithIdentifier(rank.adulthood.identifier, out pawn.story.adulthood);

                if (rank.adulthood.bodyTypeDef != null)
                    pawn.story.bodyType = rank.adulthood.bodyTypeDef;
            }

            if (rank.childhood != null || rank.adulthood != null)
                GainTraits(pawn, rank);

            if (rank.title != null)
            {
                NameTriple oldName = (NameTriple)pawn.Name;
                NameTriple newName = new NameTriple(oldName.First, rank.title + " " + oldName.Last, oldName.Last);
                pawn.Name = newName;
            }

            return;
        }

        private static void GainTraits(Pawn pawn, RankDef rank)
        {
            TraitDef newTraitDef;
            Trait newTrait;
            bool traitValid = true;
            int traitCount = 0;

            pawn.story.traits.allTraits = new List<Trait>();

            if(pawn.story.childhood.forcedTraits != null)
                foreach (TraitEntry traitEntry in pawn.story.childhood.forcedTraits)
                    pawn.story.traits.GainTrait(new Trait(traitEntry.def, traitEntry.degree));

            if (pawn.story.adulthood != null && pawn.story.adulthood.forcedTraits != null)
                foreach (TraitEntry traitEntry in pawn.story.adulthood.forcedTraits)
                    pawn.story.traits.GainTrait(new Trait(traitEntry.def, traitEntry.degree));

            traitCount = Rand.RangeInclusive(2, 3) - pawn.story.traits.allTraits.Count;

            while (traitCount > 0)
            {
                newTraitDef = DefDatabase<TraitDef>.AllDefsListForReading.RandomElementByWeight((TraitDef tr) => tr.GetGenderSpecificCommonality(pawn.gender));
                newTrait = new Trait(newTraitDef, Verse.PawnGenerator.RandomTraitDegree(newTraitDef), true);

                if (
                    (pawn.story.childhood.requiredWorkTags & newTrait.def.disabledWorkTags) != 0 ||
                    (pawn.story.childhood.workDisables & newTrait.def.requiredWorkTags) != 0
                )
                    continue;

                if (
                    pawn.story.adulthood != null && (
                        (pawn.story.adulthood.requiredWorkTags & newTrait.def.disabledWorkTags) != 0 ||
                        (pawn.story.adulthood.workDisables & newTrait.def.requiredWorkTags) != 0
                    )
                )
                    continue;

                if (pawn.story.childhood.disallowedTraits != null)
                {
                    foreach (TraitEntry traitEntry in pawn.story.childhood.disallowedTraits)
                    {
                        if (traitEntry.def.defName == newTraitDef.defName)
                        {
                            traitValid = false;
                            break;
                        }
                    }
                }

                if (!traitValid)
                {
                    traitValid = true;
                    continue;
                }

                if (pawn.story.adulthood != null && pawn.story.adulthood.disallowedTraits != null)
                {
                    foreach (TraitEntry traitEntry in pawn.story.adulthood.disallowedTraits)
                    {
                        if (traitEntry.def.defName == newTraitDef.defName)
                        {
                            traitValid = false;
                            break;
                        }
                    }
                }

                if (!traitValid)
                {
                    traitValid = true;
                    continue;
                }

                foreach (Trait curTrait in pawn.story.traits.allTraits)
                {
                    if (pawn.story.traits.HasTrait(newTrait.def) || (curTrait.def.requiredWorkTags & newTrait.def.disabledWorkTags) != 0)
                    {
                        traitValid = false;
                        break;
                    }

                    if (curTrait.def.conflictingTraits != null)
                    {
                        foreach (TraitDef conflictingTrait in curTrait.def.conflictingTraits)
                        {
                            if (conflictingTrait.defName == newTrait.def.defName)
                            {
                                traitValid = false;
                                break;
                            }
                        }
                    }
                }

                if (traitValid)
                {
                    pawn.story.traits.GainTrait(newTrait);
                    traitCount--;
                }
                else traitValid = true;
            }

            /* Without this Pawns can still do disabled work */
            pawn.workSettings.EnableAndInitialize();

            /* Removes passion sybmols for disabled skills in the Bio tab */
            foreach (SkillRecord skill in pawn.skills.skills)
            {
                if (skill.TotallyDisabled)
                {
                    skill.Level = 0;
                    skill.passion = Passion.None;
                }
            }

            return;
        }
    }
}