using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DailyMissions
{
    public class CraftItems : DailyMission
    {
        protected override bool AdditionalRequirements { get { return true; /* return PlayerStatistics.UnlockedCraftingBench; */ } }
    }
}
