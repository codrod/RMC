using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RMC
{
    public class SoldierGenerator
    {
        Pawn pawn;
        TraitDef newTraitDef;
        Trait newTrait;

        public Pawn Generate(RankDef rank)
        {
            //also consider using the disallowed/allowed traits options on the pawn generator
            pawn = Verse.PawnGenerator.GeneratePawn(new PawnGenerationRequest(rank.pawnKindDef, Find.World.factionManager.FirstFactionOfDef(rank.pawnKindDef.defaultFactionType), PawnGenerationContext.NonPlayer, -1, false, false, false, false, true, true, 1f, true, true, true, false, false, false, false, false, 0.0f, null, 0.0f, null, null, null, null));

            ForceFaction();
            ForceBackstory(rank);

            if (rank.trainingDef != null) rank.trainingDef.Train(pawn);

            if (rank.destroyInventory)
            {
                pawn.inventory.DestroyAll();
                pawn.carryTracker.DestroyCarriedThing();
            }

            if (rank.weapon != null)
            {
                pawn.equipment.DestroyAllEquipment();
                pawn.equipment.AddEquipment((ThingWithComps)ThingMaker.MakeThing(rank.weapon));
            }

            if (rank.equipmentDef != null) rank.equipmentDef.Equip(pawn);

            return pawn;
        }

        void ForceFaction()
        {
            (pawn as Thing).GetType().GetField("factionInt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(pawn, Find.World.factionManager.OfPlayer);
        }

        void ForceBackstory(RankDef rank)
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
                ForceTraits(rank);

            if (rank.title != null)
            {
                NameTriple oldName = (NameTriple)pawn.Name;
                NameTriple newName = new NameTriple(oldName.First, rank.title + " " + oldName.Last, oldName.Last);
                pawn.Name = newName;
            }

            if (rank.chronologicalAge > -1)
            {
                try
                {
                    pawn.ageTracker.AgeChronologicalTicks = rank.chronologicalAge * 3600000;
                }
                catch (Exception ex)
                {
                    Log.Error($"RMC: Chronological age (in ticks) for RankDef '{rank.defName}' caused overflow: {rank.chronologicalAge}");
                }
            }

            return;
        }

        void ForceTraits(RankDef rank)
        {
            pawn.story.traits.allTraits = new List<Trait>();

            if(pawn.story.childhood.forcedTraits != null)
                foreach (TraitEntry traitEntry in pawn.story.childhood.forcedTraits)
                    pawn.story.traits.GainTrait(new Trait(traitEntry.def, traitEntry.degree));

            if (pawn.story.adulthood != null && pawn.story.adulthood.forcedTraits != null)
                foreach (TraitEntry traitEntry in pawn.story.adulthood.forcedTraits)
                    pawn.story.traits.GainTrait(new Trait(traitEntry.def, traitEntry.degree));

            ForceRandomTraits(rank);

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

        void ForceRandomTraits(RankDef rank)
        {
            int traitCount = 0;

            traitCount = Rand.RangeInclusive(1, 3) - pawn.story.traits.allTraits.Count;

            while (traitCount > 0)
            {
                newTraitDef = DefDatabase<TraitDef>.AllDefsListForReading.RandomElementByWeight((TraitDef tr) => tr.GetGenderSpecificCommonality(pawn.gender));
                newTrait = new Trait(newTraitDef, Verse.PawnGenerator.RandomTraitDegree(newTraitDef), true);

                if (TraitIsValid(rank))
                {
                    pawn.story.traits.GainTrait(newTrait);
                    traitCount--;
                }
            }
        }

        bool TraitIsValid(RankDef rank)
        {
            if (WorkTagsConflict())
                return false;

            if (TraitIsDisallowed())
                return false;

            if (TraitConflictsWithExistingTraits())
                return false;

            return true;
        }

        bool WorkTagsConflict()
        {
            if (
                (pawn.story.childhood.requiredWorkTags & newTrait.def.disabledWorkTags) != 0 ||
                (pawn.story.childhood.workDisables & newTrait.def.requiredWorkTags) != 0
            )
                return true;

            if (
                pawn.story.adulthood != null && (
                    (pawn.story.adulthood.requiredWorkTags & newTrait.def.disabledWorkTags) != 0 ||
                    (pawn.story.adulthood.workDisables & newTrait.def.requiredWorkTags) != 0
                )
            )
                return true;

            return false;
        }

        bool TraitIsDisallowed()
        {
            if (pawn.story.childhood.disallowedTraits != null)
            {
                foreach (TraitEntry traitEntry in pawn.story.childhood.disallowedTraits)
                {
                    if (traitEntry.def.defName == newTraitDef.defName)
                    {
                        return true;
                    }
                }
            }

            if (pawn.story.adulthood != null && pawn.story.adulthood.disallowedTraits != null)
            {
                foreach (TraitEntry traitEntry in pawn.story.adulthood.disallowedTraits)
                {
                    if (traitEntry.def.defName == newTraitDef.defName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        bool TraitConflictsWithExistingTraits()
        {
            foreach (Trait curTrait in pawn.story.traits.allTraits)
            {
                if (pawn.story.traits.HasTrait(newTrait.def) || (curTrait.def.requiredWorkTags & newTrait.def.disabledWorkTags) != 0)
                {
                    return true;
                }

                if (curTrait.def.conflictingTraits != null)
                {
                    foreach (TraitDef conflictingTrait in curTrait.def.conflictingTraits)
                    {
                        if (conflictingTrait.defName == newTrait.def.defName)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}