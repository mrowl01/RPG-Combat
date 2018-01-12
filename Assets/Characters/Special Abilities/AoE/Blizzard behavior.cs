using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core ; 

namespace RPG.Characters
{
	
	public class Blizzardbehavior : AbilityBehavior
	{
		AudioSource audioSource = null; 


		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();		
		}

		public override void Use (AbilityUseParams useParams )
		{
			PlayAbilitySound ();
			PlayParticleEffect ();
			AoEDamage (useParams); 
		}	

		void AoEDamage (AbilityUseParams useParams)
		{
			print ("Area effect used by" + gameObject.name);
			//Static sphere for cast					//origin			radius				direction		max distance
			RaycastHit[] hits = Physics.SphereCastAll (transform.position, (config as BlizzardConfig).GetRadius (), Vector3.up, (config as BlizzardConfig).GetRadius ());
			foreach (RaycastHit hit in hits) 
			{
				var damageable = hit.collider.gameObject.GetComponent<IDamageable> ();
				bool hitPlayer = hit.collider.gameObject.GetComponent<Player> ();
				if (damageable != null && !hitPlayer)
					
				{
					float damageToDeal = useParams.baseDamage + (config as BlizzardConfig).GetDmgToTargets ();
					// TODO art hat
					damageable.TakeDamage (damageToDeal);
				}
			}
		}
	}
}
