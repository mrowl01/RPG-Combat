using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
    [SerializeField] float stopRadius = 0.1f;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
       
        if (Input.GetMouseButton(0) )
        {
            print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());
          
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;  //where we click     
                    break;
                case Layer.Enemy:
                    Debug.Log("Not moving there");
                    break;
                default:
                    Debug.LogWarning("Should not be here");
                    return;
            }
        }
        var playerToClickPoint = currentClickTarget- transform.position;
        if (playerToClickPoint.magnitude >= stopRadius)
        {
            m_Character.Move(playerToClickPoint, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
        

    }
}

