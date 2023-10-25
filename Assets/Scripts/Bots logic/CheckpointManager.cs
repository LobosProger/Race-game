using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Collider currentAchievingCheckpointCollider;
    [SerializeField] private int currentAchievingCheckpointIndex = 0;
	[SerializeField] private int amountCheckpointToCompleteForTraining = 0;
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
			passedWrongCheckpoints.Clear();

			if (Checkpoints.Instance.IsLastCheckpointByIndex(currentAchievingCheckpointIndex))
			{
				currentAchievingCheckpointIndex = 0;
				SetCheckpointByCurrentIndex();

				agent.AddReward(5f);
				agent.EndEpisode();

			} else
			{
				SetNextCheckpoint();
				//Debug.Log("Checkpoint completed!");
				agent.AddReward(1f);

				if(amountCheckpointToCompleteForTraining != 0 && currentAchievingCheckpointIndex == amountCheckpointToCompleteForTraining)
				{
					currentAchievingCheckpointIndex = 0;
					SetCheckpointByCurrentIndex();
					agent.EndEpisode();
				}
			}
			
			CancelInvoke(nameof(ResetCheckpointsAndAgent));
			Invoke(nameof(ResetCheckpointsAndAgent), timerForResetting);

		} else
		{
			// If agent already passed wrong checkpoint twice, we just doing nothing,
			// because by this approach we giving to agent a chance to resolve its error

			if(!IsCheckpointWasAlreadyPassedAsWrong(checkpointArchCollider))
			{
				agent.AddReward(-1f);
				//Debug.Log("Checkpoint already passed!");
				passedWrongCheckpoints.Add(checkpointArchCollider);
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

		//agent.AddReward(-6f);
		//Debug.Log("No actions, resetting!");
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
