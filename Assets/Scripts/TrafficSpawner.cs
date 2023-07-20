using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{

    public GameObject[] target;
    public float delay = 1.0f;

    private float delayTime = 0.0f;
    private Transform targetChild;
    private int totalCars;

    void Start(){
        targetChild = transform.GetChild(0);
        totalCars = target.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
        delayTime += Time.deltaTime;
        if(delayTime > delay ){
            int carNo = (int)Random.Range(0, totalCars);
            carNo = carNo >= totalCars ? totalCars - 1: carNo;
            //Debug.Log(carNo);
            GameObject myCar = Instantiate(target[carNo], transform.position, transform.rotation);
            if(myCar.GetComponent<CarControllerAuto>() != null)
                myCar.GetComponent<CarControllerAuto>().target =  targetChild;
            delayTime = 0.0f;
        }
    }
}
