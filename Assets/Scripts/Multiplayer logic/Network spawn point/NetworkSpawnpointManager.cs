using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class NetworkSpawnpointManager : NetworkBehaviour
{
	private Transform initialSpawnpoint;
	private Quaternion initialRotationOfPlayerOnSpawnpoint;

	public override void OnNetworkSpawn()
	{
		if (IsOwner)
		{
			CaptureInitialSpawnpoint();
			SpawnPlayerToSpawnpoint();

			RaceEvents.OnRestartRaceEvent += SpawnPlayerToSpawnpoint;
		}
	}

	private void OnDisable()
	{
		if (IsOwner)
		{
			RaceEvents.OnRestartRaceEvent -= SpawnPlayerToSpawnpoint;
		}
	}

	private void CaptureInitialSpawnpoint()
	{
		initialRotationOfPlayerOnSpawnpoint = transform.rotation;
		initialSpawnpoint = NetworkSpawnpoint.GetSpawnpoint();
	}

	private void SpawnPlayerToSpawnpoint()
	{
		if (IsOwner)
		{
			GetComponent<NetworkTransform>().Teleport(initialSpawnpoint.transform.position, initialRotationOfPlayerOnSpawnpoint, transform.localScale);
		}
	}
}
