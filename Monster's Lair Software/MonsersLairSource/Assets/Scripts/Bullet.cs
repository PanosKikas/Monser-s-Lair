using UnityEngine;

public class Bullet : Projectile
{

	protected override void HitTarget()
	{
		DamageTarget(target);
		Destroy(gameObject);
	}
}
