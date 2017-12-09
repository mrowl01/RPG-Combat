using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
	[SerializeField] float maxHealthPoints= 100f;
	[SerializeField] float fireBallDamage = 5f; 
	[SerializeField] float meleeDamage = 3f;
	[SerializeField] float currentHealthPoints= 100f;

	[SerializeField]  const int walkableLayerNumber = 8;
	[SerializeField]  const int enemyLayerNumber = 9;
	[SerializeField] float  minTimeBetweenHits = 0.5f;
	[SerializeField] float maxMeleeAttackRange = 2f;
	[SerializeField] float maxSpellAttackRange = 5f; 


	float lastTimeHit= 0f; 
	GameObject currentTarget; 
	CameraRaycaster cameraRaycaster;
	[SerializeField] GameObject fireBall;

	void Start()
	{
		currentHealthPoints = maxHealthPoints;
		cameraRaycaster = GameObject.FindObjectOfType<CameraRaycaster> ();
		cameraRaycaster.notifyMouseClickObservers += OnEnemyClicked;
		cameraRaycaster.notifyMouseClickObservers += OnWalkClick;
	}
	// todo fixFireball , currently it will not fire because the player clicks on walkablelayer 
	//instanatly or is to close for script to allow ranged , and does meleee instead
	public float healthAsPercentage	{get { return currentHealthPoints / maxHealthPoints;}}
	public void TakeDamage (float damage)
	{
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
	}
	void OnParticleCollision(GameObject particle)// If hit with spell
	{
		Component damageable = gameObject.GetComponent (typeof(IDamageable));
		if (damageable)
		{
			(damageable as IDamageable).TakeDamage (fireBallDamage);
		}
	}
	void OnEnemyClicked(RaycastHit raycastHit, int layerHit)
	{
		if (layerHit == enemyLayerNumber) 
		{
			currentTarget = raycastHit.transform.gameObject;
			//if enemy is out of range exit
			if ((currentTarget.transform.position - transform.position ).magnitude > maxMeleeAttackRange) 
			{
				//if within ranged attack range , do ranged attack
				if(( currentTarget.transform.position- transform.position).magnitude <= maxSpellAttackRange)
				{
					fireBall.gameObject.SetActive (true);
					
				}
				return;
			}
			// If close enough for melee turn off  fireball
			fireBall.gameObject.SetActive (false);
			Enemy enemyComponent = currentTarget.GetComponent<Enemy> ();
			if (enemyComponent) 
			{
				if (Time.time - lastTimeHit > minTimeBetweenHits) 
				{
					enemyComponent.TakeDamage (meleeDamage);
					lastTimeHit = Time.time;
				}
			}

		}

	}
	void OnWalkClick (RaycastHit raycastHit, int layerHit )
	{
		if (layerHit == walkableLayerNumber) {
			fireBall.gameObject.SetActive (false);
		}
	}
}
