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
		[SerializeField] float maxEnergy;
		[SerializeField] float currentEnergy; 

		CameraRaycaster cameraRaycaster = null; 

		void Start()
		{
			cameraRaycaster = Camera.main.GetComponent<CameraRaycaster> ();
			cameraRaycaster.notifyMouseClickObservers += DecreaseEnergy;

			SetEnergy (); 
		}
		void Update ()
		{
			
		}

		void SetEnergy ()
		{
			energyImage.fillAmount = 1; 
		}
		public void DecreaseEnergy (RaycastHit raycastHit, int layerHit)
		{
			float decrease = 0.01f; 
			energyImage.fillAmount = energyImage.fillAmount - decrease; 
		}
		
}
}