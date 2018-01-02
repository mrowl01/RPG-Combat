using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Characters 
{
	[RequireComponent(typeof(Image))]
	public class EnergyBar : MonoBehaviour 
	{
			[SerializeField] Image energyImage = null; 
			[SerializeField] float maxEnergy= 100;
			[SerializeField] float currentEnergy=100; 
			[SerializeField] float pointsPerHit = 10f; 

			CameraRaycaster cameraRaycaster = null; 

			void Start()
			{
				cameraRaycaster = Camera.main.GetComponent<CameraRaycaster> ();
				SetMaxEnergy () ; 
			}
			void Update()
			{
			}
			void SetMaxEnergy ()
			{
				currentEnergy = maxEnergy;
				energyImage.fillAmount = EnergyAsPercent (); 
			}
			public float EnergyAsPercent ()
			{
				return currentEnergy / maxEnergy; 
			}
			public bool IsEnergyAvailable (float amount)
			{
				return amount <= currentEnergy; 
			}
				
			public void ConsumeEnergy (float amount)
			{
				float newEnergyPoints = currentEnergy - amount;
				currentEnergy = Mathf.Clamp (newEnergyPoints, 0, maxEnergy);

				energyImage.fillAmount = EnergyAsPercent (); 
			}

				
	}
}