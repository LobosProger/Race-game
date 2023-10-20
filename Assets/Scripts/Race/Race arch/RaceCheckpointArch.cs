using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCheckpointArch : RaceArch
{
	private static int amountOfCheckpointArchs = 0;
	private static int amountOfCompletedCheckpoints = 0;

	private bool isCheckpointArchCompleted;

	private void Start()
	{
		amountOfCheckpointArchs++;
	}

	private void OnEnable()
	{
		RaceEvents.OnCompleteLoopEvent += ResetCompletedCheckpoints;
	}

	private void OnDisable()
	{
		RaceEvents.OnCompleteLoopEvent -= ResetCompletedCheckpoints;
	}

	protected override void OnArchPassByPlayer()
	{
		if(!isCheckpointArchCompleted)
		{
			isCheckpointArchCompleted = true;
			RaceEvents.OnCompleteCheckpoint();
			amountOfCompletedCheckpoints++;
		}
	}

	private void ResetCompletedCheckpoints()
	{
		amountOfCompletedCheckpoints = 0;
		isCheckpointArchCompleted = false;
	}

	public static bool AllCheckpointsCompleted() => amountOfCheckpointArchs == amountOfCompletedCheckpoints;
}