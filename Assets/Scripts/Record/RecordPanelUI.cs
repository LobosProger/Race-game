using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecordPanelUI : MonoBehaviour
{
	[SerializeField] private CanvasGroup panelOfRecords;
    [SerializeField] private RecordUI playerRecordPrefab;

	private void OnEnable()
	{
		RaceEvents.OnCompleteRaceEvent += ShowLeaderboard;
		RaceEvents.OnCompleteRaceByAnyNetworkPlayerEvent += UpdateLeaderboardWithRecords;
	}

	private void OnDisable()
	{
		RaceEvents.OnCompleteRaceEvent -= ShowLeaderboard;
		RaceEvents.OnCompleteRaceByAnyNetworkPlayerEvent -= UpdateLeaderboardWithRecords;
	}

	private void ShowLeaderboard()
	{
		panelOfRecords.DOFade(1f, 0.5f);
	}

	private void UpdateLeaderboardWithRecords()
	{
		ClearLeaderboard();

		NetworkRecord[] allRecordsOfPlayers = FindObjectsOfType<NetworkRecord>();
		allRecordsOfPlayers.OrderByDescending(record => record.GetAchievedRecord());

		ShowRecordsOfPlayersOnPanel(allRecordsOfPlayers);
	}

	private void ClearLeaderboard()
	{
		foreach(Transform eachRecord in panelOfRecords.transform)
		{
			Destroy(eachRecord.gameObject);
		}
	}

	private void ShowRecordsOfPlayersOnPanel(NetworkRecord[] records)
	{
		for (int i = 0; i < records.Length; i++)
		{
			float timeOfCompletionByPlayer = records[i].GetAchievedRecord();
			bool isRecordAchievedByPlayer = records[i].IsRecordAchievedByPlayer();
			int orderNumberOfRecordForShowing = i + 1;
			
			RecordUI createdRecord = Instantiate(playerRecordPrefab, panelOfRecords.transform);
			if(isRecordAchievedByPlayer)
			{
				createdRecord.ShowPlayerRecord(orderNumberOfRecordForShowing, timeOfCompletionByPlayer);
			} else
			{
				createdRecord.ShowRecord(orderNumberOfRecordForShowing, timeOfCompletionByPlayer);
			}
		}
	}
}
