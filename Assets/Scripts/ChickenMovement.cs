using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{

    public float moveSpeed;
    public float lifeExpectancy;

    private float delay = 1.0f;
    private float delayTime = 0.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        delay = Random.Range(1, 3);
        Destroy(gameObject, Random.Range(lifeExpectancy/2, lifeExpectancy));
    }

    // Update is called once per frame
    void Update()
    {

        delayTime += Time.deltaTime;
        if(delayTime > delay ){
            shiftRotate();
            delay = Random.Range(1, 3);
            delayTime = 0.0f;
        }

        
        transform.Translate(moveSpeed * Vector3.forward * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //rb.AddForce(transform.forward * moveSpeed);
    }

    void shiftRotate(){
        Vector3 randomPoint = new Vector3(Random.Range(-100, 100), transform.position.y , Random.Range(-100, 100));
        Quaternion lookRotation = Quaternion.LookRotation((randomPoint - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
    }
}
