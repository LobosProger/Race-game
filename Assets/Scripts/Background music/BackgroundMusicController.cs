using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
	[SerializeField] private KeyCode switchNextMusicKeycode = KeyCode.Q;
	[SerializeField] private KeyCode switchPreviousMusicKeycode = KeyCode.E;
    [SerializeField] private AudioClip[] backgroundMusics;
    
	private AudioSource audioSource;

	private int currentIndexOfPlayingMusic = 0;
	private int countOfMusicClips => backgroundMusics.Length;
	private Coroutine currentCoroutineOfWaiting;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		PlayRandomMusicOnStart();
	}

	private void Update()
	{
		if(Input.GetKeyDown(switchNextMusicKeycode)) 
		{
			PlayNextMusic();
		}

		if(Input.GetKeyDown(switchPreviousMusicKeycode))
		{
			PlayPreviousMusic();
		}
	}

	private void PlayRandomMusicOnStart()
	{
		currentIndexOfPlayingMusic = Random.Range(0, countOfMusicClips);
		SelectMusicOnCurrentIndexAndPlay();
	}

	private void PlayNextMusic()
	{
		currentIndexOfPlayingMusic++;

		if (currentIndexOfPlayingMusic >= countOfMusicClips)
		{
			currentIndexOfPlayingMusic = 0;
		}

		SelectMusicOnCurrentIndexAndPlay();	
	}

	private void PlayPreviousMusic()
	{
		currentIndexOfPlayingMusic--;

		if (currentIndexOfPlayingMusic < 0)
		{
			currentIndexOfPlayingMusic = countOfMusicClips - 1;
		}

		SelectMusicOnCurrentIndexAndPlay();
	}

	private void SelectMusicOnCurrentIndexAndPlay()
	{
		audioSource.clip = backgroundMusics[currentIndexOfPlayingMusic];
		audioSource.Play();

		if (currentCoroutineOfWaiting != null)
		{
			StopCoroutine(currentCoroutineOfWaiting);
		}

		currentCoroutineOfWaiting = StartCoroutine(WaitingForSwitchNextMusic());
	}

	private IEnumerator WaitingForSwitchNextMusic()
	{
		yield return new WaitUntil(() => !audioSource.isPlaying);
		PlayNextMusic();
	}
}
