using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core ; 

namespace RPG.Characters
{
	
	public class Blizzardbehavior : SpecialAbilityBehavior
	{
		BlizzardConfig config; 
		AudioSource audioSource; 

		public void SetConfig  (BlizzardConfig config)
		{
			this.config = config; 
		}

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();		}
		
		// Update is called once per frame
		void Update () 
		{
			
			
		}
		void PlayParticleEffect ()
		{
			var prefab = Instantiate (config.GetParticleSystem (), transform.position, Quaternion.identity); 
			//TODO decide if particle effect should attatch to player 
			ParticleSystem myParticleSystem = prefab.GetComponent<ParticleSystem>(); 
			myParticleSystem.Play (); 
			Destroy (prefab, myParticleSystem.main.duration); 
		}

		public override void Use (AbilityUseParams useParams )
		{
			PlayParticleEffect ();
			audioSource.clip = config.GetAudioClip ();
			audioSource.Play ();
			AoEDamage (useParams); 

		}	

		void AoEDamage (AbilityUseParams useParams)
		{
			print ("Area effect used by" + gameObject.name);
			//Static sphere for cast					//origin			radius				direction		max distance
			RaycastHit[] hits = Physics.SphereCastAll (transform.position, config.GetRadius (), Vector3.up, config.GetRadius ());
			foreach (RaycastHit hit in hits) 
			{
				var damageable = hit.collider.gameObject.GetComponent<IDamageable> ();
				bool hitPlayer = hit.collider.gameObject.GetComponent<Player> ();
				if (damageable != null && !hitPlayer)
					
				{
					float damageToDeal = useParams.baseDamage + config.GetDmgToTargets ();
					// TODO art hat
					damageable.TakeDamage (damageToDeal);
				}
			}
		}
	}
}
