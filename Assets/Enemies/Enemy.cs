using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class Enemy : MonoBehaviour , IDamageable
{
	[SerializeField] float maxHealthPoints= 100f;
	[SerializeField] float currentHealthPoints= 100f;
	[SerializeField] float attackRadius= 4f;
	[SerializeField] float stopChasingRadius = 10f;
	[SerializeField] float chaseRadius = 6f; 
	[SerializeField] float projectileDamage = 8f; 
	[SerializeField] GameObject projectileSocket;
	[SerializeField] GameObject projectile;
	[SerializeField] float shootRate= 0.5f;
	[SerializeField] float fireBallDamage = 20f; 
	[SerializeField] Vector3 aimOffset = new Vector3 (0, 1, 0);

	GameObject trash ;

	bool isAttacking = false; 

	AICharacterControl aiCharacterControl= null;
	Vector3 myPos, playerPos;
	GameObject player= null;

	public float healthAsPercentage
	{
		get { return currentHealthPoints / maxHealthPoints;}
	}

	// Use this for initialization
	void Start ()
	{
		GameObject projectileParent = new GameObject ("ProjectileParent");
		trash = projectileParent;
		aiCharacterControl = GetComponent<AICharacterControl> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
		transform.LookAt (player.transform.position);
		if (distanceToPlayer <= attackRadius && ! isAttacking)
		{
			isAttacking = true;

			InvokeRepeating ("SpawnProjectile", 0, shootRate);
		} 
		if (distanceToPlayer > attackRadius) {
			isAttacking = false;
			CancelInvoke ();
		}
		if (distanceToPlayer <= chaseRadius) 
		{
			aiCharacterControl.SetTarget (player.transform);
		}
		if (distanceToPlayer >= stopChasingRadius)
		{
			aiCharacterControl.SetTarget (transform);
		}
	}
	void SpawnProjectile ()
	{
		GameObject newProjectile = Instantiate (projectile, projectileSocket.transform.position, Quaternion.identity);
		newProjectile.transform.SetParent (trash.transform);
		EnemyProjectile projectileComponent = newProjectile.GetComponent<EnemyProjectile> ();
		projectileComponent.setDamage (projectileDamage); 

		Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
		float projectileSpeed = projectileComponent.GetSpeed();
		newProjectile.GetComponent<Rigidbody> ().velocity = unitVectorToPlayer * projectileSpeed;
	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
	}
	void OnDrawGizmos()
	{
		//Run to radius
		Gizmos.color= Color.blue; 
		Gizmos.DrawWireSphere (transform.position, chaseRadius);
		//Draw attack Sphere
		Gizmos.color= Color.red;
		Gizmos.DrawWireSphere (transform.position, attackRadius);
		//Stop chasing radius
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, stopChasingRadius);

	}
}
