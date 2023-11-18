using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceLoopArch : RaceArch
{
	[SerializeField] private int amountLoopsToCompleteRace = 1;
	[SerializeField] private AudioClip soundOfStartRace;
	[SerializeField] private AudioClip soundOfCompleteLoop;

	public static int currentAmountOfCompletedLoops { get; private set; } = 0;
	public static int maxAmountLoopsToComplete { get; private set; }
	private bool isArchWasPassedOnStartOfRace = false;

	private AudioSource audioSource;

	private void OnEnable()
	{
		RaceEvents.OnRestartRaceEvent += RestartState;
	}

	private void OnDisable()
	{
		RaceEvents.OnRestartRaceEvent -= RestartState;
	}

	private void Start()
	{
		maxAmountLoopsToComplete = amountLoopsToCompleteRace;
		audioSource = GetComponent<AudioSource>();
	}

	protected override void OnArchPassByPlayer()
	{
		if(!isArchWasPassedOnStartOfRace)
		{
            isArchWasPassedOnStartOfRace = true;
			RaceEvents.OnStartRace();
			PlaySoundOfStartedRace();
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
					PlaySoundOfCompletedLoop();
				}
			}
        }
	}

	private void PlaySoundOfCompletedLoop()
	{
		audioSource.clip = soundOfCompleteLoop;
		audioSource.Play();
	}

	private void PlaySoundOfStartedRace()
	{
		audioSource.clip = soundOfStartRace;
		audioSource.Play();
	}

	private void RestartState()
	{
		isArchWasPassedOnStartOfRace = false;
		maxAmountLoopsToComplete = amountLoopsToCompleteRace;
		currentAmountOfCompletedLoops = 0;
	}
}
