using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaceArch : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			OnArchPassByPlayer();
        }
	}

	protected abstract void OnArchPassByPlayer();
}
