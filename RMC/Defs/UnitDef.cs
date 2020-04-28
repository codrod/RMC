using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace RMC
{
    public class UnitDef : Def, IEnumerable
    {
        public Dictionary<RankDef, int> soldiers = new Dictionary<RankDef, int>();

        public UnitDef() {}

        public UnitDef(UnitDef otherUnit)
        {
            soldiers = new Dictionary<RankDef, int>();

            foreach (RankDef rank in otherUnit.soldiers.Keys)
                soldiers.Add(rank, otherUnit.soldiers[rank]);

            return;
        }

        public UnitDef Add(RankDef rank)
        {
            if (rank != null)
            {
                if (soldiers.ContainsKey(rank)) soldiers[rank]++;
                else soldiers.Add(rank, 1);
            }

            return this;
        }

        public float GetUnitCost()
        {
            float totalCost = 0f;

            foreach (RankDef rank in soldiers.Keys)
            {
                totalCost += soldiers[rank] * rank.cost;
            }

            return totalCost;
        }

        public int GetUnitSpawnTime()
        {
            int spawnTime = 0;

            foreach (RankDef rank in soldiers.Keys)
                if (rank.spawnTime * soldiers[rank] > spawnTime)
                    spawnTime = rank.spawnTime * soldiers[rank];

            return spawnTime;
        }

        public UnitDef SubtractUnit(UnitDef otherUnit)
        {
            UnitDef newUnit = new UnitDef(this);

            foreach (RankDef otherRank in otherUnit.soldiers.Keys)
            {
                if (newUnit.soldiers.ContainsKey(otherRank))
                {
                    if (newUnit.soldiers[otherRank] > otherUnit.soldiers[otherRank])
                        newUnit.soldiers[otherRank] = newUnit.soldiers[otherRank] - otherUnit.soldiers[otherRank];
                    else
                        newUnit.soldiers.Remove(otherRank);
                }
            }

            return newUnit;
        }

        /*
        public UnitDef GenerateRandomUnit()
        {
            UnitDef newUnit = new UnitDef(this);

            foreach (RankDef rank in newUnit.soldiers.Keys)
                if (newUnit.soldiers[rank].max > 0)
                    newUnit.soldiers[rank].count = Rand.RangeInclusive(newUnit.soldiers[rank].min, newUnit.soldiers[rank].max);

            return newUnit;
        }
        */

        public int GetUnitSize()
        {
            int size = 0;

            foreach (KeyValuePair<RankDef, int> rankCount in soldiers)
                size += rankCount.Value;

            return size;
        }

        public List<Pawn> GenerateUnit()
        {
            List<Pawn> pawns = new List<Pawn>();

            foreach (KeyValuePair<RankDef, int> rankCount in soldiers)
                for (int j = 0; j < rankCount.Value; j++)
                    pawns.Add(SoldierGenerator.GenerateSoldier(rankCount.Key));

            return pawns;
        }

        public static UnitDef Named(string defName)
        {
            return DefDatabase<UnitDef>.GetNamed(defName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public Dictionary<RankDef, int>.Enumerator GetEnumerator()
        {
            return soldiers.GetEnumerator();
        }

        public override string ToString()
        {
            string str = "";

            foreach (RankDef rank in soldiers.Keys)
            {
                str += rank.label + ": " + soldiers[rank] + "\n";
            }
                
            return str;
        }
    }

    /*
    public class RankCount
    {
        public int min = 0;
        public int max = 0;
        public int count = 0;
        //See RMC.Dialog_Recruit
        public string editBuffer = "";

        public RankCount()
        {
            return;
        }

        public RankCount(RankCount otherRankCount)
        {
            min = otherRankCount.min;
            max = otherRankCount.max;
            count = otherRankCount.count;

            return;
        }

        public RankCount(int count)
        {
            this.count = count;

            return;
        }
    }
    */
}
