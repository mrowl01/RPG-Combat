﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	
	public class PowerAttackBehavior : MonoBehaviour, ISpecialAbility
	{

		PowerAttackConfig config ;

		public void SetCongif (PowerAttackConfig configToSet)
		{
			this.config = configToSet;
		}

		// Use this for initialization
		void Start () 
		{
			print ("Power attack behavior attached to  " + gameObject.name); 	
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Use (AbilityUseParams useParams)
		{
			print ("SOmething on rightclick params"); 
			float damageToDeal = useParams.baseDamage + config.GetExtraDamage ();
			useParams.target.TakeDamage (damageToDeal); 
		}
	}
}
