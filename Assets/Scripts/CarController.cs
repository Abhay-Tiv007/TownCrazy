
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class CarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public bool enabled = true;

    public AudioClip[] carSFX;
    public AudioSource carAlarmSource;
    public float minEnginePitch = 0.4f;
    public float maxEnginePitch = 1.7f;
    public Vector3 com;
    public GameObject tailLights;
    public GameObject reverseLights;

    public Transform frontLeftWheel;
    public Transform frontRightWheel;


    //Tire Screeches
    [Space]
    [Space]
    public ParticleSystem frontLeftParticles;
    public ParticleSystem frontRightParticles;
    public ParticleSystem rearLeftParticles;
    public ParticleSystem rearRightParticles;
    [Space]
    public TrailRenderer frontLeftTrail;
    public TrailRenderer frontRightTrail;
    public TrailRenderer rearLeftTrail;
    public TrailRenderer rearRightTrail;
    [Space]
    public AudioSource tireScreeching;

    [SerializeField] float lateralThreshold = 0.5f;
    [SerializeField] float skidVelocityThreshold = 5.0f;
    [SerializeField] float accelerationCapFactor = 2.0f;
    [SerializeField] float max_velocity = 30.0f;
    private Rigidbody carRB;
    private AudioSource carAudioSource;

    private bool isActive = false;

    //for skidding fade out
    private IEnumerator skidCoroutine;


    void Start(){
        carRB = GetComponent<Rigidbody>();
        carAudioSource = GetComponent<AudioSource>();
        carAudioSource.clip = carSFX[2];

        carRB.centerOfMass = com;
        tailLights.SetActive(false);
        reverseLights.SetActive(false);

        //reset tire screeching
        rearLeftParticles.Stop();
        rearRightParticles.Stop();
        frontLeftParticles.Stop();
        frontRightParticles.Stop();
        tireScreeching.volume = 0;
    }

    public void FixedUpdate()
    {
        


        //Check whether the car is moving in forward or backward direction
        float car_velocity = Vector3.Dot(transform.forward, carRB.velocity);
        
        float velocity_ratio = car_velocity/max_velocity;
        

        if(!enabled){
            foreach (AxleInfo axleInfo in axleInfos) {
                if (axleInfo.steering) {
                    axleInfo.leftWheel.steerAngle = 0;
                    axleInfo.rightWheel.steerAngle = 0;
                }
                if (axleInfo.motor) {
                    axleInfo.leftWheel.brakeTorque = 90000;
                    axleInfo.rightWheel.brakeTorque = 90000;
                }
            }

            frontLeftParticles.Stop();
            frontRightParticles.Stop();
            rearLeftParticles.Stop();
            rearRightParticles.Stop();

            tireScreeching.volume = 0.0f;
            carAlarmSource.Stop();

            return;
        }

        if (!isActive)
            return;

        carAudioSource.pitch = minEnginePitch + (velocity_ratio * velocity_ratio) * (maxEnginePitch - minEnginePitch);

        //Debug.Log(car_velocity);

        float motor = 0f;
        if(Input.GetButton("Accelerate"))
            motor = maxMotorTorque;

        if(Input.GetButton("Brake"))
            motor = maxMotorTorque * -1/accelerationCapFactor;

        if (Input.GetButton("Accelerate") && Input.GetButton("Brake"))
            motor = 0f;

        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");


        //Car reverse lights and audio
        if(car_velocity < 0 && Input.GetButton("Brake") && !Input.GetButton("Horizontal") && !carAlarmSource.isPlaying && !tailLights.activeInHierarchy)
        {
            carAlarmSource.clip = carSFX[0];
            carAlarmSource.Play();
            if (!reverseLights.activeInHierarchy)
            {
                reverseLights.SetActive(true);
            }
        }else if((!Input.GetButton("Brake") || tailLights.activeInHierarchy) && carAlarmSource.isPlaying)
        {
            carAlarmSource.Stop();
            if (reverseLights.activeInHierarchy)
            {
                reverseLights.SetActive(false);
            }
        }


        //Car brake lights and audio
        if (car_velocity < 0 && Input.GetButton("Accelerate") && !tailLights.activeInHierarchy)
        {
            reverseLights.SetActive(false);
            tailLights.SetActive(true);
        }
        else if (car_velocity > 0 && Input.GetButton("Brake") && !tailLights.activeInHierarchy)
        {
            reverseLights.SetActive(false);
            tailLights.SetActive(true);
        }else if(((car_velocity >= 0 && !Input.GetButton("Brake") && tailLights.activeInHierarchy) ||
                  (car_velocity <= 0 && !Input.GetButton("Accelerate") && tailLights.activeInHierarchy)))
        {
            tailLights.SetActive(false);
        }

        //Tire Screech Logic

        
        if (isTireScreeching())
        {
            if(car_velocity > 0)
            {
                if (!rearLeftParticles.isPlaying)
                {
                    rearLeftParticles.Play();
                    rearRightParticles.Play();
                    rearLeftTrail.emitting = true;
                    rearRightTrail.emitting = true;

                    tireScreeching.volume = 0.3f;
                    //tireScreeching.Play();


                    //can remove these four lines
                    frontLeftParticles.Stop();
                    frontRightParticles.Stop();
                    frontLeftTrail.emitting = false;
                    frontRightTrail.emitting = false;
                }
            }
            else if(car_velocity < 0)
            {
                if (!frontLeftParticles.isPlaying)
                {
                    frontLeftParticles.Play();
                    frontRightParticles.Play();
                    frontLeftTrail.emitting = true;
                    frontRightTrail.emitting = true;

                    tireScreeching.volume = 0.3f;
                    //tireScreeching.Play();


                    //can remove these four lines
                    rearLeftParticles.Stop();
                    rearRightParticles.Stop();
                    rearLeftTrail.emitting = false;
                    rearRightTrail.emitting = false;
                }
            }
            else
            {
                stopSkidding();
            }
        }
        else
        {
            stopSkidding();
        }


        frontLeftWheel.localEulerAngles = new Vector3(frontLeftWheel.localEulerAngles.x, steering, frontLeftWheel.localEulerAngles.z);
        frontRightWheel.localEulerAngles = new Vector3(frontLeftWheel.localEulerAngles.x, steering, frontLeftWheel.localEulerAngles.z);

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

    private void stopSkidding()
    {
        rearLeftParticles.Stop();
        rearRightParticles.Stop();
        frontLeftParticles.Stop();
        frontRightParticles.Stop();

        tireScreeching.volume = 0.0f;
        //tireScreeching.Pause();


        rearLeftTrail.emitting = false;
        rearRightTrail.emitting = false;
        frontLeftTrail.emitting = false;
        frontRightTrail.emitting = false;
    }

    private float getLateralVelocity()
    {
        return Vector3.Dot(transform.right, carRB.velocity);
    }

    private bool isTireScreeching()//out float lateralVelocity, out bool accelerating)
    {
        //lateralVelocity = getLateralVelocity();
        bool accelerating = Input.GetButton("Accelerate") && Vector3.Dot(transform.forward, carRB.velocity) < skidVelocityThreshold;
        //Debug.Log(accelerating);
        //Debug.Log(getLateralVelocity());
        if (accelerating)
            return true;

        if (Mathf.Abs(getLateralVelocity()) > lateralThreshold)
            return true;

        return false;
    }

    public void StopCar(){
        Input.ResetInputAxes();
        stopSkidding();
        enabled = false;
        isActive = false;
        maxMotorTorque = 0.0f;
        maxSteeringAngle = 0.0f;

    }

    public void ActivateCar()
    {
        isActive = true;
    }

    public bool isActiveAndRunning()
    {
        return isActive;
    }

    //Debug Visually
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's COM position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(GetComponent<Rigidbody>().centerOfMass), 0.1f);

        //GetComponent<Rigidbody>().centerOfMass = com;
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, 1);
    }
}
    
[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}