using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class RaceTimerUI : MonoBehaviour
{
	[SerializeField] private CanvasGroup panelOfChekpointTimer;
	[SerializeField] private TMP_Text checkpointTimerText;
	[Space]
	[SerializeField] private CanvasGroup panelOfLoopTimer;
	[SerializeField] private TMP_Text loopTitleText;
	[SerializeField] private TMP_Text loopTimerText;
	[Space]
	[SerializeField] private TMP_Text completionTimerText;

    public static RaceTimerUI Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void ShowCheckpointCompletionTime(float timeOfCompletionCheckpoint)
    {
		TimeSpan timerFormatter = TimeSpan.FromSeconds(timeOfCompletionCheckpoint);
		checkpointTimerText.text = timerFormatter.ToString("mm':'ss':'ff");

		var animationSequence = DOTween.Sequence();
		animationSequence.Append(panelOfChekpointTimer.DOFade(1f, 0.5f));
		animationSequence.Append(panelOfChekpointTimer.DOFade(0f, 0.5f).SetDelay(1f));
    }

	public void ShowLoopCompletionTime(float timeOfCompletionLoop)
	{
		int currentAmountCompletedLoops = RaceLoopArch.currentAmountOfCompletedLoops;
		int maxAmountOfCompletingLoops = RaceLoopArch.maxAmountLoopsToComplete;
		
		TimeSpan timerFormatter = TimeSpan.FromSeconds(timeOfCompletionLoop);
		loopTimerText.text = timerFormatter.ToString("mm':'ss':'ff");
		loopTitleText.text = $"Loop {currentAmountCompletedLoops}/{maxAmountOfCompletingLoops}!";

		var animationSequence = DOTween.Sequence();
		animationSequence.Append(panelOfLoopTimer.DOFade(1f, 0.5f));
		animationSequence.Append(panelOfLoopTimer.DOFade(0f, 0.5f).SetDelay(1f));
	}

	public void ShowTimeOfCompletingRace(float timeOfCompletion)
	{
		TimeSpan timerFormatter = TimeSpan.FromSeconds(timeOfCompletion);
		completionTimerText.text = timerFormatter.ToString("mm':'ss':'ff");
	}
}
