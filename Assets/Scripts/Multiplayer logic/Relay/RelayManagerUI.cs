using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.UI;

public class RelayManagerUI : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button connectToHostButton;
	[SerializeField] private TMP_InputField joinCodeField;
	[SerializeField] private CanvasGroup panelOfCreatingGame;

	[SerializeField] private TMP_Text joinCodeText;
	[SerializeField] private CanvasGroup panelOfJoinCode;

	private IEnumerator Start()
	{
		startHostButton.onClick.AddListener(() => RelayManager.Singleton.CreateRelay());
		connectToHostButton.onClick.AddListener(() => RelayManager.Singleton.JoinRelay(joinCodeField.text));

		yield return NetworkManager.Singleton != null;

		NetworkManager.Singleton.OnClientConnectedCallback += ShowPanelOfJoinCode;
		NetworkManager.Singleton.OnClientDisconnectCallback += ShowPanelOfCreatingGame;
	}

	private void ShowPanelOfCreatingGame(ulong _)
	{
		// Hide panel with join code if for example, host stopped the game or client disconnected
		panelOfJoinCode.DOFade(0f, 0.5f);

		// Show panel with entering join code and creating the game
		panelOfCreatingGame.blocksRaycasts = true;
		panelOfCreatingGame.DOFade(1f, 0.5f);
	}

	private void ShowPanelOfJoinCode(ulong _)
	{
		// Hide panel with creating game
		panelOfCreatingGame.blocksRaycasts = false;
		panelOfCreatingGame.DOFade(0f, 0.5f);

		// Hide panel with creating game
		panelOfJoinCode.DOFade(1f, 0.5f);
		joinCodeText.text = RelayManager.Singleton.JoinCode;
	}
}
