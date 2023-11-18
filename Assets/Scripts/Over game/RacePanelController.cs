using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePanelController : MonoBehaviour
{
    [SerializeField] private RacePanelView racePanelView;

	private void OnEnable()
	{
		racePanelView.OnRestartButtonClicked += RestartRace;
		racePanelView.OnQuitButtonClicked += QuitFromRace;
	}

	private void OnDisable()
	{
		racePanelView.OnRestartButtonClicked -= RestartRace;
		racePanelView.OnQuitButtonClicked -= QuitFromRace;
	}

	private void RestartRace()
	{
		Debug.Log("Restarting race");
		RaceEvents.OnRestartRace();
	}

	private void QuitFromRace()
	{
		Debug.Log("Quit from race");
	}
}
