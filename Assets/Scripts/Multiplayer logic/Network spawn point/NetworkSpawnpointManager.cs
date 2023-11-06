using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class NetworkSpawnpointManager : NetworkBehaviour
{
	public override void OnNetworkSpawn()
	{
		Transform spawnpoint = NetworkSpawnpoint.GetSpawnpoint();
		GetComponent<NetworkTransform>().Teleport(spawnpoint.transform.position, transform.rotation, transform.localScale);
	}
}
