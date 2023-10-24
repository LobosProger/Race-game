using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoints : MonoBehaviour
{
	[SerializeField] private List<Transform> spawnpoints = new List<Transform>();
	public static Spawnpoints instance;

	private int seedForRandom = 0;
	private const int maxSeedForRandom = 10240;

	private void Awake()
	{
		instance = this;
	}

	public Transform GetRandomSpawnpoint()
	{
		seedForRandom++;
		if(seedForRandom >= maxSeedForRandom)
			seedForRandom = 0;

		return spawnpoints[Random.Range(0, spawnpoints.Count)];
	}
}
