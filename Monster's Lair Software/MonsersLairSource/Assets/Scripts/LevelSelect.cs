using UnityEngine.UI;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

	[SerializeField]
	private Button[] levels; // An array holding all the buttons - levels on the LevelSelect scene
	// Disable all the levels that the player hasn't reached yet
	void Start () {
		int levelCleared = PlayerPrefs.GetInt("levelCleared", 0);
		for (int i = 0; i < levels.Length; i++)
		{
			if (levelCleared < i)
			{
				levels[i].interactable = false;
			}
		}
	}
}
