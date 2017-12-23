using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour 
{
	[SerializeField] float projectileDamage = 3.5f;
	[SerializeField] float projectileSpeed = 5f; 
	[SerializeField] GameObject shooter ; // able to inspect when paused

	const float DESTROYDELAY = 3f; 


	public void SetShooter(GameObject shooter)
	{
		this.shooter = shooter;
	}

	public float GetSpeed()
	{
		return projectileSpeed;
	}
	public void setDamage (float damage)
	{
		projectileDamage = damage;
	}
	void DestroyThisObject()
	{
		Destroy (gameObject);
	}
	void Start()
	{
		Invoke ("DestroyThisObject", DESTROYDELAY);
	}
	void OnCollisionEnter(Collision collision )
	{
		Component damageable = collision.gameObject.GetComponent (typeof(IDamageable));
		if (collision.gameObject.layer != shooter.layer) 
		{
			DamageIfDamageable (collision, damageable);
		}
	}

	void DamageIfDamageable (Collision collision, Component damageable)
	{
		if (damageable) 
		{
			(damageable as IDamageable).TakeDamage (projectileDamage);
		}
		DestroyThisObject ();
	}
}
