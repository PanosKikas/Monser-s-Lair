using System;
using UnityEngine;

public class Missile : Projectile 
{
	[SerializeField]
	private float radius = 3f; // The radius of the missile at which it explodes

	[SerializeField]
	private GameObject impactEffect; // Reference to the explosion effect
	// Override function that finds all the enemies around the enemy it hit and damages every one of them
	protected override void HitTarget()
	{
		GameObject impactGO = Instantiate(impactEffect, transform.position, impactEffect.transform.rotation);
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);
		Destroy(impactGO, 1.5f);
		foreach (Collider2D col in enemies)
		{
			if (col.CompareTag("Enemy"))
			{
				DamageTarget(col.transform);
			}
		}
		Destroy(gameObject);
	}

	/* Helper method to shows the radius of the missile
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	*/
}
