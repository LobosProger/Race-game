using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class RaceStartTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text startRaceTimerText;

    public static RaceStartTimerUI Singleton;

	private void Awake()
	{
		Singleton = this;
	}

	private void OnEnable()
	{
		RaceEvents.OnStartRaceEvent += ShowStartRaceTimer;
	}

	private void OnDisable()
	{
		RaceEvents.OnStartRaceEvent -= ShowStartRaceTimer;
	}

	public void ShowStartRaceTimer()
	{
		ShowTextOfTimer("3");
		ChangeAlphaOfText(1f);

		var sequence = DOTween.Sequence();
		sequence.Append(startRaceTimerText.DOFade(0f, 0.5f).SetDelay(0.2f).OnComplete(() => { ShowTextOfTimer("2"); ChangeAlphaOfText(1f); }));
		sequence.Append(startRaceTimerText.DOFade(0f, 0.5f).SetDelay(0.2f).OnComplete(() => { ShowTextOfTimer("1"); ChangeAlphaOfText(1f); }));
		sequence.Append(startRaceTimerText.DOFade(0f, 0.5f).SetDelay(0.2f).OnComplete(() => { ShowTextOfTimer("Start!"); ChangeAlphaOfText(1f); }));
		sequence.Append(startRaceTimerText.DOFade(0f, 0.5f).SetDelay(0.2f));
	}

	private void ShowTextOfTimer(string text)
	{
		startRaceTimerText.text = text;
	}

	private void ChangeAlphaOfText(float alpha)
	{
		startRaceTimerText.alpha = alpha;
	}
}
