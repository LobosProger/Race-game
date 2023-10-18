using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceLoopArch : RaceArch
{
	[SerializeField] private int amountLoopsToCompleteRace = 1;

	public static int currentAmountOfCompletedLoops { get; private set; } = 0;
	public static int maxAmountLoopsToComplete { get; private set; }

	private bool isArchWasPassedOnStartOfRace = false;

	private void Start()
	{
		maxAmountLoopsToComplete = amountLoopsToCompleteRace;
	}

	protected override void OnArchPassByPlayer()
	{
		if(!isArchWasPassedOnStartOfRace)
		{
            isArchWasPassedOnStartOfRace = true;
			RaceEvents.OnStartRace();
		}
		else
		{
			if(RaceCheckpointArch.AllCheckpointsCompleted())
			{
				currentAmountOfCompletedLoops++;
				if (currentAmountOfCompletedLoops == amountLoopsToCompleteRace)
				{
					RaceEvents.OnCompleteRace();
				} else
				{
					RaceEvents.OnCompleteLoop();
				}
			}
        }
	}
}
