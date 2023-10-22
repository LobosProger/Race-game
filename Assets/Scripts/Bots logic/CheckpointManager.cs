using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Collider currentAchievingCheckpointCollider;
    [SerializeField] private int currentAchievingCheckpointIndex = 0;
	[SerializeField] private float timerForResetting = 20f;

	private BotControllerAgent agent;
	private HashSet<Collider> passedWrongCheckpoints = new HashSet<Collider>();

	private void Start()
	{
		currentAchievingCheckpointCollider = Checkpoints.Instance.GetColliderCheckpointOnIndex(currentAchievingCheckpointIndex);
		agent = GetComponent<BotControllerAgent>();

		Invoke(nameof(ResetCheckpointsAndAgent), timerForResetting);
	}

	public void OnPassingCheckpoint(Collider checkpointArchCollider)
	{
		if(currentAchievingCheckpointCollider == checkpointArchCollider)
		{
			Debug.Log("Chekpoint!");
			passedWrongCheckpoints.Clear();

			if (Checkpoints.Instance.IsLastCheckpointByIndex(currentAchievingCheckpointIndex))
			{
				currentAchievingCheckpointIndex = 0;
				SetCheckpointByCurrentIndex();

				float addingReward = 0.5f;
				agent.AddReward(addingReward);

			} else
			{
				SetNextCheckpoint();

				float addingReward = 0.5f / Checkpoints.Instance.amountOfCheckpoints;
				agent.AddReward(addingReward);
			}
			
			CancelInvoke(nameof(ResetCheckpointsAndAgent));
			Invoke(nameof(ResetCheckpointsAndAgent), timerForResetting);
		} else
		{
			if(!IsCheckpointWasAlreadyPassedAsWrong(checkpointArchCollider))
			{
				Debug.Log("Wrong Chekpoint!");
				agent.AddReward(-0.05f);
				passedWrongCheckpoints.Add(checkpointArchCollider);
			} else
			{
				Debug.Log("This Chekpoint was already passed as wrong!");
			}
			
		}
	}

	private bool IsCheckpointWasAlreadyPassedAsWrong(Collider passingWrongCheckpoint) => passedWrongCheckpoints.Contains(passingWrongCheckpoint);

	private void SetNextCheckpoint()
	{
		currentAchievingCheckpointIndex++;
		SetCheckpointByCurrentIndex();
	}

	private void SetCheckpointByCurrentIndex()
	{
		currentAchievingCheckpointCollider = Checkpoints.Instance.GetColliderCheckpointOnIndex(currentAchievingCheckpointIndex);
	}

	public Collider GetCurrentCheckpointCollider()
	{
		return currentAchievingCheckpointCollider;
	}

	private void ResetCheckpointsAndAgent()
	{
		currentAchievingCheckpointIndex = 0;
		SetCheckpointByCurrentIndex();
		Invoke(nameof(ResetCheckpointsAndAgent), timerForResetting);

		agent.AddReward(-1f);
		agent.EndEpisode();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Checkpoint"))
		{
			OnPassingCheckpoint(other);
		}
	}
}
