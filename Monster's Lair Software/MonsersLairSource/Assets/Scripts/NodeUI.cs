using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour 
{
	Node target; // The current node on which the NodeUI is currently pointing
	BuildManager buildManager; // Reference to the BuildManager class
	[SerializeField]
	GameObject ui; // The GUI of the nodeUI
	[SerializeField]
	Text upgradeValue; // Upgrade cost of the currently selected turret
	[SerializeField]
	Text sellValue; // Sell cost of the currently selected turret

	Turret turretOnNode; // The current turret that is built on top of the target Node

	[SerializeField]
	Button upgradeButton; 

	[SerializeField]
	GameObject helpUI; // Reference to the helpUI GUI
	Text helpTextUI;

	private void Start()
	{
		buildManager = BuildManager.instance;
		helpTextUI = helpUI.GetComponentInChildren<Text>();
	}
	// Function that directs the NodeUI to point on the _target Node and enables the Node GUI
	public void SetTarget(Node _target)
	{
		target = _target;
		transform.parent.position = target.transform.position + Vector3.down * 1f;
		turretOnNode = target.turret;
		// Set the text of the upgrade and sell cost depending on the turret on top of the target Node
		sellValue.text = turretOnNode.sellValue + " G";
		// If already upgraded once
		if (turretOnNode.upgradePrefab == null)
		{
			upgradeValue.text = "MAX";
			upgradeButton.interactable = false;
		}
		else
		{
			upgradeValue.text = turretOnNode.upgradeCost + " G";
			upgradeButton.interactable = true;
		}
	}
	// Function that is invoked from the 'Upgrade' Button and upgrades the current turret 
	public void Upgrade()
	{	// check if player has enough money to upgrade
		if (PlayerStats.Money >= turretOnNode.upgradeCost) // Decrease player's money
		{
			buildManager.UpgradeTurret(target);
			Hide(); 
		}
		else
		{
			helpTextUI.text = "Not enough money to upgrade!"; // print error message
			helpUI.SetActive(true);
		}
	}
	// Function that is invoked from the 'Sell' Button from the Node GUI and sells the current turret
	public void Sell()
	{
		Animator turretAnim = turretOnNode.GetComponent<Animator>();
		turretAnim.SetTrigger("Destroy"); // Play destroy animation
		PlayerStats.Money += turretOnNode.sellValue; // Add it's sellValue to the sum of the Player's money
		Hide();
		target.turret = null;
		Destroy(turretOnNode.gameObject, 2f);
	}
	// Hide the node GUI 
	void Hide()
	{
		ui.SetActive(false);
	}
}
