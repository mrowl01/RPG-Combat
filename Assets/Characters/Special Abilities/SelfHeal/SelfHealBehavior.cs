using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters 
{
	public class SelfHealBehavior : AbilityBehavior
	{
		Player player; 
		AudioSource audioSource; 

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();
			player = GetComponent<Player> ();
			
		}

		public override void Use(AbilityUseParams useParams)
		{
			PlayAbilitySound ();
			player.Heal ((config as SelfHealConfig).GetExtraHealth ());// note - damage
			PlayParticleEffect(); 
		}
	}
}
