using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceEvents : MonoBehaviour
{
    public delegate void RaceBaseHandler();

    public static event RaceBaseHandler OnStartRaceEvent;
    public static event RaceBaseHandler OnCompleteLoopEvent;
    public static event RaceBaseHandler OnCompleteCheckpointEvent;
    public static event RaceBaseHandler OnCompleteRaceEvent;

    public static void OnStartRace()
    {
        if(OnStartRaceEvent != null)
        {
            OnStartRaceEvent();
        }
    }

    public static void OnCompleteLoop()
    {
        if(OnCompleteLoopEvent != null)
        {
            OnCompleteLoopEvent();
        }
    }

    public static void OnCompleteCheckpoint()
    {
        if(OnCompleteCheckpointEvent != null)
        {
            OnCompleteCheckpointEvent();
        }
    }

	public static void OnCompleteRace()
	{
		if (OnCompleteRaceEvent != null)
		{
			OnCompleteRaceEvent();
		}
	}
}
