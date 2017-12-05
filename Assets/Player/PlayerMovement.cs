using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float walkStopRadius = 0.1f;
	[SerializeField] float attackStopRadius = 5f;

	ThirdPersonCharacter character;   			// A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;
	private bool isInDirectMode= false;
	private Vector3 movement;						 // vertical and horizontal movement relative to camera 
	private Transform mainCamera;                  	// A reference to the main camera in the scenes transform    
	private Vector3 mainCameraForward;              // The current forward direction of the camera
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
		mainCamera = Camera.main.transform;
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
			ProcessDirectMovement ();
		
		} else 
		{
			
			//ProcessMouseMovement ();
		}
			
    }

	void ProcessDirectMovement()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		if (mainCamera != null)
		{
			// calculate camera relative direction to move:
			mainCameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
			movement = v*mainCameraForward + h*mainCamera.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			movement = v*Vector3.forward + h*Vector3.right;
		}

		character.Move (movement,false,false);
	}


//	void ProcessMouseMovement ()
//	{
//		if (Input.GetMouseButton (0))
//		{
//			clickPoint = cameraRaycaster.hit.point;
//			switch (cameraRaycaster.currentLayerHit) {
//			case Layer.Walkable:
//				currentDestination = ShortDestination (clickPoint, walkStopRadius); // take a vector and shorten it by the stopRadius
//				//where we click     
//				break;
//			case Layer.Enemy:
//				currentDestination = ShortDestination (clickPoint, attackStopRadius);
//				Debug.Log ("enemy");
//				break;
//			default:
//				Debug.LogWarning ("Should not be here");
//				return;
//			}
//		}
//		WalkToDestination ();
//	
//	}

	void WalkToDestination ()
	{
		var playerToClickPoint = currentDestination - transform.position;
		if (playerToClickPoint.magnitude >= walkStopRadius) {
			character.Move (playerToClickPoint, false, false);
		}
		else {
			character.Move (Vector3.zero, false, false);
		}
	}

	Vector3 ShortDestination (Vector3 destination , float shortening)
	{
		Vector3 reductionVector= (destination- transform.position).normalized * shortening;// 
		return destination- reductionVector;
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawLine (transform.position,currentDestination);
		Gizmos.DrawSphere (currentDestination, 0.1f);
		Gizmos.DrawSphere (clickPoint, 0.2f);
	}

}



