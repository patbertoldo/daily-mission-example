using DailyMissions;
using System;
using System.Text;
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
            missionSlots[i].Initialise(DailyMissionManager.Instance.DailyMission(i), i);
        }
    }

    private void OnEnable()
    {
        InitialiseSlots();
    }

    private void Update()
    {
        timerText.text = FormatTime(DailyMissionManager.Instance.TimeLeft);
    }

    public string FormatTime(TimeSpan time)
    {
        StringBuilder sb = new StringBuilder();

        // ensure timespan is non-negative
        if (time < TimeSpan.Zero)
        {
            time = TimeSpan.Zero;
        }

        sb.Length = 0; // clear stringbuilder

        int days = time.Days;
        int hours = (int)time.TotalHours;
        int minutes = time.Minutes;
        int seconds = time.Seconds;

        if (hours >= 48)
        {
            string str = days > 1 ? "Days" : "Day";

            sb.Append(days).Append(" ").Append(str);
        }
        else
        {
            sb.Append(string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds));
        }

        return sb.ToString();
    }

    public void DebugResetMissions()
    {
        DailyMissionManager.Instance.DebugAssignNewMissions();
    }
}
