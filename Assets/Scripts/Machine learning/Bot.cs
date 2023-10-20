using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Bot : Agent
{
	[SerializeField] private Transform target;

	private Vector3 capturedPositionInStart;

	private void Awake()
	{
		capturedPositionInStart = transform.position;
	}

	public override void OnEpisodeBegin()
	{
		transform.position = capturedPositionInStart;
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		Debug.Log("Adding to observing");
		sensor.AddObservation(transform.position);
		sensor.AddObservation(target);
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		float moveX = actions.ContinuousActions[0];
		float moveZ = actions.ContinuousActions[1];

		float speed = 3f;
		transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * speed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Wall"))
		{
			SetReward(-1f);
			EndEpisode();
		}

		if (other.CompareTag("Target"))
		{
			Debug.Log("Achieved to target");
			SetReward(+1f);
			EndEpisode();
		}
	}
}
