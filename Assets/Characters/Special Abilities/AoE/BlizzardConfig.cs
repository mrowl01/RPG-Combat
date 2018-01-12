using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu (menuName = ("RPG/Special Ability/ Blizzard"))]
	public class BlizzardConfig : AbilityConfig
	{
		[Header ("Blizzard specific")]
		[SerializeField] float radius = 5f; 
		[SerializeField] float dmgToTargets = 15f; 

		public override AbilityBehavior GetBehaviorComponent (GameObject gameObjectToAttachTo )
		{
			return gameObjectToAttachTo.AddComponent<Blizzardbehavior>();
		}
		public float GetDmgToTargets ()
		{
			return dmgToTargets; 
		}
		public float GetRadius ()
		{
			return radius; 
		}

	}
}
