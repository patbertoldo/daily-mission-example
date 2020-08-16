using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DailyMissions
{
    public enum Difficulties
    {
        Easy,
        Medium,
        Hard
    }

    public enum MissionTypes
    {
        CompleteLevels,
        KillEnemiesWithMelee,
        KillEnemiesWithArrows,
        KillEnemiesWithFireball,
        KillEnemiesWithStorm,
        StunEnemies,
        SummonMinions,
        SurviveForTime,
        CraftItems
    }

    [CreateAssetMenu(fileName = "Daily Mission", menuName = "Data Files/Daily Missions/Daily Mission", order = 0)]
    public class DailyMission : ScriptableObject
    {
        [SerializeField]
        private Difficulties difficulty;
        public Difficulties Difficulty { get { return difficulty; } }
        // Depending on style:
        // public Difficulties Difficulty => difficulty;

        [SerializeField]
        protected MissionTypes missionType;
        public MissionTypes MissionType { get { return missionType; } }

        [SerializeField]
        private int goal = 1;
        public int Goal { get { return goal; } }

        [SerializeField, Tooltip("Player level must be equal to or higher than this number before it can be offered.")]
        private int levelRequirement;
        public int LevelRequirement {  get { return levelRequirement; } }

        /// <summary>
        /// Override this for missions that have additional requirements before they can be offered.
        /// </summary>
        protected virtual bool AdditionalRequirements { get { return true; } }

        public bool OfferRequirementsComplete { get { return /*PlayerStatistics.CurrentLevel >= LevelRequirement &&*/ AdditionalRequirements; } }

        [SerializeField, Tooltip("Missions with the same GroupID cannot be offered at the same time. Value of '0' is the exception.")]
        private int groupID = 0;
        public int GroupID { get { return groupID; } }

        // When using translation sheets, replace the value of this string with the translation key.
        [SerializeField]
        private string description;
        public string Description { get { return description; } }

        [SerializeField]
        private Sprite icon;
        public Sprite Icon { get { return icon; } }

        private int progress;
        public int Progress { get { return progress; } set { progress = value; } }

        public bool IsComplete { get { return progress >= goal; } }

        // Making these basic for the simplicity of the example, rewards should have their own dedicated system.
        private int rewardAmount;
        public int RewardAmount { get { return rewardAmount; } }

        private string rewardType;
        public string RewardType { get { return rewardType; } }

        private bool isClaimed;
        public bool IsClaimed { get { return isClaimed; } set { isClaimed = value; } }

        public Dictionary<string, object> GetSaveData()
        {
            return new Dictionary<string, object>()
            {
                { "Difficulty", difficulty.ToString() },
                { "MissionType", missionType.ToString() },
                { "MissionGoal", goal },
                { "Progress", progress },
                { "RewardType", rewardType },
                { "RewardAmount", rewardAmount },
                { "IsClaimed", isClaimed }
            };
        }

        public void Initialise()
        {
            isClaimed = false;
        }

        public void Initialise(MissionTypes missionType)
        {
            this.missionType = missionType;
            Initialise();
        }

        public void Initialise(int rewardAmount, string rewardType)
        {
            this.rewardAmount = rewardAmount;
            this.rewardType = rewardType;
            Initialise();
        }

        public void Initialise(int goal, int progress, int rewardAmount, string rewardType, bool isClaimed)
        {
            this.goal = goal;
            this.progress = progress;
            this.rewardAmount = rewardAmount;
            this.rewardType = rewardType;
            this.isClaimed = isClaimed;
        }
    }
}