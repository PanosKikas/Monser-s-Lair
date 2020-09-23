using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Pathfinding))]
public class Enemy : MonoBehaviour 
{
	[SerializeField]
	private int startHealth = 100; 

	[SerializeField]
	private float startSpeed = 1f;

	public float speed { get; private set; }

	private float _health;

	public float health { get { return _health; }  set { _health = Mathf.Clamp(value, 0, startHealth); } }
	[SerializeField]
	private int worth = 50; // Money that gives to player when it dies 
	[SerializeField]
	private int score = 100; 

	protected Pathfinding enemyMovement;

	HealthIndicator healthIndicator; // Reference to it's healthbar

	protected bool isDead; 

	Animator animator;
	SpriteRenderer sr;
	Color flashColor = Color.red; // Color shown when it's hit

	Color startColor;

	static int sortingOrder = 100; // It's order in the particular sortingLayer

	void Start () 
	{
		health = startHealth;
		enemyMovement = GetComponent<Pathfinding>();
		healthIndicator = GetComponentInChildren<HealthIndicator>();
		++WaveSpawner.enemiesAlive; 
		healthIndicator.UpdateStatus((int)health, startHealth); // Update the values of the healthbar
		animator = GetComponent<Animator>();
		isDead = false;
		speed = startSpeed;
		sr = GetComponentInChildren<SpriteRenderer>();
		startColor = sr.color;
		sr.sortingOrder = sortingOrder;
		--sortingOrder; // Decrements the sorting order so that the next enemy to be spawned is gonna appear besides him
	}

	public void TakeDamage(float damage)
	{
		StartCoroutine(TakeDamageCo(damage));
	}
	// Function that damages the current enemy
	 IEnumerator TakeDamageCo(float damage)
	{
		if (isDead)
			yield return null;

		health -= damage; // Decrement its health
		
		healthIndicator.UpdateStatus(Mathf.CeilToInt(health), startHealth); 
		sr.color = flashColor; 
		yield return new WaitForSeconds(0.1f);
		
		sr.color = startColor;
		// Check if it died
		if (health <= 0 && !isDead)
		{
			Die();
		}
	}
	// Function that handles what happens when an enemy dies
	protected virtual void Die()
	{

		if (isDead)
			return;

		isDead = true;

		// Disable the tag of the enemy so that is no longer been detected from the turrets
		gameObject.tag = "Untagged";

		// Disable it's collider
		gameObject.GetComponent<Collider2D>().enabled = false;

		--WaveSpawner.enemiesAlive;
		
		// Play Die Animation
		animator.SetTrigger("Die");

		// Stop enemy it's movement
		enemyMovement.enabled = false;
		// Hide his health bar
		healthIndicator.gameObject.SetActive(false);

		// Add Money and Score to PlayerStats
		PlayerStats.Money += worth;
		PlayerStats.Score += score;

		Destroy(gameObject, 3f);
	}
	// Function that slows the enemy
	public void Slow(float pct)
	{
		speed = (1f - pct) * startSpeed;
	}
	// Function that resets the speed of the enemy to it's starting speed
	public void RestoreSpeed()
	{
		speed = startSpeed;
	}
}
