using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class Pathfinding : MonoBehaviour 
{
	private Transform currentPoint; // The current point that the enemy is moving towards
	public int currentPointIndex { get; private set; } // The index of the point in the Waypoints array
	private float minDistance = 0.1f; // The minDistance that the enemy is approaching 
									  // the currentPoint before he moves to the next one
	Enemy enemy; // The enemy on which this object is attached to
	SpriteRenderer sr;

	private void Awake()
	{
		// Initialize the currentPoint so the enemy is moving towards the first point
		if (Waypoints.points.Length > 0)
		{
			currentPoint = Waypoints.points[0].transform;
			currentPointIndex = 0;
		}
		else
		{
			Debug.LogError("Waypoint array is empty!");
			return;
		}
		enemy = GetComponent<Enemy>();
		sr = GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update () 
	{
		// Direction in which the enemy will move
		Vector3 dir = currentPoint.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime);

		// Find the distance till the next waypoint
		float distanceRemain = Vector3.Distance(transform.position, currentPoint.position);
		if (distanceRemain < minDistance)
		{
			NextWaypoint();
		}
		enemy.RestoreSpeed();

	}
	// Function that makes the enemy point to the next position waypoint in the array of Waypoints
	void NextWaypoint()
	{
		// Check if it is the end of our path
		if (currentPointIndex >= Waypoints.points.Length - 1)
		{
			// if it is the last waypoint end the path
			EndPath();
			return;
		}

		currentPointIndex++;
		currentPoint = Waypoints.points[currentPointIndex];
		if (currentPoint.CompareTag("Flip")) 
		{
			Flip();
		}
	}
	// functionality when the enemy reaches the end of the path
	void EndPath()
	{
		WaveSpawner.enemiesAlive--;
		PlayerStats.Lives--;
		// Destroy this enemy
		Destroy(enemy.gameObject);
		this.enabled = false;
	}

	void Flip()
	{
		sr.flipX = !sr.flipX;
	}
	// Set the current point on which the enemy is moving to
	public void SetCurrentPoint(int point)
	{
		currentPointIndex = point;
		currentPoint = Waypoints.points[point];
	}
	
}
