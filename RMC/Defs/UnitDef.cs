using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace RMC
{
    public class UnitDef : Def, IEnumerable, IExposable
    {
        private Dictionary<RankDef, int> soldiers = new Dictionary<RankDef, int>();

        public UnitDef() {}

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
                totalCost += soldiers[rank] * rank.cost;

            return totalCost;
        }

        public int GetSpawnTime()
        {
            int spawnTime = 0;

            foreach (RankDef rank in soldiers.Keys)
                if (rank.spawnTime * soldiers[rank] > spawnTime)
                    spawnTime = rank.spawnTime * soldiers[rank];

            return spawnTime;
        }

        public static UnitDef CreateUnitFromArrays(RankDef[] ranks, int[] counts)
        {
            UnitDef unit = new UnitDef();

            for (int i = 0; i < ranks.Length; i++)
                if (counts[i] > 0)
                    unit.soldiers.Add(ranks[i], counts[i]);

            return unit;
        }

        public int GetSize()
        {
            int size = 0;

            foreach (KeyValuePair<RankDef, int> rankCount in soldiers)
                size += rankCount.Value;

            return size;
        }

        public List<Pawn> Spawn()
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

        public void ExposeData()
        {
            Scribe_Collections.Look(ref soldiers, "soldiers", LookMode.Def, LookMode.Value);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)soldiers).GetEnumerator();
        }

        public override string ToString()
        {
            string str = "";

            foreach (RankDef rank in soldiers.Keys)
                str += rank.label + ": " + soldiers[rank] + "\n";
                
            return str;
        }
    }
}
