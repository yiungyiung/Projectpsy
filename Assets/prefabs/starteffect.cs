using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starteffect : MonoBehaviour
{
    public float storedangle = 0;
    public GameObject joint;
    public ParticleSystem ps;
    bool started = false;


    void Update()
    {   //Debug.Log(storedangle+"  "+joint.transform.localRotation.x);
        if(storedangle==(joint.transform.localRotation.x)&&!started)
        {   
            ps.Play();
            started=true;
        }
        if(started&&!ps.isPlaying)
        {
            started=false;
        }
    }

}
