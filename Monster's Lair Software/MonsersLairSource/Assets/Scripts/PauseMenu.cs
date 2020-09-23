using UnityEngine;

public class PauseMenu : MonoBehaviour 
{
	[SerializeField]
	GameObject PauseUI; // The GUI of the pause screen
	CameraController cameraMovement; // Reference to the CameraMovement class

	private void Start()
	{
		cameraMovement = Camera.main.GetComponent<CameraController>();
	}

	void Update () 
	{

		if (GameManager.instance.gameIsOver)
			return;
		// if 'Escape' or the 'P' key was pressed toggle the Pause menu
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}
	// Toggles the state of the Pause menu
	public void Toggle()
	{
		if (PauseUI.activeSelf)
		{
			PauseUI.SetActive(false);
			cameraMovement.enabled = true;
			Time.timeScale = 1f; 
		}
		else
		{
			PauseUI.SetActive(true);
			cameraMovement.enabled = false;
			Time.timeScale = 0f; // Freeze time
		}
	}
}
