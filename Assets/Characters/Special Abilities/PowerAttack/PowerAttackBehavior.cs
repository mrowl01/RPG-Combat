using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	
	public class PowerAttackBehavior : AbilityBehavior
	{

		AudioSource audioSource = null; 

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();  	
		}
		public override void Use (AbilityUseParams useParams)
		{
			PlayAbilitySound ();
			PlayParticleEffect ();
			DealDamage (useParams); 
		}

		void DealDamage (AbilityUseParams useParams)
		{
			float damageToDeal = useParams.baseDamage + (config as PowerAttackConfig).GetExtraDamage ();
			useParams.target.TakeDamage (damageToDeal);
		}
	}
}
