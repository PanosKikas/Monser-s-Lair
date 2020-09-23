using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour 
{
	[SerializeField]
	Turret[] TurretItems; // An array with all the turret items of the shop

	BuildManager buildManager; // Reference to the BuildManager class

	[SerializeField]
	GameObject NodeUI; // Reference to the GUI of the nodeUI Class

	Animator animator; 

	private void Start()
	{
		buildManager = BuildManager.instance;
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (GameManager.instance.gameIsOver)
		{
			this.enabled = false;
			if (animator.GetBool("Pressed"))
			{
				Toggle();
			}
			GetComponentInChildren<Button>().interactable = false; // The player can't open the shop if the game is over
			return;
		}
	}
	// Function that is invoked from the button - item selected from shop. Selects the item
	public void SelectTurret(int i)
	{
		if (i >= TurretItems.Length)
		{
			Debug.LogError("No such turret found in TurretItems(Shop). Array out of index");
		}
		buildManager.selectedTurret = TurretItems[i];

		// if NodeUI is active hide it
		if (NodeUI.activeSelf)
		{
			NodeUI.SetActive(false);
		}
	}
	// Toggles the animation state (open - close) of the shop menu
	public void Toggle()
	{
		animator.SetBool("Pressed", !animator.GetBool("Pressed"));
	}
}
