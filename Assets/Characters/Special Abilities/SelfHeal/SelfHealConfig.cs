using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu(menuName= ("RPG/Special Ability/ SelfHeal"))] 
	public class SelfHealConfig : AbilityConfig  
	{
		[Header("SelfHeal Specific")]
		float extraHealth = 500f; 

		public override AbilityBehavior GetBehaviorComponent (GameObject gameObjectToAttachTo )
		{
			return gameObjectToAttachTo.AddComponent<SelfHealBehavior>();
		}
		public float GetExtraHealth ()
		{
			return extraHealth; 
		}

	}
}
