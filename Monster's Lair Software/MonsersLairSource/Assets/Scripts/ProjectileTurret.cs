using UnityEngine;

public class ProjectileTurret : Turret 
{
	[Header("Projectile Info")]
	[SerializeField]
	Projectile projectilePrefab; // The projectiles this turret is shooting
	[SerializeField]
	float fireRate = 1f; 

	private float fireCountdown;

	protected override void Start()
	{
		base.Start();
		fireCountdown = 1f/ fireRate;
	}

	protected override void Update () 
	{
		// no target to shoot
		if (target == null)
		{
			return;
		}
		// if it's time to shoot the next projectile
		if (fireCountdown <= 0f)
		{
			animator.SetTrigger("Shoot");
			fireCountdown = 1f / fireRate;
		}
		fireCountdown -= Time.deltaTime; // Decrease countdown
	}
	// Function that handles the shooting of the turret
	protected override void Shoot()
	{
		if (target != null)
		{
			// Find direction from the firePoint to the target
			Vector2 dir = target.position - firePoint.position;

			// Set the rotation of firePoint to face the enemy
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			Vector3 rot = rotation.eulerAngles;
			rot = new Vector3(rot.x, rot.y, rot.z + 180);
			firePoint.rotation = Quaternion.Euler(rot);

			// Spawn the projectile at firePoint position with firePoint rotation
			Projectile projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as Projectile;
			projectileGO.Seek(target); // Make the projectile chase the target 

		}
	}
}
