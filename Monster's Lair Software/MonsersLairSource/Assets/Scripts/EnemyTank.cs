using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy {

	[SerializeField]
	ParticleSystem deathParticles; // Reference to the effect that plays when it dies

	[SerializeField]
	Enemy deathObject; // Reference to the enemy object that it's spawned when the enemyTank dies

	private int currPoint; // Holds the position of the current waypoint that the enemy is moving

	private bool dead = false;

	protected override void Die()
	{
		if (dead)
			return;

		dead = true;
		++WaveSpawner.enemiesAlive;
		currPoint = enemyMovement.currentPointIndex; 
		base.Die();
	}

	void SpawnEnemy()
	{
		--WaveSpawner.enemiesAlive;
		deathParticles.Play(); // Play the death effect
		Enemy enemy = Instantiate(deathObject, transform.position, Quaternion.identity); // Spawn deathObject Enemy 
		enemy.GetComponent<Pathfinding>().SetCurrentPoint(currPoint); // Set deathObject's current waypoint to continue enemyTank's path
	}
}
