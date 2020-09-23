using UnityEngine;

public class BuildManager : MonoBehaviour 
{
	public static BuildManager instance; // Sigletton instance
	public Turret selectedTurret { get; set; } // The selected turret to be built
	public bool canBuild { get { return selectedTurret != null; }  } // Indicates whether there's a turret selected
	public bool canBuy { get { return PlayerStats.Money >= selectedTurret.cost; } } // Indicates whether the player has enough money

	private void Awake()
	{
		// Sigletton Pattern
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Method that builds the selected turret on top of the _node
	public void BuildTurretOn(Node _node)
	{
		Turret turrInst = Instantiate(selectedTurret, GetBuildPosition(_node), Quaternion.identity) as Turret;
		turrInst.node = _node;
		_node.turret = turrInst.GetComponent<Turret>();
		PlayerStats.Money -= turrInst.cost;
	}

	// Helper method that positions the turret on right position
	Vector3 GetBuildPosition(Node node)
	{
		return node.transform.position + selectedTurret.offset;
	}
	// Method that upgrades the turret that is built on the _node
	public void UpgradeTurret(Node _node)
	{
		Turret turretOnNode = _node.turret;
		Destroy(turretOnNode.gameObject); // Destroy the current turret 
		selectedTurret = turretOnNode.upgradePrefab; 
		BuildTurretOn(_node); // Build the upgraded turret on this node
		selectedTurret = null;
	}
}
