using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("RPG/Weapon"))]
public class Weapon : ScriptableObject 
{
	[SerializeField] GameObject weaponPrefab;
	[SerializeField] Animator weaponAnimation; 

	public Transform gripTransform; 

	public GameObject getWeaponPrefab ()
	{
		return weaponPrefab; 
	}

}
