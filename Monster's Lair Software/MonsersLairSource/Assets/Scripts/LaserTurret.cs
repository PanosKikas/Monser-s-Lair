using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserTurret : Turret 
{
	LineRenderer lr;
	private bool shooting; 
	[SerializeField]
	private float damageOverTime = 20f; 
	[SerializeField]
	private float slowness = 0.3f; // The amount of slowness that this turret is dealing 

	Enemy targetEnemy; // Reference to the current Enemy target
	private ParticleSystem laserImpactEffect; // The effect of the laser when the turret is shooting

	protected override void Start () 
	{
		base.Start();
		shooting = false;
		targetEnemy = null;
		lr = GetComponent<LineRenderer>();
		laserImpactEffect = GetComponentInChildren<ParticleSystem>();
		laserImpactEffect.Stop();
	}

	protected override void Update()
	{
		// If no target found to shoot disable the laser
		if (target == null)
		{
			if (lr.enabled)
			{
				lr.enabled = false;
				animator.SetBool("Shoot", false);
			}
			laserImpactEffect.Stop();
			shooting = false;
			return;
		}
		// Enable the laser and shoot the target
		if (!shooting)
		{
			shooting = true;
			animator.SetBool("Shoot", true);
			return;
		}
	}

	protected override void Shoot()
	{
		StartCoroutine(Laser());
	}

	// Method that handles the shooting of the LaserTurret
	IEnumerator Laser()
	{
		if (!shooting)
		{
			lr.enabled = false;
			laserImpactEffect.Stop();
			yield return null;
		}

		lr.enabled = true;
		// Calculate the position of the laser and deal damage to the target Enemy
		while (shooting)
		{
			lr.SetPosition(0, new Vector3(firePoint.position.x, firePoint.position.y, 0));
			lr.SetPosition(1, new Vector3(target.position.x, target.position.y, 0));

			targetEnemy = target.GetComponent<Enemy>();

			Vector3 dir = targetEnemy.transform.position - firePoint.position;
			laserImpactEffect.transform.position = target.transform.position + dir.normalized * -0.3f;
			// Play the laser effect
			if (laserImpactEffect.isStopped)
			{
				laserImpactEffect.Play();
			}
			
			targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
			targetEnemy.Slow(slowness);
			yield return null;
		}
	}
	// Function that disables the laser when the turret is destroyed
	protected override void DestroyTurret()
	{
		if (lr.enabled)
		{
			lr.enabled = false;
		}
		StopAllCoroutines();
		if (laserImpactEffect.isPlaying)
			laserImpactEffect.Stop();

		base.DestroyTurret();	
	}
}
