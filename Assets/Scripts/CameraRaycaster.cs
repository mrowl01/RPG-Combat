using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = { Layer.Enemy, Layer.Walkable };
    [SerializeField] float distanceToBackground = 100f; // after this distance just give up 

    Camera viewCamera;

    RaycastHit m_hit;
    public RaycastHit hit // return what was hit
    {
        get { return m_hit; }
    }

    Layer m_layerHit;
    public Layer layerHit // return the layer that was hit
    {
        get { return m_layerHit; }
    }

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
                m_hit = hit.Value;
                m_layerHit = layer;
                return;
            }
        }
        // Otherwise return background hit and get out
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
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
