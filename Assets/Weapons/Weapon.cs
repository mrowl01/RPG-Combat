using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Weapons 
{

[CreateAssetMenu(menuName = ("RPG/Weapon"))]
public class Weapon : ScriptableObject 
{
	[SerializeField] GameObject weaponPrefab;
	[SerializeField] AnimationClip weaponAnimation; 
	[SerializeField] float maxAttackRange = 3f; 
		[SerializeField] float weaponDamage = 50f; 

		public float GetWeaponDamage ()
		{
			return weaponDamage; 
		}

		public float MaxAttackRange ()
		{
			return maxAttackRange; 
		}

	public AnimationClip GetAttackAnimClip()
	{
		return weaponAnimation; 
	}

	public Transform gripTransform; 

	public GameObject getWeaponPrefab ()
	{
		return weaponPrefab; 
	}
	

}
}
