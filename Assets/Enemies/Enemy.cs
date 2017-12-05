using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class Enemy : MonoBehaviour 
{
	[SerializeField] float maxHealthPoints= 100f;
	[SerializeField] float currentHealthPoints= 100f;
	[SerializeField] float attackRadius= 4f;

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
		aiCharacterControl = GetComponent<AICharacterControl> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
		if (distanceToPlayer <= attackRadius)
		{
			aiCharacterControl.SetTarget (player.transform);
		
		} else
		{
			aiCharacterControl.SetTarget (transform);
		}
	}
}
