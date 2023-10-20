using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SumML : Agent
{
	[SerializeField] private bool training = true;
	[SerializeField] private int valueA;
	[SerializeField] private int valueB;

	private int forTrainingValueA;
	private int forTrainingValueB;
	private int forTrainingResult;
	private float amountOfErrors = 0;

	public override void OnEpisodeBegin()
	{
		GenerateValues();
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		if(training)
		{
			//Debug.Log("Insert values for observation...");
			//Debug.Log("A: " + forTrainingValueA + " B: " + forTrainingValueB);
			sensor.AddObservation(forTrainingValueA);
			sensor.AddObservation(forTrainingValueB);
		} else
		{
			sensor.AddObservation(valueA);
			sensor.AddObservation(valueB);
		}
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		//Debug.Log("Get result!");
		//Debug.Log("A: " + forTrainingValueA + " B: " + forTrainingValueB);
		int returnedResult = actions.DiscreteActions[0];
		if (training)
		{
			if(returnedResult == forTrainingResult)
			{
				AddReward((1 + (amountOfErrors / 10)));
				amountOfErrors = 0;
			}
            else
            {
				AddReward(-1f);
				amountOfErrors++;
			}
			EndEpisode();
        } else
		{
			Debug.Log("Sum A and B is:" + returnedResult);
			EndEpisode();
		}
	}

	private void GenerateValues()
	{
		SetAnotherSeedOfRandom();
		forTrainingValueA = Random.Range(0, 6);
		SetAnotherSeedOfRandom();
		forTrainingValueB = Random.Range(0, 6);

		forTrainingResult = forTrainingValueA + forTrainingValueB;
		//Debug.Log("Generate values");
		//Debug.Log("A: " + forTrainingValueA + " B: " + forTrainingValueB);
	}

	private int seed = 0;
	private void SetAnotherSeedOfRandom()
	{
		seed++;
		Random.InitState(seed);

		if(seed >= 12255)
		{
			seed = 0;
		}
	}
}
