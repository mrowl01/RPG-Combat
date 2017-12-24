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
