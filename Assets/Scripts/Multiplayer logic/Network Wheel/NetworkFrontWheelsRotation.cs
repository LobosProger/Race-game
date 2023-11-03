using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkFrontWheelsRotation : NetworkBehaviour
{
	[SerializeField] private List<Transform> animatableWheels = new List<Transform>();

    [SerializeField] private NetworkVariable<sbyte> rotationAngleY = new NetworkVariable<sbyte>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private NetworkVariable<sbyte> rotationAngleX = new NetworkVariable<sbyte>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

	private void Update()
	{
		if (IsOwner)
		{
			AssignNetworkValuesOfWheels();
		} else
		{
			AnimateWheels();
		}
	}

	private void AssignNetworkValuesOfWheels()
	{
		// Just taking first wheel, because anyway all wheels are repeating movings
		float rotationTurnValue = GetNormalizedTurnRotationOfWheel(animatableWheels[0].transform.localEulerAngles);
		float rotationMoveValue = GetNormalizedMovingRotationOfWheel(animatableWheels[0].transform.localEulerAngles);

		rotationAngleY.Value = (sbyte)rotationTurnValue;
		rotationAngleX.Value = (sbyte)rotationMoveValue;
	}

	private void AnimateWheels()
	{
		float unboxedRotationAngleY = rotationAngleY.Value;
		float unboxedRotationAngleX = rotationAngleX.Value;

		foreach (var everyWheelTransform in animatableWheels)
		{
			everyWheelTransform.transform.localEulerAngles = new Vector3(unboxedRotationAngleX, unboxedRotationAngleY, everyWheelTransform.localEulerAngles.z);
		}
	}

	// This code resolves problem with incorrect getting values of eulers as they represented in inspector window.
	// I found sequence that on the turn value (rotation axis Y) also affected axis Z and eulers are always
	// represented in the range of 0 to 360, so below code resolves this problem
	private float GetNormalizedTurnRotationOfWheel(Vector3 eulerAngles)
	{
		float rotationValue = animatableWheels[0].transform.localEulerAngles.y;
		if (animatableWheels[0].transform.localEulerAngles.z > 179)
		{
			rotationValue -= 180;
		}
		else if (animatableWheels[0].transform.localEulerAngles.y > 179)
		{
			rotationValue -= 360;
		}

		return rotationValue;
	}

	private float GetNormalizedMovingRotationOfWheel(Vector3 eulerAngles)
	{
		float rotationValue = animatableWheels[0].transform.localEulerAngles.x;
		if (animatableWheels[0].transform.localEulerAngles.x > 179)
		{
			rotationValue -= 360;
		}

		return rotationValue;
	}
}
