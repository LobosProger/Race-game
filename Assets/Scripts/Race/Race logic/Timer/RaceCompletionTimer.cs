using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCompletionTimer : MonoBehaviour
{
	private float timeOfStartingCompletingLoopOfRace;
	private float timeOfStartingCompletingLastCheckpointOfRace;
	private float timeOfStartingCompletingRace;

	public static float TimeOfCompletingRace {  get; private set; }

	private bool isRaceCompleting;

	private void OnEnable()
	{
		RaceEvents.OnStartRaceEvent += OnStartRace;
		RaceEvents.OnCompleteLoopEvent += OnCompleteLoopOfRace;
		RaceEvents.OnCompleteCheckpointEvent += OnPassCheckpoint;
		RaceEvents.OnCompleteRaceEvent += OnCompleteRace;
		RaceEvents.OnRestartRaceEvent += RestartState;
	}

	private void OnDisable()
	{
		RaceEvents.OnStartRaceEvent -= OnStartRace;
		RaceEvents.OnCompleteLoopEvent -= OnCompleteLoopOfRace;
		RaceEvents.OnCompleteCheckpointEvent -= OnPassCheckpoint;
		RaceEvents.OnCompleteRaceEvent -= OnCompleteRace;
		RaceEvents.OnRestartRaceEvent -= RestartState;
	}

	private void Update()
	{
		if(isRaceCompleting)
		{
			TimeOfCompletingRace = Time.time - timeOfStartingCompletingRace;
			RaceTimerUI.Instance.ShowTimeOfCompletingRace(TimeOfCompletingRace);
		}
	}

	private void OnStartRace()
	{
		isRaceCompleting = true;
		timeOfStartingCompletingLoopOfRace = Time.time;
		timeOfStartingCompletingLastCheckpointOfRace = Time.time;
		timeOfStartingCompletingRace = Time.time;
	}

	private void OnCompleteLoopOfRace()
	{
		float timeOfCompletionLastLoop = Time.time - timeOfStartingCompletingLoopOfRace;
		RaceTimerUI.Instance.ShowLoopCompletionTime(timeOfCompletionLastLoop);

		timeOfStartingCompletingLoopOfRace = Time.time;
		timeOfStartingCompletingLastCheckpointOfRace = Time.time;
	}

	private void OnPassCheckpoint()
	{
		float timeOfCompletionLastCheckpoint = Time.time - timeOfStartingCompletingLastCheckpointOfRace;
		RaceTimerUI.Instance.ShowCheckpointCompletionTime(timeOfCompletionLastCheckpoint);
		
		timeOfStartingCompletingLastCheckpointOfRace = Time.time;
	}

	private void OnCompleteRace()
	{
		isRaceCompleting = false;
	}

	private void RestartState()
	{
		isRaceCompleting = false;
		RaceTimerUI.Instance.ShowTimeOfCompletingRace(0);
	}
}
