using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public abstract class SpecialAbilityBehavior : MonoBehaviour 
	{
		SpecialAbilityConfig config;

		public abstract void Use (AbilityUseParams useParams); 
	}
}
