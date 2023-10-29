using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public abstract class RaceArch : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && other.GetComponent<NetworkObject>().IsOwner)
		{
			OnArchPassByPlayer();
        }
	}

	protected abstract void OnArchPassByPlayer();
}
