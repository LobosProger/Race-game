using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkRecord : NetworkBehaviour
{
    private NetworkVariable<float> timeOfCompletionRace = new NetworkVariable<float>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

	public override void OnNetworkSpawn()
	{
		if(IsOwner)
		{
			RaceEvents.OnCompleteRaceEvent += CaptureNetworkRecordOfCompletionRace;
		}
	}

	private void OnDisable()
	{
		if (IsOwner)
		{
			RaceEvents.OnCompleteRaceEvent -= CaptureNetworkRecordOfCompletionRace;
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

	public float GetAchievedRecord() => timeOfCompletionRace.Value;

	public bool IsRecordAchievedByPlayer() => IsOwner;
}
