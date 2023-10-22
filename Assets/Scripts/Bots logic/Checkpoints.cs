using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private List<Collider> listOfCheckpoints = new List<Collider>();

	public int amountOfCheckpoints => listOfCheckpoints.Count;
	public static Checkpoints Instance;

	private void Awake()
	{
		listOfCheckpoints = GetComponentsInChildren<Collider>().ToList();
		Instance = this;
	}

	public Collider GetColliderCheckpointOnIndex(int index) => listOfCheckpoints[index];

	public bool IsLastCheckpointByIndex(int index) => index == listOfCheckpoints.Count - 1;
}
