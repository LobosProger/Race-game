using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
	public static RelayManager Singleton;

	private void Awake()
	{
		Singleton = this;
	}

	private async void Start()
	{
		await UnityServices.InitializeAsync();
		await AuthenticationService.Instance.SignInAnonymouslyAsync();
	}

	public async void CreateRelay()
	{
		Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
		string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
		Debug.Log(joinCode);

		RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
		NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
		NetworkManager.Singleton.StartHost();
	}

	public async void JoinRelay(string joinCode)
	{
		JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

		RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
		NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
		NetworkManager.Singleton.StartClient();
	}
}
