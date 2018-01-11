using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	
	public class PowerAttackBehavior : SpecialAbilityBehavior
	{

		PowerAttackConfig config ;
		AudioSource audioSource; 


		public void SetCongif (PowerAttackConfig configToSet)
		{
			this.config = configToSet;
		}

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();  	
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		void PlayParticleEffect ()
		{
			var prefab = Instantiate (config.GetParticleSystem (), transform.position, Quaternion.identity); 
			ParticleSystem myParticleSystem = prefab.GetComponent<ParticleSystem> (); 
			myParticleSystem.Play ();
			Destroy (myParticleSystem, myParticleSystem.main.duration); 
		}
		public override void Use (AbilityUseParams useParams)
		{
			audioSource.clip = config.GetAudioClip ();
			audioSource.Play ();
			DealDamage (useParams); 
		}

		void DealDamage (AbilityUseParams useParams)
		{
			print ("SOmething on rightclick params");
			float damageToDeal = useParams.baseDamage + config.GetExtraDamage ();
			useParams.target.TakeDamage (damageToDeal);
		}
	}
}
