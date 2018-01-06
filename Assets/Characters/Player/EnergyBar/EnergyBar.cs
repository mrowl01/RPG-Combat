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
		[SerializeField] float regenPointsPerSecond = 5.2f; 

			CameraRaycaster cameraRaycaster = null; 

			void Start()
			{
				cameraRaycaster = Camera.main.GetComponent<CameraRaycaster> ();
				SetMaxEnergy () ; 
			}
			void Update()
			{
			if (currentEnergy < maxEnergy)
				{
					AddEnergyPoints (); 
					UpdateEnergyBar ();
				}
			}
			
			void AddEnergyPoints ()
			{
			var pointsToAdd = regenPointsPerSecond * Time.deltaTime;
			currentEnergy = Mathf.Clamp (currentEnergy + pointsToAdd, 0, maxEnergy);
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
		void UpdateEnergyBar ()
		{
			energyImage.fillAmount = EnergyAsPercent (); 
		}
				
			public void ConsumeEnergy (float amount)
			{
				float newEnergyPoints = currentEnergy - amount;
				currentEnergy = Mathf.Clamp (newEnergyPoints, 0, maxEnergy);

				UpdateEnergyBar (); 
			}

				
	}
}