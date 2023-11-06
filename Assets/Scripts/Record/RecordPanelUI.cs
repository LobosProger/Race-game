using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecordPanelUI : MonoBehaviour
{
	[SerializeField] private CanvasGroup leaderboardPanel;
	[SerializeField] private Transform recordsPanel;
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
		leaderboardPanel.DOFade(1f, 0.5f);
	}

	private void UpdateLeaderboardWithRecords()
	{
		ClearLeaderboard();

		NetworkRecord[] allRecordsOfPlayers = FindObjectsOfType<NetworkRecord>();
		allRecordsOfPlayers.OrderBy(record => record.GetAchievedRecord());

		ShowRecordsOfPlayersOnPanel(allRecordsOfPlayers);
	}

	private void ClearLeaderboard()
	{
		foreach(Transform eachRecord in recordsPanel.transform)
		{
			Destroy(eachRecord.gameObject);
		}
	}

	private void ShowRecordsOfPlayersOnPanel(NetworkRecord[] records)
	{
		int orderNumberOfRecordForShowing = 1;
		for (int i = 0; i < records.Length; i++)
		{
			float timeOfCompletionByPlayer = records[i].GetAchievedRecord();
			bool isRecordAchievedByPlayer = records[i].IsRecordAchievedByPlayer();

			if(timeOfCompletionByPlayer != 0)
			{
				RecordUI createdRecord = Instantiate(playerRecordPrefab, recordsPanel.transform);
				if (isRecordAchievedByPlayer)
				{
					createdRecord.ShowPlayerRecord(orderNumberOfRecordForShowing, timeOfCompletionByPlayer);
				}
				else
				{
					createdRecord.ShowRecord(orderNumberOfRecordForShowing, timeOfCompletionByPlayer);
				}

				orderNumberOfRecordForShowing++;
			}
		}
	}
}
