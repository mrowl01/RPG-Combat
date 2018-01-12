using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public abstract class AbilityBehavior : MonoBehaviour 
	{
		protected AbilityConfig config;

		const float PARTICE_CLEAN_UP_DELAY = 5f;

		public abstract void Use (AbilityUseParams useParams); 

		public void SetConfig (AbilityConfig configToSet)
		{
			config = configToSet;  
		}



		protected void PlayAbilitySound()
		{
			var abilitySound = config.GetRandomAbilitySound ();
			var audioSource = GetComponent<AudioSource> ();
			audioSource.PlayOneShot (abilitySound);
		}

		protected void PlayParticleEffect()
		{

			var particlePrefab = config.GetParticleSystem ();
			var ParticleObject = Instantiate 
					(particlePrefab, 
					transform.position, 
					particlePrefab.transform.rotation);
			ParticleObject.transform.parent = transform; // set world space in prefab if required
			ParticleObject.GetComponent<ParticleSystem>().Play(); 
			StartCoroutine (DestroyParticleWhenFinished(ParticleObject));
		}

		IEnumerator DestroyParticleWhenFinished (GameObject particlePrefab)
		{
			while (particlePrefab.GetComponent<ParticleSystem>().isPlaying)
				{
					yield return new WaitForSeconds(PARTICE_CLEAN_UP_DELAY); 
				}
			Destroy (particlePrefab);
				yield return new WaitForEndOfFrame(); 
		}
	}
}
