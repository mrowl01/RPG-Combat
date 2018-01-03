﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core; 

namespace RPG.Characters
{

	public struct AbilityUseParams
	{
		public IDamageable target; 
		public float baseDamage ; 

		public AbilityUseParams (IDamageable target ,  float baseDamage )
		{
			this.target = target; 
			this.baseDamage = baseDamage; 
		}

	}
	public abstract class SpecialAbilityConfig : ScriptableObject
	{
			[Header ("Special Ability Boss")]
			[SerializeField] float energyCost = 10f; 

		protected ISpecialAbility behavior;

			abstract public void AttachComponentTo (GameObject gameObjectToAttachTo ) ;


		public void Use (AbilityUseParams useParams)
			{
			behavior.Use (useParams) ;
			}

		public float GetEnergyCost ()
		{
			return energyCost; 
		}

	}



	public interface ISpecialAbility
	{
		void Use(AbilityUseParams useParams); 
	}

}