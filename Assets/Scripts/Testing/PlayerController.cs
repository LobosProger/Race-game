using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
	private void Update()
	{
		if (!IsOwner)
			return;

		Vector3 directionToMove = Vector3.zero;

		if (Input.GetKey(KeyCode.W)) directionToMove.z += 1;
		if (Input.GetKey(KeyCode.S)) directionToMove.z -= 1;
		if (Input.GetKey(KeyCode.A)) directionToMove.x -= 1;
		if (Input.GetKey(KeyCode.D)) directionToMove.x += 1;

		transform.position += directionToMove * 5f * Time.deltaTime;
	}
}
