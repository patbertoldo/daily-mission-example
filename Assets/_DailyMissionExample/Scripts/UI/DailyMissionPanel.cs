using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionPanel : MonoBehaviour
{
    [SerializeField]
    private DailyMissionSlot[] missionSlots;

    [SerializeField]
    private Text timerText;

    private void InitialiseSlots()
    {
        for (int i = 0; i < missionSlots.Length; i++)
        {
            //missionSlots[i].Initialise();
        }
    }

    private void OnEnable()
    {
        InitialiseSlots();
    }

    private void Update()
    {
        //timerText.text = 
    }

    public void DebugResetMissions()
    {

    }
}
