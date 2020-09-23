using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ScoreUI : MonoBehaviour {

	[SerializeField]
	Text scoreAmount;

	private void OnEnable()
	{
		StartCoroutine(AnimateText());
	}
	// Animate the score text 
	IEnumerator AnimateText()
	{
		int score = 0;
		scoreAmount.text = "0";
		while (score < PlayerStats.Score)
		{
			score += 30;
			scoreAmount.text = score.ToString();
			yield return null;
		}
	}
}
