using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class RelayManagerUI : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button connectToHostButton;
	[SerializeField] private TMP_InputField joinCodeField;

	private void Awake()
	{
		startHostButton.onClick.AddListener(() => RelayManager.Singleton.CreateRelay());
		connectToHostButton.onClick.AddListener(() => RelayManager.Singleton.JoinRelay(joinCodeField.text));
	}
}
