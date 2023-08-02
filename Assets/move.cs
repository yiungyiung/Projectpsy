using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class move : MonoBehaviour
{   
    enum joint
    {
        knee,
        elbow,
    };
    [SerializeField]
    joint js= new joint();
    float gyrox;
    float gyroy;
    float accx;
    float accy;
    float accz;
    public string[] data;
    float errgx;

    [SerializeField]
    TMP_Text angText;
    float errgy;

    float errax;

    float erray;

    float erraz;

    int sample_size = 500;

    int scale = 10;

    float alpha = 0.91f;
    public GameObject leg;
    void calibration()
    {
        float sumgx = 0;
        float sumgy = 0;
        float sumax = 0;
        float sumay = 0;
        float sumaz = 0;
        int i = 0;
        while (i < sample_size)
        {   
            gyrox = float.Parse(data[0]);
            gyroy = float.Parse(data[1]);
            accx = float.Parse(data[2]);
            accy = float.Parse(data[3]);
            sumgx += gyrox;
            sumgy += gyroy;
            sumax += accx;
            sumay += accy;
            sumaz += accz;
            i += 1;
        }
        errgx = sumgx / sample_size;
        errgy = sumgy / sample_size;
        errax = sumax / sample_size;
        erray = sumay / sample_size;
        erraz = sumaz / sample_size;
    }

    // Update is called once per frame
    void Update()
    {   
        if(data.Length>3)
        {
        if (Input.GetKeyDown(KeyCode.C))
        {
            calibration();
        }       
            gyrox = float.Parse(data[0]) + (-1) * errgx;
            gyroy = float.Parse(data[1]) + (-1) * errgy;
            accx = float.Parse(data[2]) + (-1) * errax;
            accy = float.Parse(data[3]) + (-1) * erray;
            Debug.Log(data);
            int angle_x = (int)((alpha * accx + (1 - alpha) * gyrox) * scale);
            int angle_y = (int)((alpha * accy + (1 - alpha) * gyroy) * scale);
            Debug.Log(angle_y);
            switch (js)
            {
                
                case joint.elbow:
                    leg.transform.eulerAngles =  Vector3.Lerp(new Vector3(leg.transform.rotation.x,leg.transform.rotation.y,leg.transform.rotation.z),(new Vector3(0,0,(angle_y))),1f);
                    break;
                case joint.knee:
                    leg.transform.eulerAngles =  Vector3.Lerp(new Vector3(leg.transform.rotation.x,0,180),(new Vector3(-1*angle_y,0,180)),1f);  
                    break;
            }
            
            angText.text = "ANGLE: "+angle_y+"° ";
        }
    }
}
