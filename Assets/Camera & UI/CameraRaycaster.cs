using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = { Layer.Enemy, Layer.Walkable };
    [SerializeField] float distanceToBackground = 100f; // after this distance just give up 

    Camera viewCamera;

    RaycastHit rayCastHit;
    public RaycastHit hit // return what was hit
    {
        get { return rayCastHit; }
    }

    Layer layerHit;
    public Layer currentLayerHit // return the layer that was hit
    {
        get { return layerHit; }
    }

	public delegate void OnLayerChange(Layer newLayer);
	public event OnLayerChange onLayerChange;



    private void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
        
    }

    private void Update()
    {
        // look for and return priority layer hit

        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);

            if (hit.HasValue)
            {
                rayCastHit = hit.Value;

				if (layerHit != layer) //if change in layer (direct variable changes before the dependant variable) layerHit= the "Setter"
				{
					layerHit = layer;
					onLayerChange (layer);
				}
				return;
            }
        }
        // Otherwise return background hit and get out
        rayCastHit.distance = distanceToBackground;


		// layer did not change since it did not hit any
		// also check if the previous layer isnt already set to RayCastEndStop 
		//to prevent unncessary setting the layerhit each frame
		if (layerHit != Layer.RaycastEndStop) {
			layerHit = Layer.RaycastEndStop;
			onLayerChange (Layer.RaycastEndStop);
		}
        //layerHit = Layer.RaycastEndStop;
    }

    RaycastHit ? RaycastForLayer(Layer layer) // did it hit anything?
    {
        int layerMask = 1 << (int)layer; // layer mask is = to the layer given to this paramater (either enemy,or walkable) 
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);// tracks mouse on screen and shoots ray

        RaycastHit hit;// used as an out paramater
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);

        if (hasHit)
        {
            return hit;
        }
        return null;

    }
}
