using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsUIManager : MonoBehaviour {
	
	[SerializeField]
	SceneFader sceneFader; // Reference to the sceneFader

	[SerializeField]
	string mainMenuScene = "MainMenu";

	[SerializeField]
	string levelSelectScene = "LevelSelect";

	[SerializeField]
	string nextLevelScene = "Level2"; // Different on each scene
	// Method that is invoked from a button and restarts the current level
	public void Retry()
	{
		sceneFader.FadeInto(SceneManager.GetActiveScene().name);
	}
	// Method that is invoked from a button and returns to the MainMenu Scene
	public void Menu ()
	{
		sceneFader.FadeInto(mainMenuScene);
	}
	// Method that is invoked from a button and returns to the LevelSelect Scene
	public void LevelSelect()
	{
		sceneFader.FadeInto(levelSelectScene);
	}
	// Method that is invoked from a button and transitions the player into the scene of the next level
	public void NextLevel()
	{
		sceneFader.FadeInto(nextLevelScene);
	}
	// Method that is invoked from a button and closes the application
	public void QuitGame()
	{
		Application.Quit();
	}
	// Method that is invoked from a button and loads the scene with the name "Level" +  the parameter levelNo
	public void LoadLevel(int levelNo)
	{
		string levelScene = "Level" + levelNo.ToString();
		sceneFader.FadeInto(levelScene);
	}
}
