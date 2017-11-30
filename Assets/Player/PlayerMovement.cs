using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float stopRadius = 0.1f;

    ThirdPersonCharacter m_Character;   			// A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
	private bool isInDirectMode= false;
	private Vector3 m_Move;						 // vertical and horizontal movement relative to camera 
	private Transform m_Cam;                  	// A reference to the main camera in the scenes transform    
	private Vector3 m_CamForward;              // The current forward direction of the camera
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
		m_Cam = Camera.main.transform;
    }
	// TODO Fix issue with wasd controls conflicting with point and click movement
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
       
		if (CrossPlatformInputManager.GetButtonDown("Fire3"))
			{
			isInDirectMode = ! isInDirectMode;
			if (isInDirectMode)
				print ("in keyboad moade");
			else
				print ("In mouse mode");
			}
		if (isInDirectMode) 
		{
			ProcessDirectMovement ();
		
		} else 
		{
			ProcessMouseMovement ();
		}
			
    }

	void ProcessDirectMovement()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		if (m_Cam != null)
		{
			// calculate camera relative direction to move:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = v*m_CamForward + h*m_Cam.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			m_Move = v*Vector3.forward + h*Vector3.right;
		}

		m_Character.Move (m_Move,false,false);
	}


	void ProcessMouseMovement ()
	{
		if (Input.GetMouseButton (0)) {
			print ("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString ());
			switch (cameraRaycaster.layerHit) {
			case Layer.Walkable:
				currentClickTarget = cameraRaycaster.hit.point;
				//where we click     
				break;
			case Layer.Enemy:
				Debug.Log ("Not moving there");
				break;
			default:
				Debug.LogWarning ("Should not be here");
				return;
			}
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if (playerToClickPoint.magnitude >= stopRadius) {
			m_Character.Move (playerToClickPoint, false, false);
		}
		else {
			m_Character.Move (Vector3.zero, false, false);
		}
	}
}

