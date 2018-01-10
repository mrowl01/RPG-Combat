using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[CreateAssetMenu(menuName= ("RPG/Special Ability/ SelfHeal"))] 
	public class SelfHealConfig : SpecialAbilityConfig  
	{
		[Header("SelfHeal Specific")]
		float extraHealth = 500f; 

		public override void AttachComponentTo (GameObject gameObjectToAttachTo)
		{
			SelfHealBehavior selfHealBehavior = gameObjectToAttachTo.AddComponent<SelfHealBehavior> (); 
			selfHealBehavior.SetConfig (this);
			behavior = selfHealBehavior; 
		}
		public float GetExtraHealth ()
		{
			return extraHealth; 
		}

	}
}
