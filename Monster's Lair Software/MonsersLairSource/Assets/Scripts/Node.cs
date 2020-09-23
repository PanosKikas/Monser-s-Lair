using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour 
{
	private SpriteRenderer rend;
	private Color startColor;

	[SerializeField]
	GameObject helpUI;

	// The hover color of the sprite
	[SerializeField]
	Color hoverColor;

	// Color shown when hovered and not enough money to build selected turret
	[SerializeField]
	Color notEnoughMoneyColor;

	// The turret on this node
	public Turret turret { get; set; }

	BuildManager buildManager;

	public static Node selectedNode { get; private set; }

	[SerializeField]
	GameObject nodeUI;

	Text textHelpUI;

	void Start () 
	{
		selectedNode = null;
		turret = null;
		rend = GetComponent<SpriteRenderer>();
		startColor = rend.material.color;
		buildManager = BuildManager.instance;
		textHelpUI = helpUI.GetComponentInChildren<Text>();
	}
	
	// When the cursor enters the node
	private void OnMouseEnter()
	{
		if (!buildManager.canBuild)
			return;

		if (!buildManager.canBuy)
		{
			rend.material.color = notEnoughMoneyColor;
			return;
		}

		if (turret == null)
		{
			rend.material.color = hoverColor;
		}	
	}

	// When the cursor exits from the node
	private void OnMouseExit()
	{
		rend.material.color = startColor;
	}

	// When the player presses the node
	private void OnMouseDown()
	{
		// Check if there is a GUI on top of the node
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		// if there is already a build turret on this node, select the node
		if (turret != null)
		{
			SelectNode();
			return;
		}
		// if no turret selected from the shop show error message
		if (!buildManager.canBuild)
		{
			textHelpUI.text = "No turret selected from the Shop.";
			helpUI.SetActive(true);
			return;
		}
		
		// if the player doesn't have enough money to buy the selected turret show error message
		if (!buildManager.canBuy)
		{
			textHelpUI.text = "Not enough money to build this turret";
			helpUI.SetActive(true);
			return;
		}
		// Build the selected turret on this node
		buildManager.BuildTurretOn(this);

	}

	// Function that handles the selection of a node
	void SelectNode()
	{
		// If already selected - deselect
		if (selectedNode == this)
		{
			DeselectNode();
			return;
		}

		selectedNode = this;
		// Enable the nodeUI
		nodeUI.SetActive(true);
		nodeUI.GetComponentInChildren<NodeUI>().SetTarget(this);
		buildManager.selectedTurret = null;
	}

	// Function that deselects the current node
	void DeselectNode()
	{
		selectedNode = null;
		nodeUI.SetActive(false);
	}
}
