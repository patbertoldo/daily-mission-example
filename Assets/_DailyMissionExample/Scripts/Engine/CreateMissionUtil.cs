using DailyMissions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DailyMissions
{
    /// <summary>
    /// Use this utility to automatically add the mission type for the specific mission.
    /// </summary>
    public class CreateMissionUtil
    {
        [MenuItem("Assets/Create/Data Files/Daily Missions/Craft Items")]
        public static void CraftItems()
        {
            string filePath = $"{AssetDatabase.GetAssetPath(Selection.activeObject)}/CraftItems.asset";

            CraftItems craftItemsMission = ScriptableObject.CreateInstance<CraftItems>();
            craftItemsMission.Initialise(MissionTypes.CraftItems);

            ProjectWindowUtil.CreateAsset(craftItemsMission, filePath);
        }
    }
}