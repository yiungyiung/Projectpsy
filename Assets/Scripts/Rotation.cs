using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    enum joint
    {
        Rotate_X,
        Rotate_Y,
        Rotate_Z,
    };
    [SerializeField]
    joint js= new joint();
    public float rotationSpeed = 200f;
    void Rotation_Y(){
        if (Input.GetMouseButton(0))
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float rotationChange = mouseInputX * rotationSpeed * Time.deltaTime;
            transform.localRotation *= Quaternion.Euler(0, -rotationChange, 0);
        }
    }
    void Rotation_X(){
        if (Input.GetMouseButton(0))
        {
            float mouseInputY = Input.GetAxis("Mouse Y");
            float rotationChange = mouseInputY * rotationSpeed * Time.deltaTime;
            transform.localRotation *= Quaternion.Euler(rotationChange, 0, 0);
        }
    }
    void Rotation_Z(){
        if (Input.GetMouseButton(0))
        {
            float mouseInputY = Input.GetAxis("Mouse X");
            float rotationChange = mouseInputY * rotationSpeed * Time.deltaTime;
            transform.localRotation *= Quaternion.Euler(0, 0, -rotationChange);
        }
    }
    
    void Update()
    {
        switch (js)
            {
                
                case joint.Rotate_X:
                    Rotation_X();
                    break;
                case joint.Rotate_Z:
                    Rotation_Z();
                    break;
                case joint.Rotate_Y:
                    Rotation_Y();
                    break;
            }
    }
}