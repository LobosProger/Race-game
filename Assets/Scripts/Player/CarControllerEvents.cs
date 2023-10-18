using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerEvents : MonoBehaviour
{
    public delegate void CarEventHandler();
    
    public static event CarEventHandler OnGoForwardHandler;
    public static event CarEventHandler OnGoBackwardHandler;
    public static event CarEventHandler OnThrottleOffHandler;
    public static event CarEventHandler OnUseHandbrakeHandler;

    public static void OnGoForward()
    {
        if(OnGoForwardHandler != null)
        {
            OnGoForwardHandler();
        }
    }

    public static void OnGoBackward()
    {
        if(OnGoBackwardHandler != null)
        {
            OnGoBackwardHandler();
        }
    }

	public static void OnThrottleOff()
	{
		if (OnThrottleOffHandler != null)
		{
			OnThrottleOffHandler();
		}
	}

	public static void OnUseHandbrake()
    {
        if(OnUseHandbrakeHandler != null)
        {
            OnUseHandbrakeHandler();
        }
    }
}
