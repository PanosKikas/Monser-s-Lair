using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	[SerializeField]
	AnimationCurve curve; 

	Image blackImg; 

	private void Awake()
	{
		blackImg = GetComponentInChildren<Image>();
	}

	private void Start()
	{
		StartCoroutine(FadeIn());
	}
	// This is a transition method and it is not calculated in the metrics
	public void FadeInto(string _scene)
	{
		StartCoroutine(FadeOut(_scene));
	}
	// Slowly fades in 
	IEnumerator FadeIn()
	{
		float time = 1f;
		while (time > 0)
		{
			time -= Time.deltaTime;
			float a = curve.Evaluate(time);
			blackImg.color = new Color(0, 0, 0, a);
			yield return 0;
		}
	}
	// Slowly fades out and loads scene
	IEnumerator FadeOut(string scene)
	{
		Debug.Log("Scene to load: " + scene);
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			float a = curve.Evaluate(time);
			blackImg.color = new Color(0, 0, 0, a);
			yield return 0;
		}
		Debug.Log("Finished Time to load");
		SceneManager.LoadScene(scene);
	}
}
