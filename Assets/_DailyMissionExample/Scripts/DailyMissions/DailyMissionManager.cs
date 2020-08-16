using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DailyMissions
{
    public class DailyMissionManager : MonoBehaviour
    {
        private static DailyMissionManager instance;
        public static DailyMissionManager Instance { get { return instance; } }

        [SerializeField, Header("Missions")]
        private DailyMission[] easyMissions;
        [SerializeField]
        private DailyMission[] mediumMissions;
        [SerializeField]
        private DailyMission[] hardMissions;

        private DailyMission[] currentMissions;
        private string[] currentMissionsPrefs;
        private const string currentMissionsPrefsKey = "DailyMissions/Missions{0}";

        private DateTime nextResetTime;
        public DateTime NextResetTime
        {
            get
            {
                return nextResetTime;
            }
            set
            {
                nextResetTime = value;
                SavePref(nextResetTimeKey, nextResetTime.ToString());
            }
        }
        private const string nextResetTimeKey = "DailyMissions/NextResetTime";

        public TimeSpan TimeLeft {  get { return NextResetTime - DateTime.Now; } }

        [SerializeField, Header("Rewards")]
        private int[] easyRewardRange;
        [SerializeField]
        private int[] mediumRewardRange;
        [SerializeField]
        private int[] hardRewardRange;

        public Action OnDailyMissionsAssigned;
        public Action OnDailyMissionsProgress;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }

            currentMissions = new DailyMission[3];
            currentMissionsPrefs = new string[3];

            for (int i = 0; i < currentMissionsPrefs.Length; i++)
            {
                currentMissionsPrefs[i] = string.Format(currentMissionsPrefsKey, i);
            }

            InitializeNextResetTime();
        }

        private void Start()
        {
            // Assign new missions if the reset time has elapsed or if there are none loaded.
            if (TimeLeft.TotalSeconds <= 0 || currentMissions[0] == null)
            {
                AssignNewDailyMissions();
            }
        }

        /// <summary>
        /// Each reset is 9.00 AM, if it's never been set before, we need to set it to the next time this time occurs.
        /// </summary>
        private void InitializeNextResetTime()
        {
            DateTime defaultNextResetTime = DateTime.Today.AddHours(9);
            NextResetTime = DateTime.Now > defaultNextResetTime ? defaultNextResetTime.AddDays(1) : defaultNextResetTime;
        }

        private void AssignNewDailyMissions()
        {
            if (TimeLeft.TotalSeconds <= 0)
                NextResetTime = DateTime.Now.AddDays(1).AddHours(9);

            for (int i = 0; i < Enum.GetNames(typeof(Difficulties)).Length; i++)
            {

            }
        }

        /// <summary>
        /// Gets a new daily mission where the returned mission can't be the same type as another.
        /// </summary>
        private DailyMission NewDailyMission(Difficulties difficulty)
        {
            DailyMission newDailyMission;
            DailyMission[] selectedMissions;

            switch(difficulty)
            {
                case Difficulties.Easy:
                    selectedMissions = easyMissions.Where(x => x.OfferRequirementsComplete && !MatchesWithOtherNewMission(x)).ToArray();
                    newDailyMission = Instantiate(selectedMissions[UnityEngine.Random.Range(0, selectedMissions.Length)]);
                    newDailyMission.Initialise(UnityEngine.Random.Range(easyRewardRange[0], easyRewardRange[1]), "Coins");
                    break;
                case Difficulties.Medium:
                    selectedMissions = mediumMissions.Where(x => x.OfferRequirementsComplete && !MatchesWithOtherNewMission(x)).ToArray();
                    newDailyMission = Instantiate(selectedMissions[UnityEngine.Random.Range(0, selectedMissions.Length)]);
                    newDailyMission.Initialise(UnityEngine.Random.Range(mediumRewardRange[0], mediumRewardRange[1]), "Coins");
                    break;
                case Difficulties.Hard:
                    selectedMissions = hardMissions.Where(x => x.OfferRequirementsComplete && !MatchesWithOtherNewMission(x)).ToArray();
                    newDailyMission = Instantiate(selectedMissions[UnityEngine.Random.Range(0, selectedMissions.Length)]);
                    newDailyMission.Initialise(UnityEngine.Random.Range(hardRewardRange[0], hardRewardRange[1]), "Coins");
                    break;
                default:
                    Debug.LogError("[DailyMissions] Should never create an instance like this. No missions from the lists was found.");
                    newDailyMission = ScriptableObject.CreateInstance<DailyMission>();
                    break;
            }

            Debug.Log($"[DailyMissions] Assigned: {newDailyMission.name}");

            return newDailyMission;
        }

        private bool MatchesWithOtherNewMission(DailyMission dailyMission)
        {
            if (dailyMission == null)
                return false;

            foreach (var currentMission in currentMissions)
            {
                if (currentMission == null)
                    continue;

                if (dailyMission.MissionType == currentMission.MissionType || (dailyMission.GroupID > 0 && dailyMission.GroupID == currentMission.GroupID))
                    return true;
            }

            return false;
        }

        private void SavePref(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
    }
}