using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance { get; private set; }
	public bool gameIsOver { get; private set; }

	[SerializeField]
	GameObject gameOverUI; // Reference to the levelCleared GUI

	[SerializeField]
	GameObject levelClearedUI; // Reference to the levelCleared GUI

	CameraController cameraController; // Reference to the CameraController

	// Sigletton Pattern
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		gameIsOver = false;
		enabled = true;
		cameraController = Camera.main.GetComponent<CameraController>();
	}

	private void Update()
	{
		if (gameIsOver)
			return;
		
		if (PlayerStats.Lives <= 0)
		{
			GameOver();
		}
	}
	// Function that handles what happens when the player wins the current game
	public void WinLevel()
	{
		gameIsOver = true;
		Debug.Log("You won the level!");
		cameraController.enabled = false;
		int levelCleared = PlayerPrefs.GetInt("levelCleared");
		// if the player cleared for the first time the current level increment and save the new levelCleared 
		if ((levelCleared + 1) < SceneManager.GetActiveScene().buildIndex )
		{
			++levelCleared;
			PlayerPrefs.SetInt("levelCleared", levelCleared);
		}
		levelClearedUI.SetActive(true); // Turn on levelCleared GUI
	}

	void GameOver()
	{
		gameIsOver = true;
		Debug.Log("Game Over!!!");
		cameraController.enabled = false; // Stop the camera from moving
		gameOverUI.SetActive(true); // Turn on GameOver GUI
	}
}
