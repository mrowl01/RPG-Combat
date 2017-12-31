using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Characters {
	[RequireComponent(typeof(Image))]
public class EnergyBar : MonoBehaviour 
{
		[SerializeField] Image energyImage; 
		[SerializeField] float maxEnergy= 100;
		[SerializeField] float currentEnergy=100; 
		[SerializeField] float pointsPerHit = 10f; 

		CameraRaycaster cameraRaycaster = null; 

		void Start()
		{
			cameraRaycaster = Camera.main.GetComponent<CameraRaycaster> ();
			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
			FullHealth () ; 
		}
		void Update()
		{
			UpdateHealth (); 
		}
		float EnergyAsPercent ()
		{
			return currentEnergy / maxEnergy; 
		}

		void FullHealth ()
		{
			currentEnergy = maxEnergy;
			energyImage.fillAmount = EnergyAsPercent (); 
		}
		void UpdateHealth ()
		{
			energyImage.fillAmount = EnergyAsPercent (); 
		}
		public void OnMouseOverEnemy (Enemy enemy)
		{
			if (Input.GetMouseButtonDown (0))
			{
				float newEnergyPoints = currentEnergy - pointsPerHit;
				currentEnergy = Mathf.Clamp (newEnergyPoints, 0, maxEnergy);
			}
		}
			
}
}