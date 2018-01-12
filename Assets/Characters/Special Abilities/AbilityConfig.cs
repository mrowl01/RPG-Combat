using System.Collections;
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
	public abstract class AbilityConfig : ScriptableObject
	{
		[Header ("Special Ability Boss")]
		[SerializeField] float energyCost = 10f; 
		[SerializeField] GameObject particleSystemPrefab; 
		[SerializeField] AudioClip[] audioClips = null ; 
			

		protected AbilityBehavior behavior;

		public abstract AbilityBehavior GetBehaviorComponent (GameObject objectToAttachTo);

		public void AttachAbilityTo (GameObject objectToAttachTo)
		{
			AbilityBehavior behaviorComponent = GetBehaviorComponent (objectToAttachTo);
			behaviorComponent.SetConfig (this);
			behavior = behaviorComponent; 
		}

		public void Use (AbilityUseParams useParams)
			{
			behavior.Use (useParams) ;
			}

		public float GetEnergyCost ()
		{
			return energyCost; 
		}
		public AudioClip GetRandomAbilitySound()
		{
			return audioClips[Random.Range(0,audioClips.Length)]; 
		}
		public GameObject GetParticleSystem()
		{
			return particleSystemPrefab;
		}

	}

}