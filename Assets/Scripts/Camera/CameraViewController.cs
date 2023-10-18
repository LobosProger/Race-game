using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraViewController : MonoBehaviour
{
	[SerializeField] private KeyCode changeViewKeycode = KeyCode.R;
	
	[SerializeField] private Vector3 goForwardCamerafollowOffset;
	[SerializeField] private Vector3 goBackwardCamerafollowOffset;
	[SerializeField] private Vector3 firstPersonViewOffset;
	[SerializeField] private Vector3 airPersonViewOffset;

    private enum View {ThirdPersonView, FirstPersonView, AirPersonView}
	private View view = View.ThirdPersonView;

	private enum Animatable3rdViewState {FrontView, BackwardView}
	private Animatable3rdViewState view3rdState = Animatable3rdViewState.FrontView;

	private CinemachineVirtualCamera virtualCamera;
	private CinemachineTransposer cinemachineTransposer;
	private CinemachineComposer cinemachineComposer;
	
	private Vector3 initialRotationOffsetFromThirdPersonView;
	private const float dampingFollowingIn3rdView = 0.1f;
	private const float dampingFollowingIn1stView = 0.03f;
	private const float dampingFollowingInAirView = 0.1f;

	private void Start()
	{
		virtualCamera = GetComponent<CinemachineVirtualCamera>();
		cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
		cinemachineComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
		initialRotationOffsetFromThirdPersonView = cinemachineComposer.m_TrackedObjectOffset;
	}

	private void OnEnable()
	{
		CarControllerEvents.OnGoForwardHandler += OnChangeViewToGoForward;
		CarControllerEvents.OnGoBackwardHandler += OnChangeViewToGoBackward;
	}

	private void OnDisable()
	{
		CarControllerEvents.OnGoForwardHandler -= OnChangeViewToGoForward;
		CarControllerEvents.OnGoBackwardHandler -= OnChangeViewToGoBackward;
	}

	private void Update()
	{
		if(Input.GetKeyDown(changeViewKeycode))
		{
			switch(view)
			{
				case View.ThirdPersonView:
					view = View.FirstPersonView;
					DOTween.Kill(cinemachineTransposer.m_FollowOffset);
					cinemachineTransposer.m_FollowOffset = firstPersonViewOffset;
					cinemachineTransposer.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;

					cinemachineTransposer.m_XDamping = dampingFollowingIn1stView;
					cinemachineTransposer.m_YDamping = dampingFollowingIn1stView;
					cinemachineTransposer.m_ZDamping = dampingFollowingIn1stView;

					cinemachineComposer.m_TrackedObjectOffset = new Vector3(0, 0, 90);
					break;

				case View.FirstPersonView:
					view = View.AirPersonView;
					cinemachineTransposer.m_FollowOffset = airPersonViewOffset;
					cinemachineTransposer.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;

					cinemachineTransposer.m_XDamping = dampingFollowingInAirView;
					cinemachineTransposer.m_YDamping = dampingFollowingInAirView;
					cinemachineTransposer.m_ZDamping = dampingFollowingInAirView;

					cinemachineComposer.m_TrackedObjectOffset = initialRotationOffsetFromThirdPersonView;

					break;

				case View.AirPersonView:
					view = View.ThirdPersonView;
					cinemachineTransposer.m_FollowOffset = goForwardCamerafollowOffset;
					cinemachineTransposer.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;

					cinemachineTransposer.m_XDamping = dampingFollowingIn3rdView;
					cinemachineTransposer.m_YDamping = dampingFollowingIn3rdView;
					cinemachineTransposer.m_ZDamping = dampingFollowingIn3rdView;

					cinemachineComposer.m_TrackedObjectOffset = initialRotationOffsetFromThirdPersonView;
					break;
			}
		}
	}

	private void OnChangeViewToGoForward()
    {
		if (view != View.ThirdPersonView)
			return;

		if(view3rdState != Animatable3rdViewState.FrontView)
		{
			view3rdState = Animatable3rdViewState.FrontView;
			DOTween.To(() => cinemachineTransposer.m_FollowOffset, x => cinemachineTransposer.m_FollowOffset = x, goForwardCamerafollowOffset, 2f).SetEase(Ease.InQuad).SetDelay(0.5f);
		}
	}

	private void OnChangeViewToGoBackward()
	{
		if (view != View.ThirdPersonView)
			return;

		if (view3rdState != Animatable3rdViewState.BackwardView)
		{
			view3rdState = Animatable3rdViewState.BackwardView;
			DOTween.To(() => cinemachineTransposer.m_FollowOffset, x => cinemachineTransposer.m_FollowOffset = x, goBackwardCamerafollowOffset, 2f).SetEase(Ease.InQuad).SetDelay(0.5f);
		}
	}
}
