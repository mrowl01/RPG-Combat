using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;
using RPG.CameraUI; // TODO possibly rewire code

namespace RPG.Characters {

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]

public class PlayerMovement : MonoBehaviour
{

    Vector3 currentDestination, clickPoint;

	private bool isInDirectMode= false;

	AICharacterControl aiMovement = null; 
	ThirdPersonCharacter character = null ;   			// A reference to the ThirdPersonCharacter on the object
	CameraRaycaster cameraRaycaster = null;

	GameObject walkTarget = null; 

	[SerializeField]  const int walkableLayerNumber = 8;
	[SerializeField]  const int enemyLayerNumber = 9;

    private void Start()
    {
		aiMovement = GetComponent<AICharacterControl> ();
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
		if (walkTarget == null) {
			walkTarget = new GameObject ("walkTarget");
		}

		cameraRaycaster.notifyMouseClickObservers +=ProcessMouseClick ;
    }

	void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
	{
		switch (layerHit)
		{
		case walkableLayerNumber:
			walkTarget.transform.position = raycastHit.point;
			aiMovement.SetTarget (walkTarget.transform);
			break;
		case enemyLayerNumber:
			aiMovement.SetTarget (raycastHit.collider.gameObject.transform);
			break;
		default:
			Debug.LogError ("Don't know how to handle ProcessMouseClick() default"); 
			break;
		}

	}

    private void FixedUpdate()
    {
       
		if (CrossPlatformInputManager.GetButtonDown("Fire3"))
			{
			isInDirectMode = ! isInDirectMode;
			currentDestination = transform.position;// RESET CLICK TARGET
			}
		if (isInDirectMode) 
		{
			//ProcessMouseClick ();
		
		} else 
		{
			
			//ProcessMouseMovement ();
		}
			
    }

	//TODO make this get called again
//	void ProcessDirectMovement()
//	{
//		float h = CrossPlatformInputManager.GetAxis("Horizontal");
//		float v = CrossPlatformInputManager.GetAxis("Vertical");
//		if (mainCamera != null)
//		{
//			// calculate camera relative direction to move:
//			mainCameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
//			movement = v*mainCameraForward + h*mainCamera.right;
//		}
//		character.Move (movement,false,false);
//	}

}
}



