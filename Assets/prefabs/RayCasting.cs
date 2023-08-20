using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    Camera cam;
    public LayerMask mask;
    public GameObject raystarter;
    public GameObject spot;
    public GameObject leg;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detect left mouse button click
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                Debug.Log("Hit object: " + hit.point);
                
                // Calculate the normal direction of the hit point
                Vector3 normalDirection = hit.normal;
                
                // Spawn the object slightly above the hit point in the normal direction
                Vector3 spawnPosition = hit.point + normalDirection * 0.01f;
                
                // Instantiate the spot object and set its parent to "leg"
                var red = Instantiate(spot, spawnPosition, Quaternion.identity);
                red.transform.parent = leg.transform;
                
                // Align the spot's up direction with the normal direction
                red.transform.up = normalDirection;
            }

            // Draw a debug line to visualize the ray
            Debug.DrawLine(ray.origin, hit.point, Color.blue, 1.0f);
        }
    }
}
