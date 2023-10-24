using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInBackground : MonoBehaviour
{
	private void Start()
	{
		Application.runInBackground = true;
		Debug.Log("Running application in background!");
	}
}
