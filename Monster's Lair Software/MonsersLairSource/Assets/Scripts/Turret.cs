using UnityEngine;


public abstract class Turret : MonoBehaviour
{
	[Header("Turret Attributes")]
	[SerializeField]
	float radius = 3f;

	[Space]

	[Header("Turret Pricing Info")]
	[SerializeField]
	private int _cost;
	public int cost { get { return _cost; } }

	[SerializeField]
	private int _upgradeCost;
	public int upgradeCost { get { return _upgradeCost; } }

	[SerializeField]
	private int _sellValue;
	public int sellValue { get { return _sellValue; } }

	public Turret upgradePrefab { get { return _upgradePrefab; } }

	[Header("Optional")]
	[SerializeField]
	Turret _upgradePrefab;

	[Header("Unity Stuff")]
	[SerializeField]
	protected Transform firePoint;

	public Vector3 offset { get { return _offset; } }

	[SerializeField]
	private Vector3 _offset;

	protected Transform target;
	protected Animator animator;

	public Node node { get; set; }

	SpriteRenderer sr;


	private void Awake()
	{
		animator = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
	}

	protected virtual void Start () 
	{
		// Sets the sorting order of the turret equal to the sorting order of the node
		sr.sortingOrder = node.GetComponent<SpriteRenderer>().sortingOrder;
		target = null;
		InvokeRepeating("UpdateTarget", 0f, 0.2f);
	}

	protected abstract void Update();

	// Function to update every 0.2f to the closest target
	void UpdateTarget () 
	{

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float minDistance = Mathf.Infinity;
		Transform nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			// Find distance of the enemy from the tower
			float distance = Vector3.Distance(transform.position, enemy.transform.position);
			// Find the closeset enemy
			if (distance < minDistance)
			{
				minDistance = distance;
				nearestEnemy = enemy.transform;
			}
		}

		if (nearestEnemy != null && minDistance <= radius)
		{
			target = nearestEnemy;
		}
		else
		{
			target = null;
		}
	}

	abstract protected void Shoot();

	/* Helping method
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	*/
	// Function that handles what happens when the player sells the turret
	protected virtual void DestroyTurret()
	{
		target = null;
		node.turret = null;
		this.enabled = false;
	}
}
