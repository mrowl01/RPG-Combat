using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour 
{
	[SerializeField] float projectileDamage = 3.5f;
	[SerializeField] float projectileSpeed = 5f; 

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
		//Invoke ("DestroyThisObject", 3f);
	}
	void OnCollisionEnter(Collision collision )
	{
		Component damageable = collision.gameObject.GetComponent (typeof(IDamageable));

		if (damageable && collision.gameObject.CompareTag("Player")) 
		{
				(damageable as IDamageable).TakeDamage (projectileDamage);
		}
		DestroyThisObject ();
	}
}
