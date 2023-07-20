
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class CarControllerAuto : MonoBehaviour {

    //public float speed;

    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public Transform target;

    void Start()
    {
        Destroy(gameObject, 45.0f);
    }
        
    public void FixedUpdate()
    {
        Vector3 targetDir = (target.position - transform.position).normalized;
        
        float motor = maxMotorTorque;
        float steering = maxSteeringAngle * Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
            
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }
}