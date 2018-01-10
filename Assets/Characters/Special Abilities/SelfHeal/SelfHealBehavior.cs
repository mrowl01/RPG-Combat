using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters 
{
	public class SelfHealBehavior : MonoBehaviour , ISpecialAbility
	{
		SelfHealConfig config; 
		Player player; 

		// Use this for initialization
		void Start () 
		{
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
		public void Use(AbilityUseParams useParams)
		{
			print ("self heal used by:  " + gameObject.name);
			player.TakeDamage (-config.GetExtraHealth ());// note - damage
		}
	}
}
