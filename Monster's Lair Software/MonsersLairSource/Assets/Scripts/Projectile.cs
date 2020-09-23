using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour 
{
	protected Transform target; // Position of the enemy that this projectile is chasing

	[SerializeField]
	private float speed = 20f; // The speed of the projectile

	[SerializeField]
	protected int damage = 25; // The damage that this projectile deals on impact
	
	public void Seek(Transform _enemy)
	{
		target = _enemy;
	}

	void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}
		// Find the distance that it is going to move this frame
		Vector2 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;
		// if this frame it's gonna hit the enemy
		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}
		// Move the projectile
		transform.Translate(distanceThisFrame * dir.normalized, Space.World);
	}

	protected abstract void HitTarget();
	// Function that deals damage to the enemy hit
	protected void DamageTarget(Transform _enemy)
	{
		Enemy enemy = _enemy.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}
	}
}


