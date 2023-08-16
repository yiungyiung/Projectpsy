using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    Camera cam;
    public LayerMask mask;
    public GameObject raystarter;
    public GameObject spot;

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
                Vector3 direction = (hit.point - raystarter.transform.position).normalized;
                Vector3 spawnPosition = hit.point + direction * 0.01f;
                 //Vector3 spawnPosition = hit.point + hit.normal * 0.01f;
                //hit.transform.GetComponent<Renderer>().material.color = Color.red;
                Instantiate(spot, spawnPosition, Quaternion.identity);
            }

            // Draw a debug line to visualize the ray
            Debug.DrawLine(ray.origin, hit.point, Color.blue, 1.0f);
        }
    }
}