using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecordPanelUI : MonoBehaviour
{
	[SerializeField] private CanvasGroup panelOfRecords;
    [SerializeField] private RecordUI playerRecord;

	private void OnEnable()
	{
		RaceEvents.OnCompleteRaceEvent += ShowRecordsOnLeaderboard;
	}

	private void OnDisable()
	{
		RaceEvents.OnCompleteRaceEvent -= ShowRecordsOnLeaderboard;
	}

	private void ShowRecordsOnLeaderboard()
	{
		panelOfRecords.DOFade(1f, 0.5f);
		float timeOfCompletionByPlayer = RaceCompletionTimer.TimeOfCompletingRace;
		playerRecord.ShowRecord(timeOfCompletionByPlayer);
	}
}
