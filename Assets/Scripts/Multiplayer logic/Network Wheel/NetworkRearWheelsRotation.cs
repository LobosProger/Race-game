using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkRearWheelsRotation : NetworkBehaviour
{
	[SerializeField] private List<Transform> animatableWheels = new List<Transform>();

	private NetworkVariable<sbyte> rotationAngleX = new NetworkVariable<sbyte>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

	private void Update()
	{
		if (IsOwner)
		{
			AssignNetworkValuesOfWheels();
		}
		else
		{
			AnimateWheels();
		}
	}

	private void AssignNetworkValuesOfWheels()
	{
		// Just taking first wheel, because anyway all wheels are repeating movings
		rotationAngleX.Value = (sbyte)(animatableWheels[0].transform.localEulerAngles.x > 180 ? animatableWheels[0].transform.localEulerAngles.x - 360 : animatableWheels[0].transform.localEulerAngles.x);
	}

	private void AnimateWheels()
	{
		float unboxedRotationAngleX = rotationAngleX.Value;

		foreach (var everyWheelTransform in animatableWheels)
		{
			everyWheelTransform.transform.localEulerAngles = new Vector3(unboxedRotationAngleX, everyWheelTransform.localEulerAngles.y, everyWheelTransform.localEulerAngles.z);
		}
	}
}
