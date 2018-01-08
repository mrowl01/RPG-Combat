using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core ; 

namespace RPG.Characters
{
	
	public class Blizzardbehavior : MonoBehaviour, ISpecialAbility
	{
		BlizzardConfig config; 

		public void SetConfig  (BlizzardConfig config)
		{
			this.config = config; 
		}

		// Use this for initialization
		void Start () 
		{
			print ("Area affect has attached to " + gameObject.name); 
		}
		
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

		public void Use (AbilityUseParams useParams )
		{
			PlayParticleEffect ();
			AoEDamage (useParams); 

		}	

		void AoEDamage (AbilityUseParams useParams)
		{
			print ("Area effect used by" + gameObject.name);
			//Static sphere for cast					//origin			radius				direction		max distance
			RaycastHit[] hits = Physics.SphereCastAll (transform.position, config.GetRadius (), Vector3.up, config.GetRadius ());
			foreach (RaycastHit hit in hits) {
				var damageable = hit.collider.gameObject.GetComponent<IDamageable> ();
				if (damageable != null) {
					float damageToDeal = useParams.baseDamage + config.GetDmgToTargets ();
					// TODO art hat
					damageable.TakeDamage (damageToDeal);
				}
			}
		}
	}
}
