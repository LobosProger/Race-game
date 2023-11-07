using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkSpawnpoint : MonoBehaviour
{
	private static int spawnpointIndex = 0;
	private static List<Transform> spawnpoints = new List<Transform>();

	private void OnEnable()
	{
		AddSpawnpoint(transform);
	}

	private void OnDisable()
	{
		RemoveSpawnpoint(transform);
	}

	public static void AddSpawnpoint(Transform spawnpoint)
	{
		spawnpoints.Add(spawnpoint);
		spawnpoints = spawnpoints.OrderBy(spawnpoint => spawnpoint.GetSiblingIndex()).ToList();
		spawnpoints.Reverse();
	}

	public static void RemoveSpawnpoint(Transform spawnpoint)
	{
		spawnpoints.Remove(spawnpoint);
		spawnpoints = spawnpoints.OrderBy(spawnpoint => spawnpoint.GetSiblingIndex()).ToList();
		spawnpoints.Reverse();
	}

	public static Transform GetSpawnpoint()
	{
		Transform spawnpoint = spawnpoints[spawnpointIndex % spawnpoints.Count];
		spawnpointIndex++;
		return spawnpoint;
	}
}
