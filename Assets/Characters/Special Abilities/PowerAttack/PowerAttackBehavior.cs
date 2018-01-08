using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	
	public class PowerAttackBehavior : MonoBehaviour, ISpecialAbility
	{

		PowerAttackConfig config ;

		public void SetCongif (PowerAttackConfig configToSet)
		{
			this.config = configToSet;
		}

		// Use this for initialization
		void Start () 
		{
			print ("Power attack behavior attached to  " + gameObject.name); 	
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
		public void Use (AbilityUseParams useParams)
		{
			
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
