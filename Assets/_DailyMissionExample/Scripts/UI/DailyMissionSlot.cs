using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DailyMissions;

public class DailyMissionSlot : MonoBehaviour
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text descriptionText;

    [SerializeField, Space()]
    private Image icon;

    [SerializeField, Space()]
    private Text rewardText;

    [SerializeField, Space()]
    private Text progressText;
    [SerializeField]
    private RectTransform progressBarParent;
    [SerializeField]
    private RectTransform progressBar;
    private float progressBarMinFill = 60;

    [SerializeField]
    private Button claimButton;

    private int slotIndex = 0;

    public void Initialise(DailyMission dailyMission, int slotIndex)
    {
        this.slotIndex = slotIndex;

        titleText.text = $"Mission {slotIndex}";
        descriptionText.text = dailyMission.Description;
        icon.sprite = dailyMission.Icon;

        progressText.text = string.Format("{0}/{1}", dailyMission.Progress, dailyMission.Goal);
        progressBar.sizeDelta = new Vector2(); // Make a range converter.

        claimButton.interactable = dailyMission.IsComplete;
    }

    /// <summary>
    /// UI Event.
    /// </summary>
    public void Claim()
    {

    }
}
