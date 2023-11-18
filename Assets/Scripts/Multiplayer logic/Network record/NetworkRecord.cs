using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEditor;

public class NetworkRecord : NetworkBehaviour
{
    private NetworkVariable<float> timeOfCompletionRace = new NetworkVariable<float>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

	public override void OnNetworkSpawn()
	{
		if(IsOwner)
		{
			RaceEvents.OnCompleteRaceEvent += CaptureNetworkRecordOfCompletionRace;
			RaceEvents.OnRestartRaceEvent += RestartStateServerRpc;
		}
	}

	private void OnDisable()
	{
		if (IsOwner)
		{
			RaceEvents.OnCompleteRaceEvent -= CaptureNetworkRecordOfCompletionRace;
			RaceEvents.OnRestartRaceEvent -= RestartStateServerRpc;
		}

		timeOfCompletionRace.OnValueChanged -= (prevVal, newVal) => RaceEvents.OnCompleteRaceByAnyNetworkPlayer();
	}

	private void Start()
	{
		timeOfCompletionRace.OnValueChanged += (prevVal, newVal) => RaceEvents.OnCompleteRaceByAnyNetworkPlayer();
	}

	private void CaptureNetworkRecordOfCompletionRace()
	{
		timeOfCompletionRace.Value = RaceCompletionTimer.TimeOfCompletingRace;
	}

	[ServerRpc]
	private void RestartStateServerRpc()
	{
		RestartStateClientRpc();
	}

	[ClientRpc]
	private void RestartStateClientRpc()
	{
		if(IsOwner)
		{
			timeOfCompletionRace.Value = 0;
		}
	}

	public float GetAchievedRecord() => timeOfCompletionRace.Value;

	public bool IsRecordAchievedByPlayer() => IsOwner;
}
