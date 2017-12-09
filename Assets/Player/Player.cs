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

	void Start()
	{
		currentHealthPoints = maxHealthPoints;
		cameraRaycaster = GameObject.FindObjectOfType<CameraRaycaster> ();
		cameraRaycaster.notifyMouseClickObservers += OnEnemyClicked;
	}
	public float healthAsPercentage	{get { return currentHealthPoints / maxHealthPoints;}}
	public void TakeDamage (float damage)
	{
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
	}
	void OnEnemyClicked(RaycastHit raycastHit, int layerHit)
	{
		if (layerHit == enemyLayerNumber) 
		{
			currentTarget = raycastHit.transform.gameObject;
			//if enemy is out of range exit
			if ((currentTarget.transform.position - transform.position ).magnitude > maxMeleeAttackRange) 
			{
				return;
			}
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

}
