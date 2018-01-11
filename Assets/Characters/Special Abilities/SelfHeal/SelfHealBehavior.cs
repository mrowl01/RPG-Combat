using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters 
{
	public class SelfHealBehavior : SpecialAbilityBehavior
	{
		SelfHealConfig config; 
		Player player; 
		AudioSource audioSource; 

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();
			player = GetComponent<Player> ();
			
		}

		public void SetConfig (SelfHealConfig config)
		{
			this.config = config; 
		}
		void PlayParticleEffect ()
		{
			var prefab = Instantiate (config.GetParticleSystem (), transform.position, Quaternion.identity); 
			ParticleSystem myParticleSystem = prefab.GetComponent<ParticleSystem> (); 
			myParticleSystem.Play ();
			Destroy (myParticleSystem, myParticleSystem.main .duration); 
		}
		public override void Use(AbilityUseParams useParams)
		{
			audioSource.clip = config.GetAudioClip ();
			audioSource.Play ();

			print ("self heal used by:  " + gameObject.name);
			player.Heal (config.GetExtraHealth ());// note - damage
		}
	}
}
