using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu (menuName = ("RPG/Special Ability/ Blizzard"))]
	public class BlizzardConfig : SpecialAbilityConfig
	{
		[Header ("Blizzard specific")]
		[SerializeField] float radius = 5f; 
		[SerializeField] float dmgToTargets = 15f; 

		public override void AttachComponentTo (GameObject gameObjectToAttachTo)
		{
			var component = gameObjectToAttachTo.AddComponent<Blizzardbehavior> (); 
			component.SetConfig (this); 
			behavior = component; 
			
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
