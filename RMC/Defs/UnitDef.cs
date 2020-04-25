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
        public Dictionary<RankDef, RankCount> soldierList = new Dictionary<RankDef, RankCount>();

        public UnitDef()
        {
            return;
        }

        public UnitDef(UnitDef otherUnit)
        {
            soldierList = new Dictionary<RankDef, RankCount>();

            foreach (RankDef rank in otherUnit.soldierList.Keys)
                soldierList.Add(rank, new RankCount(otherUnit.soldierList[rank]));

            return;
        }

        public UnitDef Add(RankDef rank)
        {
            if (rank != null)
            {
                if (soldierList.ContainsKey(rank)) soldierList[rank].count++;
                else soldierList.Add(rank, new RankCount(1));
            }

            return this;
        }

        public float GetUnitCost()
        {
            float totalCost = 0f;

            foreach (RankDef rank in soldierList.Keys)
            {
                totalCost += soldierList[rank].count * rank.cost;
            }

            return totalCost;
        }

        public int GetUnitSpawnTime()
        {
            int totalTime = 0;

            foreach (RankDef rank in soldierList.Keys)
                totalTime += soldierList[rank].count * rank.spawnTime;

            return totalTime;
        }

        public UnitDef SubtractUnit(UnitDef otherUnit)
        {
            UnitDef newUnit = new UnitDef(this);

            foreach (RankDef otherRank in otherUnit.soldierList.Keys)
            {
                if (newUnit.soldierList.ContainsKey(otherRank))
                {
                    if (newUnit.soldierList[otherRank].count > otherUnit.soldierList[otherRank].count)
                        newUnit.soldierList[otherRank].count = newUnit.soldierList[otherRank].count - otherUnit.soldierList[otherRank].count;
                    else
                        newUnit.soldierList.Remove(otherRank);
                }
            }

            return newUnit;
        }

        public UnitDef SetRandomUnitSize()
        {
            UnitDef newUnit = new UnitDef(this);

            foreach (RankDef rank in newUnit.soldierList.Keys)
                if (newUnit.soldierList[rank].max > 0)
                    newUnit.soldierList[rank].count = Rand.RangeInclusive(newUnit.soldierList[rank].min, newUnit.soldierList[rank].max);

            return newUnit;
        }

        public int GetUnitSize()
        {
            int size = 0;

            foreach (KeyValuePair<RankDef, RankCount> rankCount in soldierList)
                size += rankCount.Value.count;

            return size;
        }

        public static UnitDef Named(string defName)
        {
            return DefDatabase<UnitDef>.GetNamed(defName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public Dictionary<RankDef, RankCount>.Enumerator GetEnumerator()
        {
            return soldierList.GetEnumerator();
        }

        /*
        public override string ToString()
        {
            string str = "";

            foreach (RankDef rank in soldierList.Keys)
            {
                str += rank.label + ": " + soldierList[rank].count + "\n";
            }
                
            return str;
        }
        */
    }

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
}
