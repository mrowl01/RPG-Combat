﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Characters 
{
	[CreateAssetMenu (menuName = ("RPG/Special Ability/ Power Attack"))]
	public class PowerAttackConfig: SpecialAbilityConfig
	{
		[Header("Power Attack Specific")]
		[SerializeField] float extraDamage = 10f; 

		public override void AttachComponentTo (GameObject gameObjectToAttachTo )
		{
			var behaviorComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
			behaviorComponent.SetCongif(this);
			behavior = behaviorComponent; 
		}
		public float GetExtraDamage ()
		{
			return extraDamage; 
		}
	}
}
