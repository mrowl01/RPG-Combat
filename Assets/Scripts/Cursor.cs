using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    CameraRaycaster camCaster;

	// Use this for initialization
	void Start () {
        camCaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {
        print(camCaster.layerHit);
	}
}
