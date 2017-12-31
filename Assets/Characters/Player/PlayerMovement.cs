using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI; // TODO possibly rewire code

namespace RPG.Characters {

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]

public class PlayerMovement : MonoBehaviour
{

    Vector3 clickPoint;

	AICharacterControl aiMovement = null; 
	ThirdPersonCharacter character = null ;   			// A reference to the ThirdPersonCharacter on the object
	CameraRaycaster cameraRaycaster = null;

	GameObject walkTarget = null; 


    private void Start()
    {
		aiMovement = GetComponent<AICharacterControl> ();
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
		if (walkTarget == null) {
			walkTarget = new GameObject ("walkTarget");
		}

			cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalk;
			cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
    }
		void OnMouseOverEnemy (Enemy enemy)
		{
			if (Input.GetMouseButton (0)|| Input.GetMouseButtonDown (1)) 
			{
				aiMovement.SetTarget (enemy.transform);
			}
		}

		void OnMouseOverPotentiallyWalk (Vector3 destination )
		{
			if (Input.GetMouseButton (0)  ) 
			{
				walkTarget.transform.position = destination; 
				aiMovement.SetTarget (walkTarget.transform); 
			}
		}


    private void FixedUpdate()
    {
       
//			if (Input.GetButtonDown("Fire3"))
//			{
//			isInDirectMode = ! isInDirectMode;
//			currentDestination = transform.position;// RESET CLICK TARGET
//			}
//		if (isInDirectMode) 
//		{
//			//ProcessMouseClick ();
//		
//		} else 
//		{
//			
//			//ProcessMouseMovement ();
//		}
//			
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



