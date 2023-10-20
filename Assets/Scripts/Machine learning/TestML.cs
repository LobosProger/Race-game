using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class TestML : Agent
{
	[SerializeField] private bool training = true;
	[SerializeField] private int a;
	[SerializeField] private int b;

	private void Update()
	{
		Debug.Log(Random.Range(0, 2));
	}
}
