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
                Vector3 direction = (hit.point - raystarter.transform.position).normalized;
                Vector3 spawnPosition = hit.point + direction * 0.01f;

                var red = Instantiate(spot, spawnPosition, Quaternion.identity);
                red.transform.parent = leg.transform;

                // Calculate rotation to make the spot object face horizontally
                Vector3 targetNormal = Vector3.up; // Horizontal normal direction
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, targetNormal);
                red.transform.rotation = rotation;

                red.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            // Draw a debug line to visualize the ray
            Debug.DrawLine(ray.origin, hit.point, Color.blue, 1.0f);
}
}
}