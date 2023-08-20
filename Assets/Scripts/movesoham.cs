using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class movesoham : MonoBehaviour
{
    enum joint
    {
        knee,
        elbow,
    };

    [SerializeField]
    joint js = new joint();

    float gyrox;
    float gyroy;
    float accx;
    float accy;
    float accz;

    float errgx, errgy,errax,erray,erraz;

    public string[] data;
    
    // Kalman filter parameters
    KalmanFilter kalmanX;
    KalmanFilter kalmanY;

    [SerializeField]
    TMP_Text angText;

    int sample_size = 500;
    int scale = 10;

    float alpha = 0.91f;

    public GameObject leg;

    void Start()
    {
        // Initialize Kalman filters
        kalmanX = new KalmanFilter();
        kalmanY = new KalmanFilter();
    }

    void calibration()
    {
        // Calibration code...
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
        // Initialize Kalman filter states after calibration
        kalmanX.SetState(gyrox);
        kalmanY.SetState(gyroy);
    }

    void FixedUpdate()
    {
        if (data.Length > 3)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                calibration();
            }

            // Update sensor readings
            gyrox = float.Parse(data[0]) + (-1) * errgx;
            gyroy = float.Parse(data[1]) + (-1) * errgy;
            accx = float.Parse(data[2]) + (-1) * errax;
            accy = float.Parse(data[3]) + (-1) * erray;

            // Kalman filter prediction and update
            float filteredX = kalmanX.PredictAndUpdate(gyrox);
            float filteredY = kalmanY.PredictAndUpdate(gyroy);

            int angle_x = (int)((alpha * accx + (1 - alpha) * filteredX) * scale);
            int angle_y = (int)((alpha * accy + (1 - alpha) * filteredY) * scale);

            // Rest of the code...
            Debug.Log(angle_y);
            switch (js)
            {
                
                case joint.elbow:
                    leg.transform.localEulerAngles =  Vector3.Lerp(new Vector3(leg.transform.localRotation.x,leg.transform.localRotation.y,leg.transform.localRotation.z),(new Vector3(angle_x,leg.transform.localRotation.y,leg.transform.localRotation.z)),1f);
                    break;
                case joint.knee:
                    leg.transform.localEulerAngles =  Vector3.Lerp(new Vector3(leg.transform.localRotation.x,leg.transform.localRotation.y,leg.transform.localRotation.z),(new Vector3(angle_x,leg.transform.localRotation.y,leg.transform.localRotation.z)),1f);  
                    break;
            }
            
            angText.text = "ANGLE: "+angle_x+"° ";
        }
    }
}

public class KalmanFilter
{
    private float Xk; // State estimate
    private float Pk; // Estimate error covariance

    private float Q = 0.01f; // Process noise covariance
    private float R = 0.1f;  // Measurement noise covariance

    public void SetState(float initialState)
    {
        Xk = initialState;
        Pk = 1.0f; // Initial covariance estimation
    }

    public float PredictAndUpdate(float measurement)
    {
        // Prediction
        float Xk_ = Xk;
        float Pk_ = Pk + Q;

        // Kalman gain
        float K = Pk_ / (Pk_ + R);

        // Update state
        Xk = Xk_ + K * (measurement - Xk_);
        Pk = (1 - K) * Pk_;

        return Xk;
    }
}
