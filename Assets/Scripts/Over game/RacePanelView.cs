using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacePanelView : MonoBehaviour
{
    [SerializeField] private Button restartRaceButton;
    [SerializeField] private Button quitFromRaceButton;

    public event Action OnRestartButtonClicked;
    public event Action OnQuitButtonClicked;

	private void Start()
	{
		restartRaceButton.onClick.AddListener(OnButtonRestartClick);
		quitFromRaceButton.onClick.AddListener(OnQuitButtonClick);
	}

	private void OnButtonRestartClick()
	{
		OnRestartButtonClicked?.Invoke();
	}

	private void OnQuitButtonClick()
	{
		OnQuitButtonClicked?.Invoke();
	}
}
