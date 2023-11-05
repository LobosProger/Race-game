using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class RecordUI : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private TMP_Text recordText;

	public void ShowRecord(float timeOfCompletion)
    {
		TimeSpan timeSpan = TimeSpan.FromSeconds(timeOfCompletion);
        recordText.text = timeSpan.ToString("mm':'ss':'ff");

        DOTween.Kill(recordText);
        var sequence = DOTween.Sequence(recordText);
        sequence.Append(recordText.DOColor(Color.yellow, 0.3f)).Append(recordText.DOColor(Color.white, 0.3f)).SetLoops(-1);
    }
}
