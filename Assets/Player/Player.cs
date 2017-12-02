﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField] float maxHealthPoints= 100f;

	[SerializeField] float currentHealthPoints= 100f;

	public float healthAsPercentage
	{
		get { return currentHealthPoints / maxHealthPoints;}
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}