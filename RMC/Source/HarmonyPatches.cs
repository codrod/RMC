using Verse;
using Verse.AI;
using Verse.AI.Group;
using System.Reflection;
using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HarmonyLib;

/*
 * Notes:
 * 1) The pawn generator class only takes Factions not FactionDefs. Which casues a problem if you try mix armies since the faction def in the visting armies pawn kind def does not exist. will
 * probably have to something drastic to fix this.
 * 2) Generated backstories may contradict the forced (disallowed) traits/work tags if the backstories are not forced?
 * 3) ISC should not have relatives!
 */

namespace RMC
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            Log.Message("RMC: Started");

            var harmony = new Harmony("com.github.codrod.RMC");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            foreach(BackstoryDef backstoryDef in DefDatabase<BackstoryDef>.AllDefs)
                if(backstoryDef.isNewBackstory)
                    BackstoryDatabase.AddBackstory(backstoryDef.NewBackstory());

            Log.Message("RMC: Loaded");
        }
    }
}

