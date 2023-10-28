using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button connectToHostButton;

	private void Awake()
	{
		startHostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
		connectToHostButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
	}
}
