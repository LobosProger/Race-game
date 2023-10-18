using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarLights : MonoBehaviour
{
	[SerializeField] private float timeAnimationOfLights;
	[SerializeField] private Color brakingColorOfLights;
	[SerializeField] private Color goBackwardColorOfLights;
    [SerializeField] private Light[] brakingLights;
	
	private enum StateOfLight { Brake, Idle, Backward }
	private StateOfLight currentStateOfLight = StateOfLight.Idle;
	private Color[] capturedInitialIdleStateColorsOfLights;

	private void Start()
	{
		capturedInitialIdleStateColorsOfLights = new Color[brakingLights.Length];

		for(int i = 0; i < brakingLights.Length; i++)
		{
			capturedInitialIdleStateColorsOfLights[i] = brakingLights[i].color;
		}
	}

	private void OnEnable()
	{
		CarControllerEvents.OnGoForwardHandler += TurnIdleLights;
		CarControllerEvents.OnGoBackwardHandler += TurnGoBackwardLights;
		CarControllerEvents.OnThrottleOffHandler += TurnBrakingLights;
		CarControllerEvents.OnUseHandbrakeHandler += TurnBrakingLights;
	}

	private void OnDisable()
	{
		CarControllerEvents.OnGoForwardHandler -= TurnIdleLights;
		CarControllerEvents.OnGoBackwardHandler -= TurnGoBackwardLights;
		CarControllerEvents.OnThrottleOffHandler -= TurnBrakingLights;
		CarControllerEvents.OnUseHandbrakeHandler -= TurnBrakingLights;
	}

	public void TurnBrakingLights()
	{
		if (currentStateOfLight == StateOfLight.Brake)
			return;

		for(int i = 0; i < brakingLights.Length; i++)
		{
			brakingLights[i].DOColor(brakingColorOfLights, timeAnimationOfLights);
		}

		currentStateOfLight = StateOfLight.Brake;
	}

	public void TurnIdleLights()
	{
		if (currentStateOfLight == StateOfLight.Idle)
			return;

		for (int i = 0; i < brakingLights.Length; i++)
		{
			brakingLights[i].DOColor(capturedInitialIdleStateColorsOfLights[i], timeAnimationOfLights);
		}

		currentStateOfLight = StateOfLight.Idle;
	}

	public void TurnGoBackwardLights()
	{
		if (currentStateOfLight == StateOfLight.Backward)
			return;

		for (int i = 0; i < brakingLights.Length; i++)
		{
			brakingLights[i].DOColor(goBackwardColorOfLights, timeAnimationOfLights);
		}

		currentStateOfLight = StateOfLight.Backward;
	}
}
